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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Interactive_Sort.Window1 win1 = new Interactive_Sort.Window1();

        Window2 win2 = new Window2();
        Window3 win3 = new Window3();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window3_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window2_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window1_Button_Click(object sender, RoutedEventArgs e)
        {
            if (win1.IsVisible)
                win1.Close();
            else
                win1.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            win1.Owner = this;
            win2.Owner = this;
            win3.Owner = this;
            
        }
    }


}
