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
using System.Windows.Shapes;

namespace SAE1._01
{
    /// <summary>
    /// Logique d'interaction pour MenuDialog.xaml
    /// </summary>
    public partial class MenuDialog : Window
    {
        public bool arcadeBool = false;
        public bool normalBool = false;
        public bool durBool = false;
        public bool option = false;
        public MenuDialog()
        {
            InitializeComponent();
            jouerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/jouerBouton/jouerBoutonNormal.png")));
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));
            modeArcadeBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeArcadeBouton/modeArcadeBoutonNormal.png")));
            modeNormalBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeNormalBouton/modeNormalBoutonNormal.png")));
            modeDifficileBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeDifficileBouton/modeDifficileBoutonNormal.png")));
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonNormal.png")));

        }

        private void Button_Click_Jouer(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private void jouerBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            jouerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/jouerBouton/jouerBoutonClicker.png")));

        }

        private void jouerBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            jouerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/jouerBouton/jouerBoutonNormal.png")));

        }

        private void quitterBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonClicker.png")));

        }

        private void quitterBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));

        }

        private void modeDifficileBouton_Click(object sender, RoutedEventArgs e)
        {
            modeArcadeBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeArcadeBouton/modeArcadeBoutonNormal.png")));
            modeNormalBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeNormalBouton/modeNormalBoutonNormal.png")));
            modeDifficileBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeDifficileBouton/modeDifficileBoutonClicker.png")));
            arcadeBool = false;
            normalBool = false;
            durBool = true;
            Console.WriteLine(arcadeBool + " " + normalBool + " " + durBool);
        }

        private void modeNormalBouton_Click(object sender, RoutedEventArgs e)
        {
            modeArcadeBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeArcadeBouton/modeArcadeBoutonNormal.png")));
            modeNormalBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeNormalBouton/modeNormalBoutonClicker.png")));
            modeDifficileBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeDifficileBouton/modeDifficileBoutonNormal.png")));
            arcadeBool = false;
            normalBool = true;
            durBool = false;
            Console.WriteLine(arcadeBool + " " + normalBool + " " + durBool);
        }

        private void modeArcadeBouton_Click(object sender, RoutedEventArgs e)
        {
            modeArcadeBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeArcadeBouton/modeArcadeBoutonClicker.png")));
            modeNormalBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeNormalBouton/modeNormalBoutonNormal.png")));
            modeDifficileBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/modeDifficileBouton/modeDifficileBoutonNormal.png")));
            arcadeBool = true; ;
            normalBool = false;
            durBool = false;
            Console.WriteLine(arcadeBool + " " + normalBool + " " + durBool);
        }

        private void optionBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonClicker.png")));
        }

        private void optionBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonNormal.png")));
        }

        private void optionBouton_Click(object sender, RoutedEventArgs e)
        {
            option = true;
            Close();
        }
    }
}
