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
        private Regex regexTagEnnemi = new Regex("^ennemi.");
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
        private List<Rectangle> animASuppr = new List<Rectangle>();
        private ImageBrush[] animExplosion = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion1.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion2.png"))), new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion3.png"))), };
        private ImageBrush[] animEnnemi = new ImageBrush[] { new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/EnnemiTest.png"))) , new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/AnimationSheet_Character.png"))) };

        // Score
        private int nbrScore = 0;
        // Nombre vie restante
        private int nbrVie = 3;
        // compteur pour animation
        private int compteur = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            // Ouverture de la fenetre du menu
            MenuDialog menu = new MenuDialog();
            menu.ShowDialog();
            if (menu.DialogResult == false) { Application.Current.Shutdown(); }
            // Apparence du fond
            fond.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Background.png")));
            // Apparence du personnage
            joueur1.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/AnimationSheet_Character.png")));
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
            Regex regexEnnemi = new Regex("^ennemi"+@e.Key+".");
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (regexEnnemi.IsMatch((string)y.Tag+e.Key))
                {
                    elementASuppr.Add(y);
                    nbrScore ++;
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
            // Creation nouvel ennemi
            Ennemi ennemi = new Ennemi(alpha[random.Next(0,26)], Ennemi.TYPE_NORMAL);
            int nbLettre = Array.IndexOf(alpha, ennemi.Lettre);
            // Creation de la lettre
            Rectangle lettre = new Rectangle
            {
                Tag = "ennemi"+ennemi.Lettre,
                Height = 45,
                Width = 45,
                Fill = lettreImg[nbLettre],
            };
            // Creation de l'ennemi
            Rectangle nouvelEnnemi = new Rectangle
            {
                Tag = "ennemi"+ennemi.Lettre,
                Name = "ennemi0",
                Height = 112,
                Width = 45,
                Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/EnnemiTest.png"))),
            };
            // Placement aléatoire entre gauche et droite
            Canvas.SetTop(lettre, Canvas.GetTop(joueur1)-45);
            Canvas.SetTop(nouvelEnnemi, Canvas.GetTop(joueur1));
            int x = random.Next(0, 2) * (int)Application.Current.MainWindow.Width;
            Canvas.SetLeft(lettre, x);
            Canvas.SetLeft(nouvelEnnemi, x);
            myCanvas.Children.Add(lettre);
            myCanvas.Children.Add(nouvelEnnemi);

            // Modification vitesse pour accélérer
            vitesseEnnemi += 0.05;
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
                // Rect pour gérer colision
                Rect ennemi = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);

                // Si le rectangle est un ennemi
                if (y is Rectangle && regexTagEnnemi.IsMatch((string)y.Tag))
                {
                    Animation(animEnnemi, y, "ennemi", 20,true);
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
                Rectangle rectangleExplo = new Rectangle
                {
                    Tag = "explo",
                    Name = "explo0",
                    Width = 45,
                    Height = 45,
                };
                myCanvas.Children.Add(rectangleExplo);
                Canvas.SetTop(rectangleExplo, Canvas.GetTop(y));
                Canvas.SetLeft(rectangleExplo, Canvas.GetLeft(y)); 
                myCanvas.Children.Remove(y);
            }
            // vidage de la liste des elements a supprimer pour optimiser
            elementASuppr.Clear();

            foreach (Rectangle y in animASuppr)
            {
                myCanvas.Children.Remove(y);
            }
            // vidage de la liste des animations a supprimer pour optimiser
            animASuppr.Clear();
            
            // Changement label score
            score.Content = "Score: " + nbrScore;

            // Changement label vie
            vieRestante.Content = "Vie Restante: " + nbrVie;

            compteur++;
            Console.WriteLine(compteur);
            // système d'animation
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                Animation(animExplosion, y, "explo", 2, false);
            }
        }

        public void Animation(ImageBrush[] listeImage, Rectangle rectangle, string nomAnim, int vitesse, bool repete)
        {
            // Si repete est true lorsqu'on est à la dernière image on reviens à la première
            if (repete && (string)rectangle.Name == nomAnim + listeImage.Length.ToString() && compteur % vitesse == 0)
            {
                rectangle.Name = nomAnim + "0";
                rectangle.Fill = listeImage[0];
            }
            // Si repete est false lorsqu'on est à la dernière image on la supprime
            else if (!repete &&(string)rectangle.Name == nomAnim + listeImage.Length.ToString() && compteur%vitesse==0)
            {
                    animASuppr.Add(rectangle);
            }
            // Si on est à la première image on la fait apparaitre directement
            else if ((string)rectangle.Tag == nomAnim + "0")
            {
                rectangle.Name = nomAnim + "1";
                rectangle.Fill = listeImage[0];
            }
            else
            {
                // pour chaque image de la liste d'image pour l'animation
                for (int i = 0; i < listeImage.Length; i++)
                {
                    // Gestion avec le nom des rectangle pour changer image
                    if ((string)rectangle.Name == nomAnim + i.ToString() && compteur % vitesse == 0)
                    {
                        rectangle.Name = nomAnim + ((i + 1).ToString());
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
                if (y is Rectangle && (string)y.Tag != "joueur1" && (string)y.Tag!= "fond")
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
