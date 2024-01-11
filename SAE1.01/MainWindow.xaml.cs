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
        private bool apparitionNouvelEnnemi = true;
        // Liste image lettre
        private ImageBrush[] lettreImg = new ImageBrush[26];
        // Alphabet
        private String[] alpha = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        // liste élément à supprimer
        private List<Rectangle> elementASuppr = new List<Rectangle>() ;
        // Score
        private int nbrScore = 0;
        // Nombre vie restante
        private int nbrVie = 3;
        
        public MainWindow()
        {
            InitializeComponent();
            // Apparence du fond
            fond.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Background.png")));
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
                    nbrScore ++;
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
            // Placement aléatoire entre gauche et droite
            Canvas.SetTop(nouvelEnenemi, Canvas.GetTop(joueur1));
            Canvas.SetLeft(nouvelEnenemi, random.Next(0, 2)*(int)Application.Current.MainWindow.Width);
            myCanvas.Children.Add(nouvelEnenemi);
            // Modification vitesse pour accélérer
            vitesseEnnemi += 0.1;
        }
        private void DeplacementEnnemi(Rectangle ennemi)
        {
            // Deplacement vers la droite des ennemis à gauche
            if (Canvas.GetLeft(ennemi) < Canvas.GetLeft(joueur1))
            {
                Canvas.SetLeft(ennemi, Canvas.GetLeft(ennemi) + vitesseEnnemi);
            }
            // Deplacement vers la gauche des ennemis à droite
            if (Canvas.GetLeft(ennemi) > Canvas.GetLeft(joueur1)+joueur1.Width)
            {
                Canvas.SetLeft(ennemi, Canvas.GetLeft(ennemi) - vitesseEnnemi);
            }

        }
        private void Jeu(object sender, EventArgs e)
        {
            Random random = new Random();

            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                Rect ennemi = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);

                // Si le rectangle est un ennemi
                if (y is Rectangle && regexTagEnnemi.IsMatch((string)y.Tag))
                {

                    // Deplacement
                    DeplacementEnnemi(y);



                    // Test de collision avec joueur
                    if (ennemi.IntersectsWith(joueur))
                    {
                        if (nbrVie <= 0)
                        {
                            // Si plus de vie stop le jeu
                            dispatcherTimer.Stop();
                        }
                        else
                        {
                            // Si encore de vie on retire une vie + suppression de l'ennemi + score -1
                            nbrVie--;
                            elementASuppr.Add(y);
                            nbrScore--;
                        }
                    }
                }
            }

            // Test pour faire apparaitre nouvel ennemi aléatoirement si possible
            if (apparitionNouvelEnnemi && random.Next(1,35) == 3)
            {
                CreationEnnemi();
            }
            // Suppression des ennemis mort
            foreach (Rectangle y in elementASuppr)
            {
                myCanvas.Children.Remove(y);
            }

            // Changement label score
            score.Content = "Score: " + nbrScore;

            // Changement label vie
            vieRestante.Content = "Vie Restante: " + nbrVie;
        }
    }
}
