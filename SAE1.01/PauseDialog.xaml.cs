using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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
    /// Logique d'interaction pour PauseDialog.xaml
    /// </summary>
    public partial class PauseDialog : Window
    {
        public bool relancer = false;
        public bool quitter = false;
        public bool reprendre = false;
        public bool option = false;
        public PauseDialog()
        {
            InitializeComponent();
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonNormal.png")));
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));
        }

        private void relancerBouton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            this.relancer = true;
        }

        private void quitterBouton_Click(object sender, RoutedEventArgs e)
        {
            this.quitter = true;
            Close();
        }

        private void reprendreBouton_Click(object sender, RoutedEventArgs e)
        {
            this.reprendre = true;
            Close();
        }

        private void optionsBouton_Click(object sender, RoutedEventArgs e)
        {
            this.option = true;
            Close();
        }
        private void quitterBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonClicker.png")));

        }

        private void quitterBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));

        }
        private void optionBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonClicker.png")));
        }

        private void optionBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            optionBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/optionBouton/optionBoutonNormal.png")));
        }
    }
}
