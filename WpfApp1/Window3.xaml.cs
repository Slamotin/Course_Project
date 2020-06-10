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

//namespace HASH
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

namespace HASH
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        //Insert here your description 
        public string description3 = "Модуль демонстрации метода открытой адресации в хеш-таблице.";
        string[] arr = new string[11];
        Dictionary<int, TextBlock> myDict = new Dictionary<int, TextBlock>();
        Dictionary<int, TextBlock> myPointer = new Dictionary<int, TextBlock>();
        Dictionary<int, TextBlock> myIndex = new Dictionary<int, TextBlock>();
        
        SortedSet<int> indset = new SortedSet<int>();
        //SortedSet<int> indset2 = new SortedSet<int>();
        Queue<int> indset2= new Queue<int>();


        bool dictExist = false;
        int newNum;
        int newIdx;
        int busycount=0;
        int table_size=11;
        public Window3()
        {
            InitializeComponent();
//            indset2.Clear;
            
            myIndex.Clear();
            myIndex.Add(0, t0);
            myIndex.Add(1, t1);
            myIndex.Add(2, t2);
            myIndex.Add(3, t3);
            myIndex.Add(4, t4);
            myIndex.Add(5, t5);
            myIndex.Add(6, t6);
            myIndex.Add(7, t7);
            myIndex.Add(8, t8);
            myIndex.Add(9, t9);
            myIndex.Add(10, t10);
            //myIndex[0].Background= Brushes.;

        }
        private int Hash1(int val)
        {
            return val % table_size;
        }

        private int Hash2(int val)
        {
            return (val % 10)+1;
        }

        private void Clear_Table()
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
            myDict.Add(10, b10);
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
            myPointer.Add(10, p10);
            for (int i = 0; i < table_size; i++)
                myDict[i].Text = "x";
            dictExist = true;
            busycount = 0;
            button1.IsEnabled = true;
            
            return;
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
            for (int i = 10; i > idx; i--)
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
                    if (myDict[i].Text == "o"|| myDict[i].Text == "x")
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

        async Task goRight (int idx, int num)
        {
            switch (comboBox1.SelectionBoxItem.ToString())
            {
                case "LinearProbe":
                    {
                        for (int j = idx; j < myDict.Count; j++)
                        {
                            movePointer(j);

                            // проверяем пустая ли там ячейка и если да, то вписываем значение
                            if ((myDict[j].Text == "x") || (myDict[j].Text == "o"))
                            {
                                myDict[j].Text = num.ToString();
                                myPointer[j].Background = Brushes.LightGreen;
                                await Task.Delay(1000);
                                myPointer[j].Background = Brushes.White;

                                busycount += 1;
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
                                myPointer[j].Background = Brushes.LightGreen;
                                await Task.Delay(1000);
                                myPointer[j].Background = Brushes.White;
                                busycount += 1;
                                return;
                            }
                            await Task.Delay(1000);
                        }
                        // если мы сюда попали, то значит у нас массив заполнен!
                        //myDict.Clear();
                        //dictExist = false;
                        //return;
                        break;
                    }

                case "SquareProbe":
                    {
                        int ind = idx;
                        int j = 1;
                        while(true)
                        {
                            ind = Hash1((ind * (int)System.Math.Pow(j, 2))+j%2);
                            // проверяем пустая ли там ячейка и если да, то вписываем значение
                            if ((myDict[ind].Text == "x") || (myDict[ind].Text == "o"))
                            {
                                myDict[ind].Text = num.ToString();
                                myPointer[ind].Text = "^";
                                myPointer[ind].Background = Brushes.LightGreen;
                                await Task.Delay(1000);
                                myPointer[ind].Background = Brushes.White;
                                busycount += 1;
                                
                                return;
                            }
                            j++;
                        }
                        break;
                    }


                case "DoubleHash":
                    {
                        int ind = idx;
                        int j = 1;
                        while (true)
                        {
                            ind = (ind +j*(Hash2(ind)+j%2)) % myDict.Count;
                            // проверяем пустая ли там ячейка и если да, то вписываем значение
                            if ((myDict[ind].Text == "x") || (myDict[ind].Text == "o"))
                            {
                                myDict[ind].Text = num.ToString();
                                myPointer[ind].Text = "^";
                                myPointer[ind].Background = Brushes.LightGreen;
                                await Task.Delay(1000);
                                myPointer[ind].Background = Brushes.White;
                                busycount += 1;
                                
                                return;
                            }
                            j++;
                        }
                        break;
                    }
            }
            
        }

        async Task delRight(string delEl, int delIdx)
        {
            // идем вправо по кластеру и ищем удаляемый элемент
            for (int i = delIdx; i < 10; i++)
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
                    busycount -= 1;
                    
                    return;
                }

                await Task.Delay(1000);
            }
            // если ни один return до этого не сработал, то проверим последний эл-т
            movePointer(10);

            if (myDict[10].Text == "x")
            {
                outputWindow.Text = "Last element is not the one too";
                

                return;
            }
            else if (myDict[10].Text == delEl)
            {
                outputWindow.Text = "Element was founded at the last index";
                if (myDict[0].Text == "x")
                {
                    myDict[10].Text = "x";
                    checkEndOfClaster(10);
                    
                    
                }
                else
                {
                    myDict[10].Text = "o";
                    
                    

                }
                busycount -= 1;
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
                        
                    }
                    else
                    {
                        outputWindow.Text = "Удаляем и ставим маркер!";
                        myDict[i].Text = "o";
                        
                    }
                    busycount -= 1;

                    return;
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
                Clear_Table();
            }

            // начинаем работу
            var rand = new Random();
            newNum = rand.Next(50);
            //newIdx = newNum % table_size;   // это наша хеш-функция
            newIdx = Hash1(newNum);
            // прописываем наше число и хеш в окошки
            insertElement.Text = newNum.ToString();
            insertHash.Text = newIdx.ToString();
            // очищаем каждый раз указатель
            for (int i = 0; i < table_size; i++)
                myPointer[i].Text = "";
            if (myDict[newIdx].Text == "x")
            {
                myDict[newIdx].Text = newNum.ToString();
                myPointer[newIdx].Text = "^";
                myPointer[newIdx].Background = Brushes.LightGreen;
                await Task.Delay(1000);
                myPointer[newIdx].Background = Brushes.White;
                busycount += 1;
            }
            else
            {


                // идем вправо и ищем свободное место
                await goRight(newIdx, newNum);
            }
            //button1.IsEnabled = button2.IsEnabled = true;
            
            if (textBox1.Text.Length>0)
            {
                button2.IsEnabled = true;
            }
            if (busycount == table_size)
            {
                button1.IsEnabled = false;
                outputWindow.Text = "Таблица заполнена";
            }
            else
            {
                button1.IsEnabled = true;
            }
        }

        async private void button2_Click(object sender, RoutedEventArgs e)
        {
            string delEl = textBox1.Text;
            int delIdx = Hash1(Int32.Parse(delEl));
            button1.IsEnabled = button2.IsEnabled = false;

            switch (comboBox1.SelectionBoxItem.ToString())
            {
                case "LinearProbe":
                    {
                        // если элемент находится по указанному индексу
                        if (myDict[delIdx].Text == delEl)
                        {
                            movePointer(delIdx);

                            // проверим не конец ли массива !!!!!!! тут поставил проверку < table_size. 
                            if (delIdx < table_size-1)
                            {
                                // Удаляем элемент и заменяем его на один из символов
                                if (myDict[delIdx+1].Text == "x")
                                {
                                    outputWindow.Text = "Сразу удаляем!";
                                    myDict[delIdx].Text = "x";
                                    checkEndOfClaster(delIdx);
                                    busycount -= 1;
                                    button1.IsEnabled = true;
                                }
                                else
                                {
                                    outputWindow.Text = "Сразу удаляем и ставим маркер!";
                                    myDict[delIdx].Text = "o";
                                    busycount -= 1;
                                    button1.IsEnabled = true;
                                }
                            }
                            else
                            {
                                // тут проверка последнего элемента
                                if (myDict[0].Text == "x")
                                {
                                    myDict[delIdx].Text = "x";
                                    checkEndOfClaster(delIdx);
                                    busycount -= 1;
                                    button1.IsEnabled = true;
                                }
                                else
                                {
                                    myDict[delIdx].Text = "o";
                                    busycount -= 1;
                                    button1.IsEnabled = true;
                                }
                            }
                        }
                        // ветка когда мы попали при проверке на другое число или на маркер "о"
                        else
                        {
                            await delRight(delEl, delIdx);
                            
                        }
                        if (busycount < table_size)
                        {
                            button1.IsEnabled = true;
                        }
                        if (busycount == 0)
                        {
                            Clear_Table();
                        }
                        break;
                    }
                case "SquareProbe":
                    {
                        //считываем список значений с хешем таким же как и у удаляемого
                        if(myDict[delIdx].Text != "x")
                        {
                            for (int i = 0; i < table_size; ++i)
                            {
                                indset.Add(i);
                            }

                            int ind = delIdx;
                            int j = 1;
                            
                            while (true)
                            {
                                outputWindow.Text = "ind= " + ind.ToString();
                                await Task.Delay(1000);
                                if (indset.Contains(ind))
                                {
                                    
                                    if ((myDict[ind].Text != "x"))
                                    {
                                        if (Int32.Parse(myDict[ind].Text) % table_size == delIdx)
                                        {
                                            indset2.Enqueue(ind);
                                            

                                        }
                                        

                                        
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else if(indset.Count()==0)
                                {
                                    break;
                                }
                                
                                indset.Remove(ind);
                                outputWindow.Text = (string)indset2.Count().ToString();
                                await Task.Delay(1000);
                                j++;
                                ind = Hash1((ind * (int)System.Math.Pow(j, 2)) + j % 2);
                            }
                            outputWindow.Text = "j= "+j.ToString();
                            await Task.Delay(1000);

                            int index2,size2,next;
                            size2 = indset2.Count();
                            
                            for (int i = 0; i < indset2.Count(); ++i)
                            {
                                index2 = indset2.Dequeue();
                                outputWindow.Text = index2.ToString();
                                await Task.Delay(1000);
                                if (myDict[index2].Text== delEl)
                                {
                                    if (indset2.Count() != 0)
                                    {
                                        for (int z = 0; z < indset2.Count(); ++i)
                                        {
                                            next = indset2.Dequeue();
                                            myDict[index2].Text = myDict[next].Text;
                                            index2 = next;
                                            if (indset2.Count() == 0)
                                            {
                                                break;
                                            }
                                        }
                                        myDict[index2].Text = "x";
                                    }
                                    else
                                    {
                                        outputWindow.Text = "Сразу удаляем!";
                                        myDict[index2].Text = "x";
                                        busycount -= 1;
                                        button1.IsEnabled = true;
                                    }
                                    
                                    
                                }
                            }

                            button1.IsEnabled = true;
                        }
                        else
                        {
                            outputWindow.Text = "Элемент не найден.";

                            return;
                        }

                        break;
                    }
                case "DoubleHash":
                    {
                        break;
                    }
            }


           

            button2.IsEnabled = true;
            return;
            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Length>0)
            {
                button2.IsEnabled = true;
            }
            else
            {
                button2.IsEnabled = false;
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clear_Table();

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Clear_Table();
        }
    }
}