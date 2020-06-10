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
using System.Threading;

namespace WpfApp1
{
    

    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        int N_in;
        int n_public = 10; // значение для строительства по умолчанию
        bool PauseFlag = true;
        int count = 0;
        List<int> numbers = new List<int> { };

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
        public string description2 = "Оценка времени поиска в BST при разном количестве элементов дерева и отображением отношения количества вершин ко времени поиска";

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
        }
        public bool Prov_in_mass(int k)
        {
           for(int i=0; i< count; i++)
            {
                if (k == numbers[i]) // если такое число уже есть в числе посчитанных
                {
                    return true;
                }
            }
            return false;
        }

      
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
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();

        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {

            PauseFlag = true;
            N_in = Convert.ToInt32(textBox1.Text);
            button1.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            DoEvents();
            if (N_in < 0)
                MessageBox.Show("N не может быть отрицательным!");
            else
            {
                n_public = N_in;
                //проверка на то что элемент еще не посчитан
                if (Prov_in_mass(n_public) == false)//проверка на вхождение в массив
                {

                    tf.Add(new TFData(n_public));
                    listView1.ItemsSource = tf;
                    listView1.Items.Refresh();
                    numbers.Add(n_public);
                    count++;
                }
                else
                {
                    MessageBox.Show("Это значение уже посчитано!");
                }
          
            }
            button1.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //int n_public; 
            //int nMax = 10000001;

            /*string[] args = Environment.GetCommandLineArgs();
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
            */
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //N_in = (TextBox)sender;
            TextBox textBox = (TextBox)sender;
            N_in = Convert.ToInt32(textBox.Text);
            //tf.Add(new TFData(N_in));
            //tf.Clear();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            int k1=0; // счетчик на вывод предупреждения
            PauseFlag = true;
            button1.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            n_public = 10;
            int p = 1;
            for (int i = 100; i < 100001; i*=5)
            {
                if (PauseFlag == true)
                {

                    if (Prov_in_mass(i) == false)//проверка на вхождение в массив
                    {
                        tf.Add(new TFData(n_public * i));
                        listView1.ItemsSource = tf;
                        listView1.Items.Refresh();
                        numbers.Add(i * n_public);
                        count++;
                        progressBar1.Value = 100 * p++ / 5;
                        DoEvents();
                    }
                    else
                    {
                        if(k1==0)
                        {
                            MessageBox.Show("Результат для этих значений N уже посчитан!");
                            k1 = 1;
                        }
                    }
                }
                
                
            }


            listView1.ItemsSource = tf;
            listView1.Items.Refresh();
            button1.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            PauseFlag = false;
            button1.IsEnabled = false;
            progressBar1.Value = 0;
            DoEvents();
            e.Handled = true;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            tf.Clear();
            listView1.Items.Refresh();
            count = 0;
            numbers.Clear();
            PauseFlag = true;
        }

        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e) //проверка корректности ввода с клавиатуры
        {
            e.Handled = !(Char.IsNumber(e.Text, 0));//только цифры
        }
    }
       

}
