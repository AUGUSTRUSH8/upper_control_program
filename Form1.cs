using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;

namespace 串口控制
{
    public partial class Form1 : Form
    {
        int i=0;
        byte last = 0;
        //device 1
        const byte DeviceOpen1 = 0x01;
        const byte DeviceClose1 = 0x81;
        //device 2
        const byte DeviceOpen2 = 0x02;
        const byte DeviceClose2 = 0x82;
        //device 3
        const byte DeviceOpen3 = 0x03;
        const byte DeviceClose3 = 0x83;
        //SerialPort Write Buffer
        byte[] SerialPortDataBuffer = new byte[1];
        public Form1()
        {
            InitializeComponent();      //窗口构造
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)                                     //串口打开就关闭
            {
                try
                {
                    serialPort1.Close();
                }
                catch { }                                               //确保万无一失
                ovalShape1.FillColor = Color.Gray;
                button1.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;              //端口号
                    serialPort1.Open();                                 //打开端口
                    ovalShape1.FillColor = Color.Green;
                    button1.Text = "关闭串口";
                }
                catch
                {
                    MessageBox.Show("串口打开失败","错误");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)//窗体初始化
        {
            comboBox2.Text = "9600";
            ovalShape1.FillColor = Color.Gray;
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);

           
           
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)//数据接收处理程序
        {
            byte data;
            int sum;
            
            data = (byte)serialPort1.ReadByte();
           
           // string str = Convert.ToString(data, 10);

            if ((data-last)<0)
            {
                i++;
            }
            sum = i * 255 + data;
            String str1 = Convert.ToString(sum,10);
            textBox3.AppendText(sum+" ");
            double f = 0.6 * sum;
            textBox4.AppendText(f+ "°");

            last = data;
            serialPort1.DiscardInBuffer();
        }

        private void WriteByteToSerialPort(byte data)                   //单字节写入串口
        {
            serialPort1.DiscardInBuffer();
            serialPort1.DiscardOutBuffer();
            byte[] Buffer = new byte [1]{ data };                       //定义数组
            if (serialPort1.IsOpen)                                     //传输数据的前提是端口已打开
            {
                try
                {
                    serialPort1.Write(Buffer, 0, 1);                    //写数据
                }
                catch 
                {
                    MessageBox.Show("串口数据发送出错，请检查.","错误");//错误处理
                }
            }
        }

        private void SearchAndAddSerialToComboBox(SerialPort MyPort,ComboBox MyBox)
        {                                                               //将可用端口号添加到ComboBox
            //string[] MyString = new string[20];                         //最多容纳20个，太多会影响调试效率
            string Buffer;                                              //缓存
            MyBox.Items.Clear();                                        //清空ComboBox内容
            //int count = 0;
            for (int i = 1; i < 20; i++)                                //循环
            {
                try                                                     //核心原理是依靠try和catch完成遍历
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open();                                      //如果失败，后面的代码不会执行
                   // MyString[count] = Buffer;
                    MyBox.Items.Add(Buffer);                            //打开成功，添加至下俩列表
                    MyPort.Close();                                     //关闭
                    //count++;
                }
                catch 
                {
                    //count--;
                }
            }
            //MyBox.Text = MyString[0];                                   //初始化
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen1);                         //器件一开
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose1);                        //器件一关
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen2);                         //器件二开
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose2);                        //器件二关
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceOpen3);                         //器件三开
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteByteToSerialPort(DeviceClose3);                        //器件三关
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);       //扫描并讲课用串口添加至下拉列表
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("软件打开后扫描串口，\n通信无误后，按下设备开，\n会发送控制代码，串口中断,\n单片机检测到对应控制码后会发出位控信号；\n设备有两种停止方式：\n立即抱闸停止，减速停止","帮助");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            double deg;
            double pulse;
            double extra;
            double sum;
            double t;
            double tsum;
            deg = Convert.ToDouble(textBox1.Text);
            extra = 3.02 * 5000;
            pulse = 5000*deg;
            sum = extra + pulse;
            textBox2.Text =sum.ToString();
            t = pulse / 20000;
            tsum = t + 0.775;
            textBox5.Text = tsum.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此计算以20000脉冲每秒为目标速度","提示");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double jiao;
            double maichong;
            jiao = Convert.ToDouble(textBox6.Text);
            maichong = 5000 * jiao;
            textBox7.Text = maichong.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
