﻿using System;
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
        private double vitesseEnnemi = 2;
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
            // Creation nouvel ennemi
            Rectangle nouvelEnenemi = new Rectangle
            {
                Tag = "ennemi",
                Height = 45,
                Width = 45,
                Fill = new SolidColorBrush(System.Windows.Media.Colors.Red),
            };
            // Placement aléatoire
            Canvas.SetTop(nouvelEnenemi, 0);
            Canvas.SetLeft(nouvelEnenemi, random.Next(30, (int)Application.Current.MainWindow.Width-29));
            myCanvas.Children.Add(nouvelEnenemi);
            // Modification vitesse pour accélérer
            vitesseEnnemi += 0.1;
        }
        private void DeplacementEnnemi(Rectangle ennemi)
        {
            // Deplacement vers le bas
            Canvas.SetTop(ennemi, Canvas.GetTop(ennemi) + vitesseEnnemi);
        }
        private void Jeu(object sender, EventArgs e)
        {
            // Parcourt de tous les rectangles du canvas
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                // Si le rectangle est un ennemi
                if (y is Rectangle && (string)y.Tag == "ennemi")
                {
                    // Deplacement
                    DeplacementEnnemi(y);
                }
            }
        }
    }
}
