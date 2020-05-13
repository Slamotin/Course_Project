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
using System.Windows.Shapes;

//namespace WpfApp1
//{
//    /// <summary>
//    /// Логика взаимодействия для Window3.xaml
//    /// </summary>
//    public partial class Window3 : Window
//    {
//        public Window3()
//        {
//            InitializeComponent();
//            //test
//        }
//    }
//}

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        //Insert here your description 
        public string description3 = "";
        string[] arr = new string[10];
        Dictionary<int, TextBlock> myDict = new Dictionary<int, TextBlock>();
        Dictionary<int, TextBlock> myPointer = new Dictionary<int, TextBlock>();
        bool dictExist = false;
        int newNum;
        int newIdx;

        public Window3()
        {
            InitializeComponent();
        }

        void movePointer(int idx)
        {
            // стираем указатели
            for (int j = 0; j < myPointer.Count; j++)
                myPointer[j].Text = "";
            // двигаем указатель вправо от хеша и до конца массива
            myPointer[idx].Text = "^";
        }

        void checkClasterFromEnd(int idx)
        {
            for (int i = 9; i > idx; i--)
            {
                if (myDict[i].Text == "o")
                {
                    outputWindow.Text = "Маркеры очищены!";
                    myDict[i].Text = "x";
                    continue;
                }
                return;
            }
        }
        void checkEndOfClaster(int idx)
        {
            // проверяем есть ли слева маркеры, если есть, то их можно удалять
            if (idx > 0)
            {
                for (int i = idx - 1; i > -1; i--)
                {
                    if (myDict[i].Text == "o")
                    {
                        outputWindow.Text = "Маркеры очищены!";
                        myDict[i].Text = "x";
                        continue;
                    }
                    return;
                }
                // дошли до левого края массива и теперь начинаем обход правой части с конца 
                checkClasterFromEnd(idx);

            }
            else
            {
                checkClasterFromEnd(idx);
            }
        }

        async Task goRight(int idx, int num)
        {
            for (int j = idx; j < myDict.Count; j++)
            {
                movePointer(j);

                // проверяем пустая ли там ячейка и если да, то вписываем значение
                if ((myDict[j].Text == "x") || (myDict[j].Text == "o"))
                {
                    myDict[j].Text = num.ToString();
                    return;
                }
                await Task.Delay(1000);
            }
            // если дошли до конца массива и не нашли свободной ячейки, то просматриваем массив с начала
            for (int j = 0; j < idx; j++)
            {
                movePointer(j);

                // проверяем пустая ли там ячейка и если да, то вписываем значение
                if ((myDict[j].Text == "x") || (myDict[j].Text == "o"))
                {
                    myDict[j].Text = num.ToString();
                    return;
                }
                await Task.Delay(1000);
            }
            // если мы сюда попали, то значит у нас массив заполнен!
            myDict.Clear();
            dictExist = false;
            return;
        }

        async Task delRight(string delEl, int delIdx)
        {
            // идем вправо по кластеру и ищем удаляемый элемент
            for (int i = delIdx; i < 9; i++)
            {
                movePointer(i);

                if (myDict[i].Text == "x")
                {
                    outputWindow.Text = "Элемент не найден.";
                    return;
                }
                else if (myDict[i].Text == delEl)
                {
                    if (myDict[i + 1].Text == "x")
                    {
                        myDict[i].Text = "x";
                        outputWindow.Text = "Элемент удален. Конец кластера!";
                        checkEndOfClaster(i);
                    }
                    else
                    {
                        myDict[i].Text = "o";
                        outputWindow.Text = "Элемент удален. Ставим маркер!";
                    }
                    return;
                }

                await Task.Delay(1000);
            }
            // если ни один return до этого не сработал, то проверим последний эл-т
            movePointer(9);

            if (myDict[9].Text == "x")
            {
                outputWindow.Text = "Last element is not the one too";
                return;
            }
            else if (myDict[9].Text == delEl)
            {
                outputWindow.Text = "Element was founded at the last index";
                if (myDict[0].Text == "x")
                {
                    myDict[9].Text = "x";
                    checkEndOfClaster(9);
                }
                else
                {
                    myDict[9].Text = "o";
                }
                return;
            }
            // задержимся на последнем элементе для показухи
            await Task.Delay(1000);
            // проверили все элементы справа. Теперь надо проверить слева.
            for (int i = 0; i < delIdx; i++)
            {
                movePointer(i);

                if (myDict[i].Text == "x")
                {
                    outputWindow.Text = "There is no element you find";
                    return;
                }
                else if (myDict[i].Text == delEl)
                {
                    if (myDict[i + 1].Text == "x")
                    {
                        outputWindow.Text = "Нашел и удалил!";
                        myDict[i].Text = "x";
                        checkEndOfClaster(i);
                        return;
                    }
                    else
                    {
                        outputWindow.Text = "Удаляем и ставим маркер!";
                        myDict[i].Text = "o";
                        return;
                    }
                }

                await Task.Delay(1000);
            }

            // все проверили. Если дошли сюда, то такого элемента нет.
            outputWindow.Text = "Такого элемента не обнаружено";
            return;
        }

        async private void button1_Click(object sender, RoutedEventArgs e)
        {
            // отключим кнопку, чтобы на нее не тыкали часто
            button1.IsEnabled = button2.IsEnabled = false;
            // Если словарь еще не создан, то создаем его
            if (!dictExist)
            {
                myDict.Clear();
                myDict.Add(0, b0);
                myDict.Add(1, b1);
                myDict.Add(2, b2);
                myDict.Add(3, b3);
                myDict.Add(4, b4);
                myDict.Add(5, b5);
                myDict.Add(6, b6);
                myDict.Add(7, b7);
                myDict.Add(8, b8);
                myDict.Add(9, b9);
                myPointer.Clear();
                myPointer.Add(0, p0);
                myPointer.Add(1, p1);
                myPointer.Add(2, p2);
                myPointer.Add(3, p3);
                myPointer.Add(4, p4);
                myPointer.Add(5, p5);
                myPointer.Add(6, p6);
                myPointer.Add(7, p7);
                myPointer.Add(8, p8);
                myPointer.Add(9, p9);
                for (int i = 0; i < 10; i++)
                    myDict[i].Text = "x";
                dictExist = true;
            }

            // начинаем работу
            var rand = new Random();
            newNum = rand.Next(50);
            newIdx = newNum % 7;   // это наша хеш-функция
            // прописываем наше число и хеш в окошки
            insertElement.Text = newNum.ToString();
            insertHash.Text = newIdx.ToString();
            // очищаем каждый раз указатель
            for (int i = 0; i < 10; i++)
                myPointer[i].Text = "";
            if (myDict[newIdx].Text == "x")
            {
                myDict[newIdx].Text = newNum.ToString();
                myPointer[newIdx].Text = "^";
            }
            else
            {
                // идем вправо и ищем свободное место
                await goRight(newIdx, newNum);
            }
            button1.IsEnabled = button2.IsEnabled = true;
        }

        async private void button2_Click(object sender, RoutedEventArgs e)
        {
            string delEl = textBox1.Text;
            int delIdx = Int32.Parse(delEl) % 7;
            button1.IsEnabled = button2.IsEnabled = false;

            // если элемент находится по указанному индексу
            if (myDict[delIdx].Text == delEl)
            {
                movePointer(delIdx);

                // проверим не конец ли массива !!!!!!! тут поставил проверку < 9... Это под конкретный наш пример.!!!!!!!!!!
                if (delIdx < 9)
                {
                    // Удаляем элемент и заменяем его на один из символов
                    if (myDict[delIdx + 1].Text == "x")
                    {
                        outputWindow.Text = "Сразу удаляем!";
                        myDict[delIdx].Text = "x";
                        checkEndOfClaster(delIdx);
                    }
                    else
                    {
                        outputWindow.Text = "Сразу удаляем и ставим маркер!";
                        myDict[delIdx].Text = "o";
                    }
                }
                else
                {
                    // тут проверка последнего элемента
                    if (myDict[0].Text == "x")
                    {
                        myDict[delIdx].Text = "x";
                        checkEndOfClaster(delIdx);
                    }
                    else
                    {
                        myDict[delIdx].Text = "o";
                    }
                }
            }
            // ветка когда мы попали при проверке на другое число или на маркер "о"
            else
            {
                await delRight(delEl, delIdx);
            }

            button1.IsEnabled = button2.IsEnabled = true;
        }
    }
}