using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    

    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        int N_in;
        int n_public = 10; // значение для строительства по умолчанию



        class TFData
        {
            public double N { get; set; }
            public double Time1 { get; set; }
            public double Time_N { get; set; }

            public TFData(double arg)
            {
                N = arg;
                Time1 = BstBenchmark.ContainsBenchmark(Convert.ToInt32(arg));
                Time_N = Time1/N;
            }
        }
        List<TFData> tf = new List<TFData>();


        //Insert here your description 
        public string description2 = "";


        public Window2()
        {
            InitializeComponent();

            //BstBenchmark.ContainsBenchmark(15);

            textBox1.Focus();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            N_in = Convert.ToInt32(textBox1.Text);
            n_public = N_in;
            tf.Clear();

            for (int i = 0; i < n_public; i++)
            {
                tf.Add(new TFData(n_public*i));
            }
            listView1.ItemsSource = tf;
            listView1.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //int n_public; 
            int nMax = 10000001;

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                try
                {
                    int n0 = int.Parse(args[1]);
                    if (n0 > 2 || n0 > nMax)
                        throw new Exception();
                    else
                        n_public = n0;
                }
                catch
                {
                    string s = string.Format("Неверный параметр: {0}\n" + "Допустимые значения: от 2 до {1}", args[1], nMax);
                    MessageBox.Show(s, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }
            
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //N_in = (TextBox)sender;
            TextBox textBox = (TextBox)sender;
            N_in = Convert.ToInt32(textBox.Text);
            //tf.Add(new TFData(N_in));
            tf.Clear();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < 1001; i+=100)
            {
                tf.Add(new TFData(n_public*i));
            }

            listView1.ItemsSource = tf;
            listView1.Items.Refresh();

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Close(); // не так, потом исправим
        }
    }
       

}
