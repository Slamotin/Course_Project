using System;
//using System.Drawing;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace Interactive_Sort
{
    
    public partial class Window1 : Window
    {
        //description of your program
        public string description1 = "Представлены 3 алгоритма сортировки: сортировка вставками, сортировка пузырьком и шейкерная сортировка";

        /// <summary>For determinate number of buttons and count generation rainbow colors </summary>
        static int ArraySize;

        int Sorting_Delay;
        bool Skipflag = false;
        bool PauseFlag = false;

        ///<summary>This struct contains colors of rainbow (from red to violet) with some step</summary>
        public struct ClrsOfRnbw64
        {
            public int r, g, b;

            public ClrsOfRnbw64(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

        }

        /// <summary> Class of methods, creating buttons and work with their colors </summary>
        internal class ButtonRainbow : Window1
        {
            /// <summary> Serial struct colours of rainbow </summary>
            private static ClrsOfRnbw64[] clrsOfRnbw64 = new ClrsOfRnbw64[ArraySize];
            /// <summary> Array of random 1..n non repeateble numbers n==ArraySize </summary>
            public int[] Random_Rainbow_Array = new int[ArraySize];

            //Button array for rainbow drawing
            public static Button[] _buttons = new Button[ArraySize];

            //Labels arrays, i used this labels for debugging, need to deleted and rework for counting of steps
            private static Label[] _labels0 = new Label[ArraySize];
            private static Label[] _labels1 = new Label[ArraySize];
            private static Label[] _labels2 = new Label[ArraySize];
            private static Label[] _labels3 = new Label[ArraySize];

            //No comments
            static Random _rand = new Random();

            //Constructor
            public ButtonRainbow()
            {
                GenerateColoursOfRainbow(ArraySize);
                Random_Rainbow_Array = Non_RepeatingRandom(1, ArraySize - 1, ArraySize);
            }

            /// <summary> Constructor for new size</summary>
            public ButtonRainbow(int NewArraySize)
            {
                ArraySize = NewArraySize;
                clrsOfRnbw64 = new ClrsOfRnbw64[NewArraySize];
                Random_Rainbow_Array = new int[NewArraySize];
                Random_Array = new int[NewArraySize];
                GenerateColoursOfRainbow(NewArraySize);
                Random_Rainbow_Array = Non_RepeatingRandom(1, NewArraySize - 1, NewArraySize);
            }

            /// <summary> Return button object with determine button </summary>
            public Button GetButtons(int NumberOfButton)
            {
                return _buttons[NumberOfButton];
            }

            /// <summary>Generate rainbow </summary>
            private static void GenerateColoursOfRainbow(int ArraySize)
            {
                int r = 0;
                int g = 0;
                int b = 0;
                double h = 0; //Hue
                int S = 100; //Saturation (const)
                int V = 100; //Value (const)

                //Because 360 it is again red color, 320 have violet color, 330 have pink color 
                double step = 320 / ((double)ArraySize - 1);

                //Filling array by colors of rainbow in cycle
                for (int i = 1; (i <= ArraySize - 1) && (h <= 360); i++)
                {
                    Color temp_rainbow_struct = HsvToRgb(h, S, V);
                    r = temp_rainbow_struct.R;
                    g = temp_rainbow_struct.G;
                    b = temp_rainbow_struct.B;
                    clrsOfRnbw64[i] = new ClrsOfRnbw64(r, g, b);

                    //increment Hue from 0 to 320 (max 360) by some step
                    h += step;
                }
            }

            ///<summary> Create an array of non-repeatable numbers</summary>
            public static int[] Non_RepeatingRandom(int min_num, int max_num, int SizeOfSourceArray)
            {
                int[] result_array = new int[SizeOfSourceArray];
                int temp_x;
                int i = 1;
                int[] hashArray = new int[SizeOfSourceArray];

                do
                {
                    temp_x = _rand.Next(min_num, max_num + 1);

                    if (hashArray[temp_x] != temp_x)
                    {
                        result_array[i] = temp_x;
                        i++;
                        hashArray[temp_x] = temp_x;
                        if (temp_x == max_num) { max_num -= 1; }
                        if (temp_x == min_num) { min_num += 1; }
                    }

                } while (i < SizeOfSourceArray);
                return result_array;
            }

            /// <summary> Return RGB color in "System.Windows.Media.Color" struct </summary>
            private static Color GetRgb(double r, double g, double b)
            {
                return Color.FromArgb(
                    255,
                    (byte)(r * (255.0 / 100)),
                    (byte)(g * (255.0 / 100)),
                    (byte)(b * (255.0 / 100))
                    );
            }

            /// <summary>
            /// Set Random RGB Color For Button
            /// </summary>
            /// <param name="ButtonNumber">NumberOfButton</param>
            public void SetRandomRGBColorForButton(int ButtonNumber, int[] Random_Rainbow_Array)
            {
                _buttons[ButtonNumber].Background = new SolidColorBrush(
                        System.Windows.Media.Color.FromArgb(
                            255,
                            (byte)clrsOfRnbw64[Random_Rainbow_Array[ButtonNumber]].r,
                            (byte)clrsOfRnbw64[Random_Rainbow_Array[ButtonNumber]].g,
                            (byte)clrsOfRnbw64[Random_Rainbow_Array[ButtonNumber]].b));
            }

            /// <summary>
            /// Set RGB Color For Button
            /// </summary>
            /// <param name="ButtonNumber">NumberOfButton</param>
            /// <param name="SortableArray">SortableArray</param>
            public void SetRGBColorForButton(int ButtonNumber, int[] SortableArray)
            {
                _buttons[ButtonNumber].Background = new SolidColorBrush(
                        System.Windows.Media.Color.FromArgb(
                            255,
                            (byte)clrsOfRnbw64[SortableArray[ButtonNumber]].r,
                            (byte)clrsOfRnbw64[SortableArray[ButtonNumber]].g,
                            (byte)clrsOfRnbw64[SortableArray[ButtonNumber]].b));
            }

            /// <summary> Change HSV colors to RGB colors </summary>
            public static Color HsvToRgb(double h, double s, double v)
            {
                int Hi = (int)Math.Floor(h / 60.0) % 6;
                double f = (h / 60.0) - Math.Floor(h / 60.0);

                double Vmin = ((100 - s) * v) / 100;
                double a = (v - Vmin) * ((h % 60) / 60);
                double Vinc = Vmin + a;
                double Vdec = v - a;

                Color ret;

                switch (Hi)
                {
                    case 0:
                        ret = GetRgb(v, Vinc, Vmin);
                        break;
                    case 1:
                        ret = GetRgb(Vdec, v, Vmin);
                        break;
                    case 2:
                        ret = GetRgb(Vmin, v, Vinc);
                        break;
                    case 3:
                        ret = GetRgb(Vmin, Vdec, v);
                        break;
                    case 4:
                        ret = GetRgb(Vinc, Vmin, v);
                        break;
                    case 5:
                        ret = GetRgb(v, Vmin, Vdec);
                        break;
                    default:
                        ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                        break;
                }
                return ret;
            }

            /// <summary>
            /// Create Buttons with random colors
            /// </summary>
            /// <param name="canvas1"></param>
            /// <returns></returns>
            public async Task CreateButtons(Canvas canvas1)
            {
                for (int i = 1; i <= _buttons.Length - 1; i++)
                    canvas1.Children.Remove(_buttons[i]);

                _buttons = new Button[ArraySize];
                for (int i = 1; i <= ArraySize - 1; i++)
                {
                    _buttons[i] = new Button();
                    _buttons[i].Width = (canvas1.ActualWidth - 80) / (ArraySize - 1);
                    _buttons[i].Height = (canvas1.ActualHeight - 200);
                    _buttons[i].MinHeight = 10;
                    _buttons[i].BorderThickness = new Thickness(0);
                    Canvas.SetTop(_buttons[i], 58);
                    Canvas.SetLeft(_buttons[i], 40 + (_buttons[i].Width * (i - 1)));

                    SetRandomRGBColorForButton(i, Random_Rainbow_Array);
                    canvas1.Children.Add(_buttons[i]);

                    await Task.Delay(1); //Delay Creating Buttons
                }
                return;
            }
        }

        //second array for sorting
        public int[] Random_Array;

        //обработчик кнопки build random rainbow
        private async void Button0_Click(object sender, RoutedEventArgs e)
        {
            button0.IsEnabled = false;
            PauseFlag = false;
            button1.IsEnabled = true;
            button1.Content = "Start";

            ButtonRainbow RnbwBtn = new ButtonRainbow(ArraySize);
            Random_Array = RnbwBtn.Random_Rainbow_Array;
            await RnbwBtn.CreateButtons(canvas1);

            button1.IsEnabled = true;
            
            e.Handled = true;
        }

        //обработчик кнопки start
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreatingSize.IsEnabled = false;
            comboBox1.IsEnabled = false;
            SkipButton.IsEnabled = true;
            if (button1.Content.ToString() == "Pause")
            {
                PauseFlag = true;
                button1.Content = "Start";
            }
            else if (button1.Content.ToString() == "Start" & PauseFlag == true)
            {
                PauseFlag = false;
                button1.Content = "Pause";
            }
            else
            {
                label1.Content = "0";
                label3.Content = "0";
                switch (comboBox1.SelectionBoxItem.ToString())
                {
                    case "InsertSort":
                        InsertSort();
                        button1.Content = "Pause";
                        break;
                    case "BubbleSort":
                        BubbleSort();
                        button1.Content = "Pause";
                        break;
                    case "ShakerSort":
                        ShakerSort();
                        button1.Content = "Pause";
                        break;
                }
                e.Handled = true;
            }
        }

        //обработчки кнопки skip
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (!Skipflag) Skipflag = true;
            else Skipflag = false;
            button1.IsEnabled = false;
        }

        //Insert sort
        public async void InsertSort()
        {
            //Object of ButtonRaindbow class
            ButtonRainbow RnbwBtn = new ButtonRainbow(ArraySize);
            int[] Temp_Array = Random_Array;
            
            for (int i = 1; i < Temp_Array.Length; i++)
            {
                SkipButton.IsEnabled = true;
                label1.Content = i;
                int j = 0;

                for (j = i; j >= 2 &&
                    (Temp_Array[j - 1] > Temp_Array[j]); j--)
                {
                    int tmp = Temp_Array[j - 1];
                    Temp_Array[j - 1] = Temp_Array[j];
                    Temp_Array[j] = tmp;

                    RnbwBtn.SetRGBColorForButton(j, Temp_Array);
                    RnbwBtn.SetRGBColorForButton(j - 1, Temp_Array);

                    label3.Content = Int32.Parse(label3.Content.ToString()) + 1;

                    //Delay between iterations
                    do
                    {
                        if (!Skipflag) await Task.Delay(Sorting_Delay);
                        if (PauseFlag)
                        {
                            await Task.Delay(1);
                            SkipButton.IsEnabled = false; 
                        }
                    } while (PauseFlag);
                    
                }

            }
            button0.IsEnabled = true;
            button1.IsEnabled = false;
            comboBox1.IsEnabled = true;
            CreatingSize.IsEnabled = true;
            Skipflag = false;
            SkipButton.IsEnabled = false;
        }

        //сортировка пузырьком
        public async void BubbleSort()
        {
            ButtonRainbow rainbow = new ButtonRainbow(ArraySize);
            int[] arr = Random_Array;
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                SkipButton.IsEnabled = true;
                label1.Content = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;

                        rainbow.SetRGBColorForButton(j, arr);
                        rainbow.SetRGBColorForButton(i, arr);

                        label3.Content = Int32.Parse(label3.Content.ToString()) + 1;
                        do
                        {
                            if (!Skipflag) await Task.Delay(Sorting_Delay);
                            if (PauseFlag)
                            {
                                await Task.Delay(1);
                                SkipButton.IsEnabled = false;
                            }
                        } while (PauseFlag);
                    }
                }
            }
            button0.IsEnabled = true;
            button1.IsEnabled = false;
            comboBox1.IsEnabled = true;
            CreatingSize.IsEnabled = true;
            Skipflag = false;
            SkipButton.IsEnabled = false;
        }

        //шейкерная сортировка
        public async void ShakerSort()
        {
            SkipButton.IsEnabled = true;
            ButtonRainbow rainbow = new ButtonRainbow(ArraySize);
            int[] array = Random_Array;
            int temp;
            for (var i = 0; i < array.Length / 2; i++)
            {
                SkipButton.IsEnabled = true;
                label1.Content = i;
                var swapFlag = false;
                
                for (var j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        swapFlag = true;

                        rainbow.SetRGBColorForButton(j + 1, array);
                        rainbow.SetRGBColorForButton(j, array);

                        label3.Content = Int32.Parse(label3.Content.ToString()) + 1;
                        do
                        {
                            if (!Skipflag) await Task.Delay(Sorting_Delay);
                            if (PauseFlag)
                            {
                                await Task.Delay(1);
                                SkipButton.IsEnabled = false;
                            }
                        } while (PauseFlag);
                    }
                }
                
                for (var j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        temp = array[j-1];
                        array[j - 1] = array[j];
                        array[j] = temp;

                        swapFlag = true;

                        rainbow.SetRGBColorForButton(j, array);
                        rainbow.SetRGBColorForButton(j - 1, array);

                        label3.Content = Int32.Parse(label3.Content.ToString()) + 1;
                        do
                        {
                            if (!Skipflag) await Task.Delay(Sorting_Delay);
                            if (PauseFlag)
                            {
                                await Task.Delay(1);
                                SkipButton.IsEnabled = false;
                            }
                        } while (PauseFlag);
                    }
                }
                
                if (!swapFlag)
                {
                    break;
                }
                do
                {
                    if (!Skipflag) await Task.Delay(Sorting_Delay);
                    if (PauseFlag)
                    {
                        await Task.Delay(1);
                        SkipButton.IsEnabled = false;
                    }
                } while (PauseFlag);
            }
            CreatingSize.IsEnabled = true;
            comboBox1.IsEnabled = true;
            button0.IsEnabled = true;
            button1.IsEnabled = false;
            Skipflag = false;
            SkipButton.IsEnabled = false;
        }

        public Window1()
        {
            InitializeComponent();
            ComboBox1_SelectionChanged(null, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void Canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (canvas1.ActualHeight - 115 > 125)
                Canvas.SetTop(ButtonGrid, canvas1.ActualHeight - 110);
            ButtonRainbow temp_obj = new ButtonRainbow(ArraySize);
            if (temp_obj.GetButtons(ArraySize - 1) != null)
            {
                for (int i = 1; i <= ArraySize - 1; i++)
                {
                    Button a = temp_obj.GetButtons(i);
                    canvas1.Children.Remove(a);
                    if (canvas1.ActualHeight - 200 > 0)
                        a.Height = (canvas1.ActualHeight - 200);

                    a.Width = (canvas1.ActualWidth - 80) / (ArraySize - 1);
                    Canvas.SetLeft(a, 40 + (a.Width * (i - 1)));
                    
                    canvas1.Children.Add(a);
                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void SortingDelayText_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            int.TryParse(SortingDelayText.Text, out number);
            if (number >= 1)
            {
                Sorting_Delay = number;
            }      

        }

        private void CreatingSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(CreatingSize.Text, out ArraySize);
            if (ArraySize > 2)
            { 
                button1.IsEnabled = false;
                button0.IsEnabled = true;
            }         
            else button0.IsEnabled = false;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Descriptioning_sort != null)
            {
                switch (comboBox1.Text)
                {
                    case "ShakerSort":
                        Descriptioning_sort.Text = "Worst О(n\x00B2); Medium О(n\x00B2)";
                        break;

                    case "InsertSort":
                        Descriptioning_sort.Text = "Worst О(n\x00B2); Medium О(n\x00B2)";
                        break;

                    case "BubbleSort": 
                        Descriptioning_sort.Text = "Worst О(n\x00B2); Medium О(n\x00B2) ";
                        break;

                }
            }
        }
    }
}
