using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Arkanoid.components
{
    public partial class EndGameDialog : Window
    {
        public static DependencyProperty TextProperty;
        public static DependencyProperty DialogBackgroundProperty;

        static EndGameDialog()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EndGameDialog));
            DialogBackgroundProperty = DependencyProperty.Register("DialogBackground", typeof(Brush), typeof(EndGameDialog));
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public Brush DialogBackground
        {
            get => (Brush)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public EndGameDialog()
        {
            InitializeComponent();
        }

        private void StartOverClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
        private void MainMenuClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void root_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}