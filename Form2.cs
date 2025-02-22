using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MYDelivery
{    
    public partial class Form2 : Form
    {
        int My_counter;
        public async void MyCounter()
        {
            int _counter = 1;
            using (StreamReader prob = new StreamReader(_deliveryLog))
            {
                string line = prob.ReadLine();
                while ((line = await prob.ReadLineAsync()) != null)
                {
                    _counter++;
                }
                textBox1.Text = Convert.ToString(_counter);
                My_counter = _counter;
            }
        }
        
        string _deliveryLog = @"C:\deliveryOrder\deliveryOrder.txt";
       
    public Form2()
        {
            InitializeComponent();
            MyCounter();
            comboBox1.Items.AddRange(DeliveryService._listCity);
            comboBox2.Items.AddRange(DeliveryService._listTime);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
           form1.ShowDialog();
        }
        private  void button1_Click(object sender, EventArgs e)
        {
            CheckCity(Convert.ToString(comboBox1.SelectedItem));
        }

        // Создаем заказ
        public void CreateOrders()
        {
            using (StreamWriter name = new StreamWriter(_deliveryLog, true))
            {
                name.WriteLine("|"+textBox1.Text + "\t\t|" + comboBox1.Text + "\t|" + textBox2.Text + "\t\t|" + dateTimePicker1.Text+"\t|"+comboBox2.Text);
            }

            MyCounter();
            DeliveryService deliveryService = new DeliveryService();
            deliveryService.CreateOrder(Convert.ToInt32(textBox1.Text), Convert.ToString(comboBox1.SelectedItem), Convert.ToDouble(textBox2.Text), dateTimePicker1, Convert.ToString(comboBox2.SelectedItem));
            textBox2.Clear();
            comboBox1.Text = null;
            comboBox2.Text = null;
            MessageBox.Show("Ваш заказ создан \nНомер заказа:  "+My_counter);
        }

        //проверка на отсутсвии значение района
        public void CheckCity(string values)
        {
            if (values != "")
            {
                CheckWeight(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Не выбран район заказа");
            }

        }
        //обработка некорректного ввода в поля вес
        public void CheckWeight(string values)
        {
            double x;
            if (double.TryParse(values, out x)&& Convert.ToDouble(values)>=0.01)
            {
                CheckTime(Convert.ToString(comboBox2.SelectedItem));
            }
            else
            {
                MessageBox.Show("Не указан или некорректно указан вес заказа");
            }
        }
        //проверка на выбора времени(интервала) доставки
        public void CheckTime(string values)
        {
            if (values != "")
            {
                CreateOrders();
            }
            else
            {
                MessageBox.Show("Не выбрана время доставки");
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
    
}
