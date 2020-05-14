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

        //Hey, here you need insert your namespace name before your "window" for access
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
                win1.Hide();
            else
                win1.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            win1.Owner = this;
            win2.Owner = this;
            win3.Owner = this;
            
        }

        private void Window_Button_MouseMove(object sender, MouseEventArgs e)
        {
            Button my_button = e.Source as Button;

            //description_Box1.Text = my_button.Content.ToString();

            if (Window1_Button.Content.ToString() == my_button.Content.ToString())
                description_Box1.Text = win1.description1;

            if (Window2_Button.Content.ToString() == my_button.Content.ToString())
                description_Box1.Text = win2.description2;

            if (Window3_Button.Content.ToString() == my_button.Content.ToString())
                description_Box1.Text = win3.description3;

        }

   
    }


}
