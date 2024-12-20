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
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));
            this.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./font/#pixelade");
        }

        private void PleinEcran_Check(object sender, RoutedEventArgs e)
        {
            pleinEcran = true;
        }

        private void quitterBouton_Click(object sender, RoutedEventArgs e)
        {
            quitter = true;
            Hide();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            pleinEcran = false;
        }
        private void quitterBouton_MouseEnter(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonClicker.png")));

        }

        private void quitterBouton_MouseLeave(object sender, MouseEventArgs e)
        {
            quitterBouton.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bouton/quitterBouton/quitterBoutonNormal.png")));

        }

    }
}
