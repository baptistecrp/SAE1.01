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
    /// Logique d'interaction pour MortDialog.xaml
    /// </summary>
    public partial class MortDialog : Window
    {
        public bool quitter = false;
        public bool relancer = false;
        public MortDialog()
        {
            InitializeComponent();
            this.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./font/#pixelade");
            relancerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/relancerBouton/relancerBoutonNormal.png")));
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));
        }
        private void quitterBouton_Click(object sender, RoutedEventArgs e)
        {
            quitter = true;
            Hide();

        }
        private void relancerBouton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            this.relancer = true;
        }
        private void quitterBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonClicker.png")));

        }

        private void quitterBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));

        }
        private void relancerBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            relancerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/relancerBouton/relancerBoutonClicker.png")));
        }

        private void relancerBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            relancerBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/relancerBouton/relancerBoutonNormal.png")));
        }



    }
}
