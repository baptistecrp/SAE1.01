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
        private List<Rectangle> animASuppr = new List<Rectangle>();

        // Score
        private int nbrScore = 0;
        // Nombre vie restante
        private int nbrVie = 3;
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
                // Rect pour gérer colision
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
                AnimationExplosion(Canvas.GetLeft(y), Canvas.GetTop(y));
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
            // système d'animation
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)y.Tag == "explo1")
                {
                    y.Tag = "explo2";
                    y.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion1.png")));
                }
                else if ((string)y.Tag == "explo2" && compteur%10 == 0)
                {
                    y.Tag = "explo3";
                    y.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion2.png")));
                }
                else if ((string)y.Tag == "explo3" && compteur % 10 == 0)
                {
                    y.Tag = "exploFin";
                    y.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/Explosion3.png")));
                }
                else if ((string)y.Tag == "exploFin" && compteur % 10 == 0)
                {
                    animASuppr.Add(y);
                }
            }
        }
        public void AnimationExplosion(double x, double y)
        {
            Rectangle anim = new Rectangle
            {
                Tag = "explo1",
                Width = 45,
                Height = 45,
            };
            myCanvas.Children.Add(anim);
            Canvas.SetTop(anim, y);
            Canvas.SetLeft(anim, x);
        }
    }
}
