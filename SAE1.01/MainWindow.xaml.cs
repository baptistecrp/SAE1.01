using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SAE1._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Regex regexTagEnnemi = new Regex("^ennemi.$");
        // Timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // Vitesse ennemi
        private double vitesseEnnemi = 0.5;
        // Skin ennemi
        private ImageBrush ennemiSkin = new ImageBrush();
        // Apparation Nouvel Ennemi
        private bool apparitionNouvelEnnemi = false;
        // Liste image lettre
        private ImageBrush[] lettreImg = new ImageBrush[26];
        // Alphabet
        private String[] alpha = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        // liste élément à supprimer
        private List<Rectangle> elementASuppr = new List<Rectangle>() ;
        
        public MainWindow()
        {
            InitializeComponent();
            // Configuration du timer
            dispatcherTimer.Tick += Jeu;
            // Rafraichissment chaque 16ms
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // Lancement timer
            dispatcherTimer.Start();
            // Creation de la liste des images de lettre
            for (int i = 0; i < alpha.Length; i++)
            {
                lettreImg[i] = new ImageBrush();
                lettreImg[i].ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/" + alpha[i] + ".png"));
            }
            // Creation Ennemi
            CreationEnnemi();
        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        { 
            Console.WriteLine(e.Key);
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)y.Tag == "ennemi" + e.Key)
                {
                    elementASuppr.Add(y);
                }
                
            }
        }
        private void CreationEnnemi()
        {
            Random random = new Random();
            // Creation nouvel ennemi
            Ennemi ennemi = new Ennemi(alpha[random.Next(0,26)], Ennemi.TYPE_NORMAL);
            int nbLettre = Array.IndexOf(alpha, ennemi.Lettre);
            Rectangle nouvelEnenemi = new Rectangle
            {
                Tag = "ennemi"+ennemi.Lettre,
                Height = 45,
                Width = 45,
                Fill = lettreImg[nbLettre],
            };
            // Placement aléatoire
            Canvas.SetTop(nouvelEnenemi, 0);
            Canvas.SetLeft(nouvelEnenemi, random.Next(30, (int)Application.Current.MainWindow.Width-(29+(int)nouvelEnenemi.Width)));
            myCanvas.Children.Add(nouvelEnenemi);
            // Modification vitesse pour accélérer
            vitesseEnnemi += 0.1;
        }
        private void DeplacementEnnemi(Rectangle ennemi)
        {
            // Deplacement vers le bas 
            Canvas.SetTop(ennemi, Canvas.GetTop(ennemi) + vitesseEnnemi);
        }
        private void Jeu(object sender, EventArgs e)
        {
            Random random = new Random();

            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                // Si le rectangle est un ennemi
                if (y is Rectangle && regexTagEnnemi.IsMatch((string)y.Tag))
                {

                    // Deplacement
                    DeplacementEnnemi(y);

                    // Detectiion hauteur pour apparation nouvel ennemi
                    if (Canvas.GetTop(y) >= random.Next(40, 130))
                    {
                        apparitionNouvelEnnemi = true;
                    }
                }
            }

            // Test pour faire apparaitre nouvel ennemi aléatoirement si possible
            if (apparitionNouvelEnnemi && random.Next(1,30) == 3)
            {
                CreationEnnemi();
                apparitionNouvelEnnemi = false;
            }
            foreach (Rectangle y in elementASuppr)
            {
                myCanvas.Children.Remove(y);
            }
        }
    }
}
