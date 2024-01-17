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
        public bool pleinEcran = false;
        public MenuDialog()
        {
            InitializeComponent();
        }

        private void Button_Click_Jouer(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_Quitter(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Arcade_Check(object sender, RoutedEventArgs e)
        {
            Dur.IsChecked = false;
            Normal.IsChecked = false;
            arcadeBool = true;
            normalBool = false;
            durBool = false;
            Console.WriteLine(arcadeBool +  " " + normalBool + " " + durBool);
        }

        private void Normal_Check(object sender, RoutedEventArgs e)
        {
            Dur.IsChecked = false;
            Arcade.IsChecked = false;
            arcadeBool = false;
            normalBool = true;
            durBool = false;
            Console.WriteLine(arcadeBool + " " + normalBool + " " + durBool);
        }

        private void Dur_Check(object sender, RoutedEventArgs e)
        {
            Arcade.IsChecked = false;
            Normal.IsChecked = false;
            arcadeBool = false;
            normalBool = false;
            durBool = true;
            Console.WriteLine(arcadeBool + " " + normalBool + " " + durBool);
        }

        private void PleinEcran_Check(object sender, RoutedEventArgs e)
        {
            pleinEcran = true;
        }
    }
}
