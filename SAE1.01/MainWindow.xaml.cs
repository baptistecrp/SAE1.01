using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        // Timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // Vitesse ennemi
        private double vitesseEnnemi = 0.5;
        // Skin ennemi
        private ImageBrush ennemiSkin = new ImageBrush();
        // Apparation Nouvel Ennemi
        private bool apparitonNouvelEnnemi = false;
        // Nombre ennemi sur la canvas
        private int nombreEnnemi = 0;
        public MainWindow()
        {
            InitializeComponent();
            // Configuration du timer
            dispatcherTimer.Tick += Jeu;
            // Rafraichissment chaque 16ms
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // Lancement timer
            dispatcherTimer.Start();
            CreationEnnemi();
            ennemiSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/A.png"));
        }
        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        { }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        { }
        private void CreationEnnemi()
        {
            Random random = new Random();
            // Creation nouvel ennemi
            Rectangle nouvelEnenemi = new Rectangle
            {
                Tag = "ennemi",
                Height = 45,
                Width = 45,
                Fill = ennemiSkin,
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

            // Remise à 0 du compteur
            nombreEnnemi = 0;

            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                // Si le rectangle est un ennemi
                if (y is Rectangle && (string)y.Tag == "ennemi")
                {
                    // Ajout d'un ennemi au compteur
                    nombreEnnemi += 1;

                    // Deplacement
                    DeplacementEnnemi(y);

                    // Detectiion hauteur pour apparation nouvel ennemi
                    if (Canvas.GetTop(y) >= random.Next(40, 130))
                    {
                        apparitonNouvelEnnemi = true;
                    }
                }
            }

            // Test pour faire apparaitre nouvel ennemi aléatoirement si possible
            if (apparitonNouvelEnnemi && nombreEnnemi <= 4 && random.Next(1,5) == 3)
            {
                CreationEnnemi();
                apparitonNouvelEnnemi = false;
            }
        }
    }
}
