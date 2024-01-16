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
        private Regex regexTagToutEnnemi = new Regex("^ennemi.");
        private Regex regexTagEnnemi = new Regex("^ennemi.$");
        private Regex regexTagLettre = new Regex("Lettre$");
        // Timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // Vitesse ennemi
        private double vitesseEnnemi = 0.5;
        // Vitesse annimation
        private double vitesseAnim = 10;
        // Liste image lettre
        private ImageBrush[] lettreImg = new ImageBrush[26];
        // Alphabet
        private String[] alpha = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        // liste élément à supprimer
        private List<Rectangle> elementASuppr = new List<Rectangle>() ;
        private List<Rectangle> animASuppr = new List<Rectangle>();
        private ImageBrush[] animLettreDisparition = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition4.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition5.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition6.png"))) };
        private ImageBrush[] animEnnemiMarche = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche3.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche4.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche5.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche6.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche7.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche8.png")))  , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche9.png"))) };
        private ImageBrush[] animFlamme = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme8.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme9.png"))) };
        private ImageBrush[] animEnnemiMort = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort8.png")))};
        private ImageBrush[] animJoueurStatique = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/joueurStatique/joueurStatique1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/joueurStatique/joueurStatique2.png"))) };

        // Curseur personnalisé
        private Cursor curseur = new Cursor(Application.GetResourceStream(new Uri("img/astro_arrow.cur", UriKind.Relative)).Stream);
        private Cursor curseurClick = new Cursor(Application.GetResourceStream(new Uri("img/astro_link.cur", UriKind.Relative)).Stream);

        // Score
        private int nbrScore = 0;
        // Nombre vie restante
        private int nbrVie = 3;
        // compteur pour animation/apparition
        private int compteur = 0;
        
        public MainWindow()
        {
            #if DEBUG
                        Console.WriteLine("Debug version");
            #endif
            InitializeComponent();
            // Application du curseur personnalisé sur le canvas
            myCanvas.Cursor = curseur;
            // Ouverture de la fenetre du menu
            MenuDialog menu = new MenuDialog();
            menu.ShowDialog();
            if (menu.DialogResult == false) { Application.Current.Shutdown(); }
            // Apparence du fond
            fond.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/fond.png")));
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
            int nbEnnemi = 0;
            #if DEBUG
                Console.WriteLine("Touche appuyer: " + e.Key);
            #endif
            Regex regexEnnemi = new Regex("^ennemi"+@e.Key);
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (regexEnnemi.IsMatch((string)y.Name+e.Key))
                {
                    elementASuppr.Add(y);
                    nbEnnemi++;
                }
            }
            if (nbEnnemi == 0)
            {
                nbrScore -= 2;
            }
            for (int i = 0;i < alpha.Length;i++) 
            { 
                if (e.Key.ToString() == alpha[i])
                {
                    lettreJoueur.Visibility = Visibility.Visible;
                    lettreJoueur.Fill = lettreImg[i];
                }
            }
            if (e.Key == Key.Enter)
            {
                Relance();
            }

        }
        private void CreationEnnemi()
        {
            Random random = new Random();
            int nbrRafraichissement = random.Next(30,51);

            if (compteur % nbrRafraichissement == 0)
            {
                // Creation nouvel ennemi
                Ennemi ennemi = new Ennemi(alpha[random.Next(0, 26)], Ennemi.TYPE_NORMAL);
                int nbLettre = Array.IndexOf(alpha, ennemi.Lettre);
                // Creation de la lettre
                Rectangle lettre = new Rectangle
                {
                    Name = "ennemi" + ennemi.Lettre + "Lettre",
                    Height = 45,
                    Width = 45,
                    Fill = lettreImg[nbLettre],
                };
                // Creation de l'ennemi
                Rectangle nouvelEnnemi = new Rectangle
                {
                    Name = "ennemi" + ennemi.Lettre,
                    Tag = "ennemi0",
                    Height = 115,
                    Width = 60,
                    Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/EnnemiTest.png"))),
                };
                // Placement aléatoire entre gauche et droite
                Canvas.SetTop(lettre, Canvas.GetTop(joueur) - 45);
                Canvas.SetTop(nouvelEnnemi, Canvas.GetTop(joueur));
                int x = random.Next(0, 2) * (int)Application.Current.MainWindow.Width;
                Canvas.SetLeft(lettre, x);
                Canvas.SetLeft(nouvelEnnemi, x);
                myCanvas.Children.Add(lettre);
                myCanvas.Children.Add(nouvelEnnemi);

                // Modification vitesse pour accélérer
                vitesseEnnemi += 0.05;
                #if DEBUG
                                Console.WriteLine("Vitesse Ennemi: " + vitesseEnnemi);
                #endif

                // Modification vitesse animation
                vitesseAnim *= 0.999;
                #if DEBUG
                                Console.WriteLine("Vitesse Animation: " + vitesseAnim);
                #endif
            }
        }
        private void DeplacementEnnemi(Rectangle ennemi)
        {
            // Deplacement vers la droite des ennemis à gauche
            if (Canvas.GetLeft(ennemi) < Canvas.GetLeft(joueur))
            {
                Canvas.SetLeft(ennemi, Canvas.GetLeft(ennemi) + vitesseEnnemi);
            }
            // Deplacement vers la gauche des ennemis à droite
            if (Canvas.GetLeft(ennemi) > Canvas.GetLeft(joueur)+joueur.Width)
            {
                Canvas.SetLeft(ennemi, Canvas.GetLeft(ennemi) - vitesseEnnemi);
            }

        }
        private void Jeu(object sender, EventArgs e)
        {
            Random random = new Random();
            if (nbrVie <= 0)
                        {
                            // Si plus de vie stop le jeu
                            dispatcherTimer.Stop();
                        }
            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                Animation(animEnnemiMarche, y, "ennemi", (int)vitesseAnim , true);
                Animation(animEnnemiMort, y, "mort", 2, false);
                Animation(animLettreDisparition, y, "lettreSuppr", 2, false);
                Animation(animJoueurStatique, y, "joueur", 10, true);
                Animation(animFlamme, y, "flamme", 3, true);




                // Rect pour gérer colision
                Rect ennemi = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                Rect joueurRect = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);

                // Si le rectangle est un ennemi
                if (y is Rectangle && regexTagToutEnnemi.IsMatch((string)y.Name))
                {
                    // Deplacement
                    DeplacementEnnemi(y);
                    y.Cursor = curseurClick;

                    // Test de collision avec joueur
                    if (ennemi.IntersectsWith(joueurRect))
                    {
                        // Si encore de vie on retire une vie + suppression de l'ennemi + score -1
                        if (regexTagEnnemi.IsMatch((string)y.Name))
                        {
                                nbrVie--;
                        }
                        elementASuppr.Add(y);
                        nbrScore--;
                    }
                }
            }

            CreationEnnemi();

            // Suppression des ennemis mort
            foreach (Rectangle y in elementASuppr)
            {
                if (regexTagLettre.IsMatch(y.Name))
                {
                    Rectangle rectangleLettreSuppr = new Rectangle
                    {
                        Tag = "lettreSuppr0",
                        Name = "lettreSuppr",
                        Width = 45,
                        Height = 39,
                    };
                    myCanvas.Children.Add(rectangleLettreSuppr);
                    Canvas.SetTop(rectangleLettreSuppr, Canvas.GetTop(y));
                    Canvas.SetLeft(rectangleLettreSuppr, Canvas.GetLeft(y));
                    myCanvas.Children.Remove(y);

                }
                else if (regexTagEnnemi.IsMatch(y.Name))
                {
                    Rectangle rectangleMort = new Rectangle
                    {
                        Tag = "mort0",
                        Name = "mort",
                        Width = 140,
                        Height = 165,
                    };
                    myCanvas.Children.Add(rectangleMort);
                    Canvas.SetTop(rectangleMort, Canvas.GetTop(y) - (rectangleMort.Height - y.Height));
                    Canvas.SetLeft(rectangleMort, Canvas.GetLeft(y) - (rectangleMort.Width - y.Width));
                    myCanvas.Children.Remove(y);
                    nbrScore++;
                }
                else
                {
                    myCanvas.Children.Remove(y);
                }
                
            }
            // vidage de la liste des elements a supprimer pour optimiser
            elementASuppr.Clear();
            
            // Changement label score
            score.Content = "Score: " + nbrScore;

            // Changement label vie
            vieRestante.Content = "Vie Restante: " + nbrVie;

            // Cacher lettre au dessus joueur
            if (compteur % 40 == 0)
            {
                lettreJoueur.Visibility = Visibility.Hidden;
            }
            compteur++;
        }

        public void Animation(ImageBrush[] listeImage, Rectangle rectangle, string nomAnim, int vitesse, bool repete)
        {
            // Si repete est true lorsqu'on est à la dernière image on reviens à la première
            if (repete && (string)rectangle.Tag == nomAnim + listeImage.Length.ToString() && compteur % vitesse == 0)
            {
                rectangle.Tag = nomAnim + "0";
                rectangle.Fill = listeImage[0];
            }
            // Si repete est false lorsqu'on est à la dernière image on la supprime
            else if (!repete &&(string)rectangle.Tag == nomAnim + listeImage.Length.ToString() && compteur%vitesse==0)
            {
                    elementASuppr.Add(rectangle);
            }
            // Si on est à la première image on la fait apparaitre directement
            else if ((string)rectangle.Tag == nomAnim + "0")
            {
                rectangle.Tag = nomAnim + "1";
                rectangle.Fill = listeImage[0];
            }
            else
            {
                // pour chaque image de la liste d'image pour l'animation
                for (int i = 0; i < listeImage.Length; i++)
                {
                    // Gestion avec le nom des rectangle pour changer image
                    if ((string)rectangle.Tag == nomAnim + i.ToString() && compteur % vitesse == 0)
                    {
                        rectangle.Tag = nomAnim + ((i + 1).ToString());
                        rectangle.Fill = listeImage[i];
                        break;
                    }
                }
            }            
        }

        public void Relance()
        // Suppression des ennemis
        {
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (y is Rectangle && regexTagToutEnnemi.IsMatch(y.Name))
                {
                    elementASuppr.Add(y);
                }
            }
            foreach (Rectangle y in elementASuppr)
            {
                myCanvas.Children.Remove(y);
            }
            // Remise a zéro des valeurs de base
            elementASuppr.Clear();
            nbrScore = 0;
            nbrVie = 3;
            compteur = 0;
            vitesseEnnemi = 0.5;
            dispatcherTimer.Start();
            
        }
    }
}
