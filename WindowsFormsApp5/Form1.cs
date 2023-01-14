using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        int elkuldott = 0;
        int sikeres = 0;
        private Stopwatch stopWatch=new Stopwatch();
        int hossz = 0;
        public Form1()
        {
            InitializeComponent();
            timer2 = new Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 500; // in miliseconds
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string fajlnev = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + ".txt";
            elkuldott++;
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send("google.com", 1000);
                if (reply != null)
                {
                    label3.Text = reply.RoundtripTime.ToString();

                }
                if (reply.RoundtripTime > 200)
                {
                    StreamWriter sw = new StreamWriter(fajlnev, true);
                    sw.WriteLine(DateTime.Now.ToString() + " Latency: " + reply.RoundtripTime.ToString());
                    sw.Close();
                    label2.Text = "!";
                }
                else 
                {
                    label2.Text = "";
                    sikeres++;
                }
            }
            catch
            {
                
                
                StreamWriter sw = new StreamWriter(fajlnev, true);
                sw.WriteLine(DateTime.Now.ToString() + " unable to ping");
                sw.Close();
                label2.Text = "!";
            }
            double szazalekki = Convert.ToDouble(sikeres) / Convert.ToDouble(elkuldott) * 100;
            szazalek.Text = sikeres.ToString() + " / " + elkuldott.ToString() +" "+ szazalekki.ToString("#0.##")+"%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hossz = Convert.ToInt32(numericUpDown1.Value);
            timer2.Start();
            stopWatch.Reset();
            stopWatch.Start();
            progressBar1.Maximum = hossz * 60000+100;
            progressBar1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            stopWatch.Stop();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void stopwatch_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = Convert.ToInt32(stopWatch.ElapsedMilliseconds);
            if (progressBar1.Value>=hossz*60000)
            {
                progressBar1.Visible = false;
            }
            if (stopWatch.ElapsedMilliseconds >= hossz*60000)
            {
                button2_Click(sender, e);
            }
            this.ido.Text = string.Format("{0:hh\\:mm\\:ss\\:fff}", stopWatch.Elapsed);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
