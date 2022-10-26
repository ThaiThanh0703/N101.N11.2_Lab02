using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Numerics;

namespace Task1
{
    public partial class Task1 : Form
    {
        DateTime startTime;
        DateTime endTime;
        TimeSpan timeSpan;
        public Task1()
        {
            InitializeComponent();
        }
        private int random_Length(int max)
        {
            Random length = new Random();
            return length.Next(30,max);
        }
        private char random_NumForBNum(string digit)
        {
            Random index = new Random();
            return digit[index.Next(digit.Length)];
        }
        private int random_Num(int min,int max)
        {
            Random number= new Random();
            return number.Next(min,max);
        }
        private string CountTime()
        {
            endTime = DateTime.Now;
            timeSpan = endTime - startTime;
            return timeSpan.TotalSeconds + " s";
        }
        private string generateBNum(string digit)
        {
            int support_Length =48;
            string Number = "";
            int num_Length=random_Length(support_Length);
            char temp = 'm';
            for (int i = 0; i <= num_Length; i++)
            {
                temp = random_NumForBNum(digit);
                while (i >0 && temp == Number[Number.Length - 1])
                {
                    temp = random_NumForBNum(digit);
                }       
                Number += temp;
            }
            return Number;
        }
        private void Decompose(BigInteger N,ref int a,ref BigInteger b)
        {
            int i = 0;
            while (N % 2 == 0)
            {
                i++;
                N=N/2;
            }
            a = i;
            b = N;
        }

        private Boolean witness(BigInteger a, BigInteger N)
        {
            int k;
            BigInteger m,b,n;
            n = N - 1;
            m = k = 0;
            Decompose(N - 1, ref k, ref m);
            b = ModPow_BInt(a,m,N);
            if (b==1 ||b == n)
            {
                return true;
            }
            for (int i = 1; i < k; i++)
            {
                b = (b * b) % N; 
                if (b==n) return true;
                if (b == 1) return false;
            }
            return false;
        }
        private BigInteger BIntGCD(BigInteger num1,BigInteger num2)
        {
            if (num2 == 0)
                return num1;
            return BIntGCD((BigInteger)num2, (BigInteger)num1%num2);
        }

        private Boolean CheckPrime_BInt(string number)
        {
            BigInteger N =BigInteger.Parse(number);
            if (N <= 1) return false; 
            if (N == 2) return true;
            if (N > 2 && (N % 2 == 0)) return false;
            int loop = 50;
            for (int i = 0; i < loop; i++)
            {
                BigInteger a = BigInteger.Parse(generateBNum(number));
                if (witness(a, N) == false) return false;
            }
            return true;
        }
        private BigInteger ModPow_BInt(BigInteger a, BigInteger x, BigInteger p)
        {
            return BigInteger.ModPow(a, x, p);
        }
        private int ModPow_Int(int a, int x, int p)
        {
            if (x==1)
                return a% p;
            else
            {
                int i = ModPow_Int(a, x / 2, p);
                if (x % 2 == 0) return (i * i) % p;
                else return (i*i*a)%p;
            }
        }
        private void btnGCD_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            if (tbNum1.Text == "") tbNum1.Text = generateBNum("0123456789");
            if (tbNum2.Text == "") tbNum2.Text = generateBNum("0123456789");
            tbResult.Text = BIntGCD(BigInteger.Parse(tbNum1.Text), BigInteger.Parse(tbNum2.Text)).ToString();
            tbTime.Text=CountTime();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            tbNum1.Text = generateBNum("0123456789");
            tbNum2.Text = generateBNum("0123456789");
            tbTime.Text=CountTime();
        }

        private void btnRandom2_Click(object sender, EventArgs e)
        {
            tbNumA.Text = random_Num(1,10).ToString();
            tbNumX.Text = random_Num(80,100).ToString();
            tbNumP.Text = random_Num(10, 50).ToString();
        }
        private void btnCal_Click(object sender, EventArgs e)
        {
            //BigInteger a = BigInteger.Parse(tbNumA.Text);
            //BigInteger x = BigInteger.Parse(tbNumX.Text);
            //BigInteger p = BigInteger.Parse(tbNumP.Text);
            //BigInteger r = new BigInteger();
            //r = BigInteger.Pow(a,(int)x) % p;
            startTime = DateTime.Now;
            if (tbNumA.Text == "" || tbNumX.Text == "" || tbNumP.Text == "")
            {
                MessageBox.Show("None-Parameter");
                return;
            }
            tbResult2.Text= ModPow_Int(int.Parse(tbNumA.Text), int.Parse(tbNumX.Text), int.Parse(tbNumP.Text)).ToString(); ;
            //tbResult2.Text = ModPow_BInt(BigInteger.Parse(tbNumA.Text), BigInteger.Parse(tbNumX.Text), BigInteger.Parse(tbNumP.Text)).ToString();
            tbTime2.Text = CountTime();
        }

        private void btnCheckPrime_Click(object sender, EventArgs e)
        {
            startTime=DateTime.Now;
            if (tbBNum.Text=="")
            {
                MessageBox.Show("None!");
                return;
            }
            if (CheckPrime_BInt(tbBNum.Text)== false)
            {
                MessageBox.Show("It is not a prime !");
            }
            if (CheckPrime_BInt(tbBNum.Text) == true)
            {
                MessageBox.Show("It is a prime !");
            }
            tbTime3.Text=CountTime();
        }

        private void btnRandomPrime_Click(object sender, EventArgs e)
        {
            startTime= DateTime.Now;
            string N = generateBNum("0123456789");
            while (CheckPrime_BInt(N) == false)
            {
                N = generateBNum("0123456789");
            };
            tbPrime.Text = N;
            tbTime3.Text = CountTime();
        }
    }
}
