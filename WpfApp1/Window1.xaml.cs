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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>For determinate number of buttons and count generation rainbow colors </summary>
        static int ArraySize = 65;

        //index need for button0 work, need to rework start sort
        int index = 0;

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

            /// <summary> Constructor for work with method on canvas </summary>
            /// I save it for myself, do not delete them, now canvas1 transmit to CreateButtons() method 
            public ButtonRainbow(Canvas canv)
            {
                GenerateColoursOfRainbow(ArraySize);
                Random_Rainbow_Array = Non_RepeatingRandom(1, ArraySize - 1, ArraySize);
                //CreateButtons(canv);
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

                    //increment Hue from 0 to 320 (max 360) by some step (old version 64 steps from 5 to 320)
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
                // Create a random non-repeatable array for mixing rainbow
                Random_Rainbow_Array = Non_RepeatingRandom(1, ArraySize - 1, ArraySize);

                for (int i = 1; i <= ArraySize - 1; i++)
                {
                    // _rand.Next(64)

                    _buttons[i] = new Button();
                    _buttons[i].Width = (canvas1.ActualWidth - 80) / (ArraySize - 1);
                    _buttons[i].Height = 270;
                    _buttons[i].BorderThickness = new Thickness(0);
                    //_buttons[i].;
                    Canvas.SetTop(_buttons[i], 58);
                    Canvas.SetLeft(_buttons[i], 40 + (_buttons[i].Width * (i - 1)));

                    SetRandomRGBColorForButton(i, Random_Rainbow_Array);
                    canvas1.Children.Add(_buttons[i]);

                    /*_labels0[i] = new Label();
                    _labels0[i].Width = 20;
                    _labels0[i].Height = 30;
                    _labels0[i].FontSize = 7;
                    _labels0[i].Content = Random_Rainbow_Array[i];
                    Canvas.SetTop(_labels0[i], 298);
                    Canvas.SetLeft(_labels0[i], 40 + (20 * (i - 1)) - 1);
                    canvas1.Children.Add(_labels0[i]);

                    _labels1[i] = new Label();
                    _labels1[i].Width = 20;
                    _labels1[i].Height = 30;
                    _labels1[i].FontSize = 7;
                    _labels1[i].Content = clrsOfRnbw64[Random_Rainbow_Array[i]].r;
                    Canvas.SetTop(_labels1[i], 328);
                    Canvas.SetLeft(_labels1[i], 40 + (20 * (i - 1)) - 1);
                    canvas1.Children.Add(_labels1[i]);

                    _labels2[i] = new Label();
                    _labels2[i].Width = 20;
                    _labels2[i].Height = 30;
                    _labels2[i].FontSize = 7;
                    _labels2[i].Content = clrsOfRnbw64[Random_Rainbow_Array[i]].g;
                    Canvas.SetTop(_labels2[i], 358);
                    Canvas.SetLeft(_labels2[i], 40 + (20 * (i - 1)) - 1);
                    canvas1.Children.Add(_labels2[i]);

                    _labels3[i] = new Label();
                    _labels3[i].Width = 20;
                    _labels3[i].Height = 30;
                    _labels3[i].FontSize = 6;
                    _labels3[i].Content = clrsOfRnbw64[Random_Rainbow_Array[i]].b;
                    Canvas.SetTop(_labels3[i], 388);
                    Canvas.SetLeft(_labels3[i], 40 + (20 * (i - 1)) - 1);
                    canvas1.Children.Add(_labels3[i]);*/

                    await Task.Delay(20);


                }
                return;
            }

        }

        public async void InsertSort()
        {
            //Object of ButtonRaindbow class
            ButtonRainbow RnbwBtn = new ButtonRainbow();
            int[] Temp_Array = RnbwBtn.Random_Rainbow_Array;

            for (int i = 1; i < Temp_Array.Length; i++)
            {
                //Label label5 = new Label();
                string b = (string)label5.Content;
                label5.Content = b + " " + Temp_Array[i];
            }
            for (int i = 1; i < Temp_Array.Length; i++)
            {
                label1.Content = i;
                int j = 0;
                //string a = (string)label4.Content;
                //label4.Content = a + " " + Temp_Array[i];
                for (j = i; j >= 2 &&
                    (Temp_Array[j - 1] > Temp_Array[j]); j--)
                {
                    int tmp = Temp_Array[j - 1];
                    Temp_Array[j - 1] = Temp_Array[j];
                    Temp_Array[j] = tmp;

                    RnbwBtn.SetRGBColorForButton(j, Temp_Array);
                    RnbwBtn.SetRGBColorForButton(j - 1, Temp_Array);

                    label3.Content = Int32.Parse(label3.Content.ToString()) + 1;

                    //Animation for Labels

                    /*Canvas.SetLeft(_buttons[j], 40 + (20 * (j-1)));
                    Canvas.SetLeft(_buttons[j - 1], 40 + (20 * (j)));
                    canvas1.Children.Remove(_buttons[i + 6]);
                    canvas1.Children.Insert(i+6,_buttons[i]);

                    Canvas.SetLeft(_labels0[j], 40 + (20 * (j - 1)));
                    Canvas.SetLeft(_labels0[j - 1], 40 + (20 * (j)));

                    Canvas.SetLeft(_labels1[j], 40 + (20 * (j - 1)));
                    Canvas.SetLeft(_labels1[j - 1], 40 + (20 * (j)));

                    Canvas.SetLeft(_labels2[j], 40 + (20 * (j - 1)));
                    Canvas.SetLeft(_labels2[j - 1], 40 + (20 * (j)));

                    Canvas.SetLeft(_labels3[j], 40 + (20 * (j - 1)));
                    Canvas.SetLeft(_labels3[j - 1], 40 + (20 * (j)));*/

                    /*Canvas.SetLeft(_buttons[i], 40 + (20 * (i + 1)));
                    Canvas.SetLeft(_labels0[i], 40 + (20 * (i + 1)));
                    Canvas.SetLeft(_labels1[i], 40 + (20 * (i + 1)));
                    Canvas.SetLeft(_labels2[i], 40 + (20 * (i + 1)));
                    Canvas.SetLeft(_labels3[i], 40 + (20 * (i + 1)));
                    */

                    //Delay between iterations
                    await Task.Delay(1);
                }

            }

        }

        public Window1()
        {
            InitializeComponent();
            //don't need in non main window?
            //InitializeComponent(); /
            //ButtonRainbow BtnRnbw = new ButtonRainbow(canvas1);

        }



        //Start to create buttons, if buttons was created, start sort (need to rework on 2 and more sort alg)
        private async void Button0_Click(object sender, RoutedEventArgs e)
        {
            //label5.Content += " " + comboBox1.SelectionBoxItem.ToString();
            //Object of ButtonRaindbow class
            ButtonRainbow RnbwBtn = new ButtonRainbow();
            if (index == 1)
            {
                switch (comboBox1.SelectionBoxItem.ToString())
                {
                    case "InsertSort":
                        InsertSort();
                        break;
                }
            }
            else
            {
                await RnbwBtn.CreateButtons(canvas1);
                //ChangeColor();
            }
            index = 1;
            e.Handled = true;
            //CreateButtons();

            //InsertSort();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void Canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonRainbow temp_obj = new ButtonRainbow();
            if (temp_obj.GetButtons(ArraySize - 1) != null)
            {
                for (int i = 1; i <= ArraySize - 1; i++)
                {
                    Button a = temp_obj.GetButtons(i);
                    //canvas1.Children.Remove(a);
                    a.Width = (canvas1.ActualWidth - 80) / (ArraySize - 1);
                    Canvas.SetLeft(a, 40 + (a.Width * (i - 1)));
                    //canvas1.Children.Add(a);
                }
            }

        }

        /* public async Task ChangeColor()
         {
             for (int i = 1; i <= 64; i += 1)
             {

                 button0.Foreground = new SolidColorBrush(
                     System.Windows.Media.Color.FromArgb(
                         255,
                     (byte)clrsOfRnbw64[i].r,
                     (byte)clrsOfRnbw64[i].g,
                     (byte)clrsOfRnbw64[i].b));
                 //Canvas.

                 await Task.Delay(100);
                 //button1.Foreground = new SolidColorBrush(Color.FromRgb(2, 2, 2));
             }

         }*/


    }
}
