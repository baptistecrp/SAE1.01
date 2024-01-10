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
            CreationEnnemi();
        }
        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        { }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        { }
        private void CreationEnnemi()
        {
            Random random = new Random();
            Rectangle nouvelEnenemi = new Rectangle
            {
                Tag = "ennemi",
                Height = 45,
                Width = 45,
                Fill = new SolidColorBrush(System.Windows.Media.Colors.Red),
        };
            Canvas.SetTop(nouvelEnenemi, 0);
            Canvas.SetLeft(nouvelEnenemi, random.Next(30, (int)Application.Current.MainWindow.Width-29));
            myCanvas.Children.Add(nouvelEnenemi);
        }
        private void Jeu(object sender, EventArgs e)
        {
        }
    }
}
