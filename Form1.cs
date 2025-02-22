using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYDelivery
{
    public partial class Form1 : Form
    {
        static public string _filePath = "C:\\deliveryOrder\\";//В кавычках укажите польный путь куда можно сохранить файл
        
        static public string _nameFile = "deliveryOrder.txt"; // Имя файла куда сохраняется информации о заказах
        static public string _deliveryLog = @""+_filePath+""+_nameFile ;
        public Form1()
        {
            InitializeComponent();
            
            FileInfo order = new FileInfo(_deliveryLog);
            if (order.Exists)//проверяем наличие файла для записи инф-ии о заказах
            {
                button2.Hide();//Если файл найден, из формы убираем след. элементы управление
                label2.Hide();
                label3.Hide();
                label4.Hide();
                label5.Hide();
                label6.Hide();
                label7.Hide();
                label8.Hide();

            }
            else
            {                
                button1.Hide();//Если файл не найден, из формы убираем след. элементы управление
                button3.Hide();
                button4.Hide();
                label1.Hide();
                textBox1.Hide();
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter name = new StreamWriter(_deliveryLog, false))
            {
                name.WriteLine($"|Номер заказа\t|Район дос-ки\t|Вес заказа\t|Дата дос-ки\t|Время дос-ки".ToUpperInvariant());
            }
            //_deliveryLog = @textBox2.Text + _myFilev;
            MessageBox.Show("Файл создан \n Путь к файл: "+_deliveryLog);
            button2.Hide();//После создание файла убираем/возвращаем след элементы управление
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            button1.Show();
            button3.Show();
            button4.Show();
            listBox1.Show();
            label1.Show();
            textBox1.Show();
        }
        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (StreamReader name = new StreamReader(_deliveryLog))
            {
                listBox1.Text = await name.ReadToEndAsync();
            }
        }
        public async void button3_Click(object sender, EventArgs e) //открыть список заказов
        {
            listBox1.Items.Clear();
            int d = -1;
            using (StreamReader prob = new StreamReader(_deliveryLog))
            {
                string line;
                while ((line = await prob.ReadLineAsync()) != null)
                {
                    listBox1.Items.Add(line);
                    d++; 
                }
                textBox1.Text = Convert.ToString(d);
            }
        }
        private async void button4_Click(object sender, EventArgs e)// сортировка заказов
        {
            listBox1.Items.Clear();
            Form2 newForm = new Form2();

            
            using (StreamReader myString = new StreamReader(_deliveryLog)) // 1-я строка открыватся без сортировки
            {
                string myLine = await myString.ReadLineAsync();
                int a = 1;
                while (a < 2)
                {
                    listBox1.Items.Add(myLine);
                    a++;
                }
            }
                for (int i = 0; i < DeliveryService._listCity.Length; i++)
                {
                    using (StreamReader prob = new StreamReader(_deliveryLog))
                    {
                        string line;
                        while ((line = prob.ReadLine()) != null)
                        {
                            string sb = line;
                            Regex prof = new Regex((DeliveryService._listCity[i] + @"\b*"));
                            MatchCollection mate = prof.Matches(line);
                            if (mate.Count > 0)
                            {
                                foreach (Match m in mate)
                                {
                                    listBox1.Items.Add(line);
                                }
                            }
                        }
                    }
                }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    class DeliveryService
    {
        int OrderNumber;
        private double orderWeight;
        string OrderCity { get; set; }
        DateTimePicker DeliveryDate;
        string DeliveryTime;
        static public string[] _listCity = { "Аэропорт", "Войковская", "Кунцево   ", "Молодежная", "Сокол     " };
        static public string[] _listTime = { "06:00 - 08:00", "08:00 - 10:00", "10:00 - 12:00", "12:00 - 14:00", "14:00 - 16:00", "16:00 - 18:00", "18:00 - 20:00" };
        double OrderWeight;
        
        public void CreateOrder(int _oredrNumber, string _orderCity, double _orderWeight, DateTimePicker _deliveryDate, string _deliveryTime)
        {
            OrderNumber = _oredrNumber;
            OrderCity = _orderCity;
            OrderWeight = _orderWeight;
            DeliveryDate = _deliveryDate;
            DeliveryTime = _deliveryTime;
        }
    }
}
