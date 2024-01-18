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
    /// Logique d'interaction pour OptionDialog.xaml
    /// </summary>
    public partial class OptionDialog : Window
    {
        public bool pleinEcran = false;
        public bool quitter = false;
        public OptionDialog()
        {
            InitializeComponent();
        }

        private void PleinEcran_Check(object sender, RoutedEventArgs e)
        {
            pleinEcran = true;
        }

        private void quitterBouton_Click(object sender, RoutedEventArgs e)
        {
            quitter = true;
            Close();
        }
    }
}
