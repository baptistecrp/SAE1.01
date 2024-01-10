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
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            // Configuration du timer
            dispatcherTimer.Tick += Jeu;
            // Rafraichissment chaque 16ms
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // Lancement timer
            dispatcherTimer.Start();
        }
        private void Canvas_KeyIsDown(object sender, KeyEventArgs e)
        { }
        private void Canvas_KeyIsUp(object sender, KeyEventArgs e)
        { }
        private void CreationEnnemi(int limit)
        { }
        private void Jeu(object sender, EventArgs e)
        { }
    }
}
