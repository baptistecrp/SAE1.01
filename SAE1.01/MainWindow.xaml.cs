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
        private double multiplicateurX = 1;
        private double multiplicateurY = 1;
        private double largeurFenetre = 800;
        private double hauteurFenetre = 450;
        // Regex
        private Regex regexTagToutEnnemi = new Regex("^ennemi.");
        private Regex regexTagEnnemi = new Regex("^ennemi.$");
        private Regex regexTagLettre = new Regex("Lettre$");
        // Timer
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        // Vitesse ennemi
        private double vitesseEnnemi = 0.5;
        // Vitesse annimation
        private double vitesseAnim = 11;
        // Liste image lettre
        private ImageBrush[] lettreImg = new ImageBrush[26];
        // Alphabet
        private String[] alpha = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        // liste élément à supprimer
        private List<Rectangle> elementASuppr = new List<Rectangle>() ;
        private ImageBrush[] animLettreDisparition = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition4.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition5.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/lettreDisparition/lettreDisparition6.png"))) };
        private ImageBrush[] animEnnemiMarche = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche3.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche4.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche5.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche6.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche7.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche8.png")))  , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarche/ennemiMarche9.png"))) };
        private ImageBrush[] animEnnemiMarcheDroite = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche8.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMarcheDroite/ennemiMarche9.png"))) };
        private ImageBrush[] animFlamme = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme8.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/flamme/flamme9.png"))) };
        private ImageBrush[] animEnnemiMort = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMort/ennemiMort8.png"))) };
        private ImageBrush[] animEnnemiMortDroite = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort3.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort4.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort5.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort6.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort7.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/ennemiMortDroite/ennemiMort8.png")))};
        private ImageBrush[] animJoueurStatique = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/joueurStatique/joueurStatique1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/joueurStatique/joueurStatique2.png"))) };

        // Bonus
        private Rectangle bonus = new Rectangle();

        // Curseur personnalisé
        private Cursor curseur = new Cursor(Application.GetResourceStream(new Uri("img/astro_arrow.cur", UriKind.Relative)).Stream);
        private Cursor curseurClick = new Cursor(Application.GetResourceStream(new Uri("img/astro_link.cur", UriKind.Relative)).Stream);

        // Score
        private int nbrScore = 0;
        // Nombre vie restante
        private int nbrVie = 3;
        // compteur pour animation/apparition
        private int compteur = 0;

        // Random
        private Random random = new Random();

        // Son Ennemi
        private MediaPlayer sonEnnemi = new MediaPlayer();

        // Son jeu
        private MediaPlayer sonJeu = new MediaPlayer();


        // Temps Ecoule Bonus
        private int tempsEcouleBonus = 0;

        // Temps Ecoule Lettre Joueur
        private int tempsEcouleLettreJoueur = 0;

        // Temps pour gerer apparition ennemi
        private int tempsEcouleEnnemi = 0;


        // Test difficulté arcade
        bool diffArcade = false;
        bool diffNormal = false;
        bool diffDur = false;


        // Pour menu
        private bool jeuEstLance = false;



        public MainWindow()
        {
            #if DEBUG
                        Console.WriteLine("Debug version");
            #endif
            InitializeComponent();
            // Application du curseur personnalisé sur le canvas
            myCanvas.Cursor = curseur;
            MenuPrincipale();
            FenetrePrincipale.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./font/#pixelmix");

            // Apparence du fond
            solHerbe.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/solHerbe.png")));
            fond.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/fond.png")));
            // Configuration du timer
            dispatcherTimer.Tick += Jeu;
            // Rafraichissment chaque 16ms
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // Lancement timer
            dispatcherTimer.Start();
            // Chargement son ennemi
            sonJeu.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "son/musiqueFond.wav"));
            sonJeu.Volume = 0.02;
            // Lancement sonJeu
            sonJeu.Play();
            // Gestion de l'événement MediaEnded pour la boucle de la musique de fond
            sonJeu.MediaEnded += SonJeuFini;
            // Creation de la liste des images de lettre
            for (int i = 0; i < alpha.Length; i++)
            {
                lettreImg[i] = new ImageBrush();
                lettreImg[i].ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/" + alpha[i] + ".png"));
            }
        }
        private void Jeu(object sender, EventArgs e)
        {
            Console.WriteLine(diffArcade + " " + diffNormal+" " + diffDur);
            Console.WriteLine("Temps Bonus " + tempsEcouleBonus);
            CreationEnnemi();
            RedimensionFenetre();
            GestionElementASupprimer();
            GestionTemps();
            if (nbrVie <= 0)
            {
                // Si plus de vie stop le jeu
                dispatcherTimer.Stop();
            }

            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {

                Animation(animEnnemiMarche, y, "ennemi", (int)vitesseAnim , true);
                Animation(animEnnemiMarcheDroite, y, "ennemiDroite", (int)vitesseAnim, true);
                Animation(animEnnemiMortDroite, y, "mortDroite", 2, false);
                Animation(animEnnemiMort, y, "mort", 2, false);
                Animation(animLettreDisparition, y, "lettreSuppr", 2, false);
                Animation(animJoueurStatique, y, "joueur", 10, true);
                Animation(animFlamme, y, "flamme", 3, true);

                GestionEnnemi(y);


                

                // Si le rectangle est un bonus
                if (y is Rectangle && (string)y.Name == "bonus")
                {
                    y.Cursor = curseurClick;
                }
            }
            // Creation bonus aléatoirement
            if (tempsEcouleBonus >= random.Next(1250,3000))
            {
                ApparitionBonus();
            }

            
            
            
            // Changement label score
            score.Content = "Score: " + nbrScore;

            // Changement label vie
            vieRestante.Content = "Vie Restante: " + nbrVie;

            
        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            bool ennemiTuer = false;
            #if DEBUG
                Console.WriteLine("Touche appuyer: " + e.Key);
            #endif
            // Regex
            Regex regexEnnemi = new Regex("^ennemi"+@e.Key);
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (regexEnnemi.IsMatch((string)y.Name+e.Key))
                {
                    elementASuppr.Add(y);
                    ennemiTuer = true;
                    sonEnnemi.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "son/disparitionEnnemi.wav"));
                    sonEnnemi.Play();
                }
            }
            if (!ennemiTuer && e.Key != Key.Escape)
            {
                nbrScore -= 2;
            }
            for (int i = 0;i < alpha.Length;i++) 
            { 
                if (e.Key.ToString() == alpha[i])
                {
                    lettreJoueur.Visibility = Visibility.Visible;
                    lettreJoueur.Fill = lettreImg[i];
                    tempsEcouleLettreJoueur=0;
                }
            }
            if (e.Key == Key.Escape)
            {
                MenuPause();
                
            }
        }
        private void CreationEnnemi()
        {
            if (tempsEcouleEnnemi % random.Next(20, 41) == 0)
            {
                
                // Creation de la lettre
                int nbLettre = random.Next(0, 26);
                Rectangle lettre = new Rectangle
                {
                    Name = "ennemi" + alpha[nbLettre] + "Lettre",
                    Height = 48*multiplicateurY,
                    Width = 48*multiplicateurX,
                    Fill = lettreImg[nbLettre],
                };
                // Creation de l'ennemi
                Rectangle nouvelEnnemi = new Rectangle
                {
                    Name = "ennemi" + alpha[nbLettre],
                    Tag = "ennemi0",
                    Height = 115*multiplicateurY,
                    Width = 60*multiplicateurX,
                };
                // Placement aléatoire entre gauche et droite
                Canvas.SetTop(lettre, Canvas.GetTop(joueur) - lettre.Height);
                Canvas.SetTop(nouvelEnnemi, Canvas.GetTop(joueur));
                int x = random.Next(0, 2) * (int)Application.Current.MainWindow.Width;
                // pour que les ennemis apparaissent en dehors de la fenetre
                if (x == 0) { x -= (int)nouvelEnnemi.Width; }
                // si un ennemi est à droite on change le nom pour les animations
                if (x > Canvas.GetLeft(joueur) + joueur.Width)
                {
                    nouvelEnnemi.Tag = "ennemiDroite0";
                }
                Canvas.SetLeft(lettre, x);
                Canvas.SetLeft(nouvelEnnemi, x);
                myCanvas.Children.Add(lettre);
                myCanvas.Children.Add(nouvelEnnemi);

                if (diffNormal)
                {
                    // Modification vitesse pour accélérer
                    vitesseEnnemi += 0.075*multiplicateurX;
                }
                else if (diffDur)
                {
                    // Modification vitesse pour accélérer
                    vitesseEnnemi += 0.15* multiplicateurX;
                }
                // Modification vitesse animation
                if ((diffNormal || diffDur ) && (11 - (vitesseEnnemi/multiplicateurX )* 0.5) > 1)
                {
                    vitesseAnim = 11 - vitesseEnnemi*0.5;
                }
                tempsEcouleEnnemi = 0;
                #if DEBUG
                    Console.WriteLine("Vitesse Ennemi: " + vitesseEnnemi);
                #endif

                #if DEBUG
                    Console.WriteLine("Vitesse Animation: " + vitesseAnim);
                #endif
                Console.WriteLine(vitesseEnnemi);

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
            else if (Canvas.GetLeft(ennemi) > Canvas.GetLeft(joueur)+joueur.Width)
            {
                Canvas.SetLeft(ennemi, Canvas.GetLeft(ennemi) - vitesseEnnemi);
            }

        }
        public void GestionTemps()
        {
            // Cacher lettre au dessus joueur
            tempsEcouleLettreJoueur++;
            if (tempsEcouleLettreJoueur == 20)
            {
                lettreJoueur.Visibility = Visibility.Hidden;
                tempsEcouleLettreJoueur = 0;
            }

            // Test temps ecoule bonus
            tempsEcouleBonus++;
            if (tempsEcouleBonus % 45 == 0)
            {
                DisparitionBonus();
            }

            tempsEcouleEnnemi++;

            // Compteur de rafraichissement
            compteur++;
        }
        public void GestionEnnemi(Rectangle y)
        {
            // Rect pour gérer colision
            Rect joueurRect = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width, joueur.Height);

            // Si le rectangle est un ennemi
            if (y is Rectangle && regexTagToutEnnemi.IsMatch((string)y.Name))
            {
                // creation rect pour collision
                Rect ennemi = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                // Deplacement
                DeplacementEnnemi(y);

                // Test de collision avec joueur
                if (ennemi.IntersectsWith(joueurRect))
                {
                    // Si encore de vie on retire une vie + suppression de l'ennemi + score -1
                    if (regexTagEnnemi.IsMatch((string)y.Name))
                    {
                        nbrVie--;
                        nbrScore--;
                    }
                    elementASuppr.Add(y);
                    
                }
            }
        }
        public void GestionElementASupprimer()
        {
            foreach (Rectangle y in elementASuppr)
            {
                // Suppression des ennemis mort
                if (regexTagLettre.IsMatch(y.Name))
                {
                    Rectangle rectangleLettreSuppr = new Rectangle
                    {
                        Tag = "lettreSuppr0",
                        Name = "lettreSuppr",
                        Width = 45 * multiplicateurX,
                        Height = 39 * multiplicateurY,
                    };
                    myCanvas.Children.Add(rectangleLettreSuppr);
                    Canvas.SetTop(rectangleLettreSuppr, Canvas.GetTop(y));
                    Canvas.SetLeft(rectangleLettreSuppr, Canvas.GetLeft(y));

                }
                else if (regexTagEnnemi.IsMatch(y.Name))
                {

                    Rectangle rectangleMort = new Rectangle
                    {
                        Tag = "mort0",
                        Name = "mort",
                        Width = 140 * multiplicateurX,
                        Height = 165 * multiplicateurY,
                    };
                    if (Canvas.GetLeft(y) > Canvas.GetLeft(joueur) + joueur.Width)
                    {
                        rectangleMort.Tag = "mortDroite0";
                        Canvas.SetTop(rectangleMort, Canvas.GetTop(y) - (rectangleMort.Height - y.Height));
                        Canvas.SetLeft(rectangleMort, Canvas.GetLeft(y));
                    }
                    else
                    {
                        Canvas.SetTop(rectangleMort, Canvas.GetTop(y) - (rectangleMort.Height - y.Height));
                        Canvas.SetLeft(rectangleMort, Canvas.GetLeft(y) + (y.Width - rectangleMort.Width));
                        Console.WriteLine(Canvas.GetLeft(y));
                    }
                    myCanvas.Children.Add(rectangleMort);
                    nbrScore++;
                }
                myCanvas.Children.Remove(y);
            }
            // vidage de la liste des elements a supprimer pour optimiser
            elementASuppr.Clear();
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
            tempsEcouleBonus = 0;
            vitesseEnnemi = 0.5;
            vitesseAnim = 11;
            dispatcherTimer.Start();
            
        }
        public void RedimensionFenetre()
        {
            // méthode pour redimensionner tout les éléments de la fenetre 
            double largeurNouvelleFenetre = (int)Application.Current.MainWindow.Width;
            double hauteurNouvelleFenetre = (int)Application.Current.MainWindow.Height;
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                multiplicateurX = largeurFenetre/800;
                multiplicateurY = hauteurFenetre/450;
                y.Width = largeurNouvelleFenetre * (y.Width / largeurFenetre);
                y.Height = hauteurNouvelleFenetre * (y.Height / hauteurFenetre);
                Canvas.SetLeft(y, largeurNouvelleFenetre * (Canvas.GetLeft(y) / largeurFenetre));
                Canvas.SetTop(y, hauteurNouvelleFenetre * (Canvas.GetTop(y) / hauteurFenetre));
            }
            vitesseEnnemi = largeurNouvelleFenetre * (vitesseEnnemi / largeurFenetre);
            largeurFenetre = largeurNouvelleFenetre;
            hauteurFenetre = hauteurNouvelleFenetre;
        }
        public void ApparitionBonus()
        {
            if (!myCanvas.Children.Contains(bonus))
            {
                int valeurBonus = random.Next(1, 5);

                bonus = new Rectangle
                {
                    Name = "bonus"+valeurBonus,
                    Height = 50 * multiplicateurY,
                    Width = 50 * multiplicateurX,
                    Fill = lettreImg[valeurBonus] // à modifier quand image bonus finit
                };

                Canvas.SetTop(bonus, random.Next((int)bonus.Height, (int)Canvas.GetTop(lettreJoueur)-(int)bonus.Height));
                Canvas.SetLeft(bonus, random.Next((int)bonus.Width, (int)largeurFenetre) - bonus.Width);

                myCanvas.Children.Add(bonus);

                bonus.MouseLeftButtonDown += clicBonus;

                tempsEcouleBonus = 0;
            }
        }

        public void clicBonus(object sender, MouseButtonEventArgs e)
        {
            if (bonus.Name == "bonus1")
            {
                nbrScore += 25;
            }
            else if (bonus.Name == "bonus2")
            {
                nbrVie++;
            }
            else if (bonus.Name == "bonus3")
            {
                vitesseAnim /= 1.5;
                vitesseEnnemi /= 1.5;
            }
            else if (bonus.Name == "bonus4")
            {
                foreach (var y in myCanvas.Children.OfType<Rectangle>())
                {
                    if (y is Rectangle && regexTagToutEnnemi.IsMatch(y.Name))
                    {
                        elementASuppr.Add(y);
                    }
                }
            }

            // Disparition immédiate du bonus
            DisparitionBonus();
        }

        // Méthode pour gérer la disparition du bonus
        private void DisparitionBonus()
        {
            // Suppression du bonus
            if (bonus != null && myCanvas.Children.Contains(bonus))
            {
                myCanvas.Children.Remove(bonus);
                tempsEcouleBonus = 0;
            }
        }

        // Rembobiner et rejouer la musique de fond
        private void SonJeuFini(object sender, EventArgs e)
        {
            sonJeu.Position = TimeSpan.Zero;
            sonJeu.Play();
        }

        private void PleinEcran()
        {
            FenetrePrincipale.WindowStyle = WindowStyle.None;
            FenetrePrincipale.WindowState = WindowState.Maximized;
            RedimensionFenetre();
        }
        private void Fenetre()
        {
            FenetrePrincipale.Width = 800;
            FenetrePrincipale.Height = 450;
            FenetrePrincipale.WindowStyle = WindowStyle.SingleBorderWindow;
            FenetrePrincipale.WindowState = WindowState.Normal;
            RedimensionFenetre();
        }
        private void MenuPrincipale()
        {
            // Ouverture de la fenetre du menu
            MenuDialog menu = new MenuDialog();
            menu.ShowDialog();
            if (menu.option)
            {
                MenuOptions();
            }
            else if (menu.DialogResult == false) { Application.Current.Shutdown(); }
            else if (menu.DialogResult == true) { jeuEstLance = true; }
            // Test difficulté dur
            if (menu.durBool)
            {
                diffArcade = false;
                diffNormal = false;
                diffDur = true;
            }
            // Test difficulté arcade
            else if (menu.arcadeBool)
            {
                diffArcade = true;
                diffNormal = false;
                diffDur = false;

            }
            // Test difficulté normal
            else if (menu.normalBool)
            {
                diffArcade = false;
                diffNormal = true;
                diffDur = false;
            }
        }
        private void MenuPause()
        {
            PauseDialog menuPause = new PauseDialog();
            dispatcherTimer.Stop();
            menuPause.ShowDialog();
            if (menuPause.reprendre) { dispatcherTimer.Start(); }
            if (menuPause.quitter) { Application.Current.Shutdown(); }
            if (menuPause.relancer) { Relance(); }
            if (menuPause.option) { MenuOptions(); }
        }

        private void MenuOptions()
        {
            OptionDialog menuOptions = new OptionDialog();
            menuOptions.ShowDialog();
            if (menuOptions.pleinEcran) { PleinEcran(); }
            if (!menuOptions.pleinEcran) { Fenetre(); }
            if (menuOptions.quitter && jeuEstLance) { MenuPause(); }
            if (menuOptions.quitter && !jeuEstLance) { MenuPrincipale(); }
        }
    }
}
