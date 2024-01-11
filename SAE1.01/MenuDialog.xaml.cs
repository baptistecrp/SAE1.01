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
    /// Logique d'interaction pour MenuDialog.xaml
    /// </summary>
    public partial class MenuDialog : Window
    {
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
    }
}