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
    }
}
