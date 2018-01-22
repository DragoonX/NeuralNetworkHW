using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNet_Homework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_calc_Click(object sender, EventArgs e)
        {

            double  Ex = 0, Ey=0, Exx=0, Eyy = 0, Ex2 = 0, Ey2 = 0, Exy=0;
           
            double a = 0, b = 0;
            double hataorani = 0, mutlakhata = 0, hatayuzdesi = 0;
            int n = dataGridView1.Rows.Count - 1;

            /*VERİLERİ AL VE HESAPLA*/

            for (int i = 0; i < dataGridView1.Rows.Count; i++){
                double satirx = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                double satiry = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);

                Ey += satiry;
                Ex += satirx;
                Exx += satirx * satirx; //her x in kareleri alınarak toplanır.
                Exy += satirx * satiry; //her satırdaki x ve y ayrı ayrı çarpılarak bulunan sonuçlar toplanır.
                Eyy += satiry * satiry; //her y nin kareleri alınarak toplanır.
            }   //for döngüsü sonu
                Ex2 = Ex * Ex;
                Ey2 = Ey * Ey;

            /*VERİLERİ YAZDIR*/

            tbx_ex.Text = Convert.ToString(Ex);
            tbx_ey.Text = Convert.ToString(Ey);
            tbx_exx.Text = Convert.ToString(Exx);
            tbx_exy.Text = Convert.ToString(Exy);
            tbx_ex2.Text = Convert.ToString(Ex2);
            tbx_ey2.Text = Convert.ToString(Ey2);
            tbx_eyy.Text = Convert.ToString(Eyy);
            tbx_datacount.Text = Convert.ToString(n);

            /*REGRESYON MODELİ OLUŞTUR*/

            for (int d = 0; d < n; d++){
                double  below = (n * Exx) - (Ex2);
                double calculatedy = 0;
                a = (Ey * Exx - Ex * Exy) / below;
                b = (n * Exy - Ex * Ey) / below;
                calculatedy = a + b * (Convert.ToDouble(dataGridView1.Rows[d].Cells[0].Value));

                hataorani = (Convert.ToDouble(dataGridView1.Rows[d].Cells[1].Value) - calculatedy) / Convert.ToDouble(dataGridView1.Rows[d].Cells[1].Value);
                // hataoranı = (mevcut değer - bulunan değer) / mevcut değer.
                mutlakhata = Math.Abs(hataorani);
                //hata oranının mutlak değeri
                hatayuzdesi = mutlakhata * 100;

                dataGridView2.Rows.Add();   //ilk satır başlangıcı verilir. bu kod yazılmazsa datagridview e veri aktarılamaz.
                dataGridView2.Rows[d].Cells[0].Value = dataGridView1.Rows[d].Cells[0].Value;
                dataGridView2.Rows[d].Cells[1].Value = dataGridView1.Rows[d].Cells[1].Value;
                dataGridView2.Rows[d].Cells[2].Value = calculatedy.ToString("0.###");
                dataGridView2.Rows[d].Cells[3].Value = hataorani.ToString("0.###");
                dataGridView2.Rows[d].Cells[4].Value = mutlakhata.ToString("0.###");
                dataGridView2.Rows[d].Cells[5].Value = hatayuzdesi.ToString("0.##");

            }   //for döngüsü sonu
            

            lbl_a.Text = a.ToString("0.###");
            lbl_b.Text = b.ToString("0.###");

            /* NORMALİZASYON*/

            double[] xcont = new double[n+1];
            double[] ycont = new double[n+1];
            double maksx, minx, maksy, miny;

            for (int j=0;j<n;j++)
            {
                xcont[j] = Convert.ToDouble(dataGridView1.Rows[j].Cells[0].Value);
                ycont[j] = Convert.ToDouble(dataGridView1.Rows[j].Cells[1].Value);
            }

            minx = xcont[0];
            maksx = xcont[0];
            miny = ycont[0];
            maksy = ycont[0];

            for (int k=0;k<n;k++) {
                if (minx > xcont[k]) minx = xcont[k];
                if (maksx < xcont[k]) maksx = xcont[k];
                if (miny > ycont[k]) miny = ycont[k];
                if (maksy < ycont[k]) maksy = ycont[k];
            }

            for(int l = 0; l < n; l++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[l].Cells[0].Value = ((Convert.ToDouble(dataGridView1.Rows[l].Cells[0].Value) - minx)/(maksx - minx)).ToString("0.###");
                dataGridView3.Rows[l].Cells[1].Value = ((Convert.ToDouble(dataGridView1.Rows[l].Cells[1].Value) - miny) / (maksy - miny)).ToString("0.###");
            }


        } //buton tıklama fonksiyonu sonu


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("35","9");
            dataGridView1.Rows.Add("49", "15");
            dataGridView1.Rows.Add("21", "7");
            dataGridView1.Rows.Add("39", "11");
            dataGridView1.Rows.Add("15", "5");
            dataGridView1.Rows.Add("28", "8");
            dataGridView1.Rows.Add("25", "9");
            dataGridView1.Rows.Add("14", "7");
            dataGridView1.Rows.Add("19", "8");
            dataGridView1.Rows.Add("54", "19");

            /*2. tabdaki ysa da random değerler atanacak*/

            Random rastgele = new Random();
            int count = 0;
            foreach (var element in panel3.Controls)
            { //Random rastgele = new Random();
                if (element is Label)
                {
                    count++;

                    double sayi = rastgele.NextDouble();
                    sayi = Math.Round(sayi, 1);
                    (element as Label).Text = (String)Convert.ToString(sayi);
                }
                else
                    continue;
            }

            foreach (var element in panel4.Controls)
            {

                if (element is Label)
                {

                    double sayi = rastgele.NextDouble();
                    sayi = Math.Round(sayi, 1);
                    (element as Label).Text = (String)Convert.ToString(sayi);
                }
                else
                    continue;
            }

        }

        public float[] insertion_sort(float[] dizi)
        {
            for (int j = 1; j < 10; j++)
            {
                float key = dizi[j];
                int i = j - 1;
                while (i >= 0 && dizi[i] > key)
                {
                    dizi[i + 1] = dizi[i];
                    i = i - 1;
                }
                dizi[i + 1] = key;
            }
            return dizi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float[] dizi = new float[10];
            float[] dizi1 = new float[10];
            float[] dizi2 = new float[10];

            int count = 0, i = 0;
            float max = 0, min = 0, norm;
            foreach (var element in panel1.Controls)
            {
                if (element is TextBox)
                {
                    if ((element as TextBox).Text != string.Empty)
                    {
                        dizi[9 - count] = (float)Convert.ToDouble((element as TextBox).Text);
                        dizi1[9 - count] = (float)Convert.ToDouble((element as TextBox).Text);
                        count++;
                    }
                    else
                        continue;
                }
                else
                    continue;
            }

            dizi1 = insertion_sort(dizi1);
            max = dizi1[9];
            min = dizi1[0];

            for (i = 0; i < count; i++)
            {
                norm = (dizi[i] - min) / (max - min);
                dizi2[i] = norm;
            }

            lbl1.Text = dizi2[0].ToString();
            lbl2.Text = dizi2[1].ToString();
            lbl3.Text = dizi2[2].ToString();
            lbl4.Text = dizi2[3].ToString();
            lbl5.Text = dizi2[4].ToString();
            lbl6.Text = dizi2[5].ToString();
            lbl7.Text = dizi2[6].ToString();
            lbl8.Text = dizi2[7].ToString();
            lbl9.Text = dizi2[8].ToString();
            lbl10.Text = dizi2[9].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x33, x44, x55;
            foreach (var element in groupBox1.Controls)
            {
                double x = (float)Convert.ToDouble(x1.Text);
                double y = (float)Convert.ToDouble(x2.Text);
                x33 = (x * ((float)Convert.ToDouble(label24.Text))) + (y * ((float)Convert.ToDouble(label27.Text)));
                x33 = 1 / (1 + (Math.Exp(-x33)));
                x33 = Math.Round(x33, 4);
                x44 = (x * ((float)Convert.ToDouble(label25.Text))) + (y * ((float)Convert.ToDouble(label28.Text)));
                x44 = 1 / (1 + (Math.Exp(-x44)));
                x44 = Math.Round(x44, 4);
                x55 = (x * ((float)Convert.ToDouble(label26.Text))) + (y * ((float)Convert.ToDouble(label29.Text)));
                x55 = 1 / (1 + (Math.Exp(-x55)));
                x55 = Math.Round(x55, 4);
                x3.Text = (String)Convert.ToString(x33);
                x4.Text = (String)Convert.ToString(x44);
                x5.Text = (String)Convert.ToString(x55);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double xnet;
            foreach (var element in groupBox2.Controls)
            {
                double x = (float)Convert.ToDouble(x3.Text);
                double y = (float)Convert.ToDouble(x4.Text);
                double z = (float)Convert.ToDouble(x5.Text);
                xnet = (x * ((float)Convert.ToDouble(label30.Text))) + (y * ((float)Convert.ToDouble(label31.Text)) + (z * ((float)Convert.ToDouble(label32.Text))));
                xnet = 1 / (1 + (Math.Exp(-xnet)));
                xnet = Math.Round(xnet, 4);

                x6.Text = (String)Convert.ToString(xnet);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[] deltaW = new double[3];
            double[] W = new double[3];
            double[] sigma = new double[3];
            double[] deltaA = new double[6];
            double x = (float)Convert.ToDouble(x3.Text);
            double y = (float)Convert.ToDouble(x4.Text);
            double z = (float)Convert.ToDouble(x5.Text);

            double sonuc = (float)Convert.ToDouble(x6.Text);
            double beklenen = (float)Convert.ToDouble(textBox11.Text);

            double hata = beklenen - sonuc;
            textBox12.Text = (String)Convert.ToString(hata);

            double sigmam = sonuc * (1 - sonuc) * hata;
            sigmam = Math.Round(sigmam, 3);
            textBox13.Text = (String)Convert.ToString(sigmam);

            deltaW[0] = 0.5 * sigmam * x;
            deltaW[0] = Math.Round(deltaW[0], 4);
            textBox14.Text = (String)Convert.ToString(deltaW[0]);
            deltaW[1] = 0.5 * sigmam * y;
            deltaW[1] = Math.Round(deltaW[1], 4);
            textBox15.Text = (String)Convert.ToString(deltaW[1]);
            deltaW[2] = 0.5 * sigmam * z;
            deltaW[2] = Math.Round(deltaW[2], 4);
            textBox16.Text = (String)Convert.ToString(deltaW[2]);

            W[0] = deltaW[0] * (double)Convert.ToDouble(label30.Text);
            W[0] = Math.Round(W[0], 4);
            textBox17.Text = (String)Convert.ToString(W[0]);
            label68.Text = (String)Convert.ToString(W[0]);
            W[1] = deltaW[1] * (double)Convert.ToDouble(label31.Text);
            W[1] = Math.Round(W[1], 4);
            textBox18.Text = (String)Convert.ToString(W[1]);
            label69.Text = (String)Convert.ToString(W[1]);
            W[2] = deltaW[2] * (double)Convert.ToDouble(label32.Text);
            W[2] = Math.Round(W[2], 4);
            textBox19.Text = (String)Convert.ToString(W[2]);
            label67.Text = (String)Convert.ToString(W[2]);

            sigma[0] = x * (1 - x) * sigmam * W[0];
            sigma[0] = Math.Round(sigma[0], 10);
            textBox20.Text = (String)Convert.ToString(sigma[0]);

            sigma[1] = y * (1 - y) * sigmam * W[1];
            sigma[1] = Math.Round(sigma[1], 10);
            textBox21.Text = (String)Convert.ToString(sigma[1]);

            sigma[2] = z * (1 - z) * sigmam * W[2];
            sigma[2] = Math.Round(sigma[2], 10);
            textBox22.Text = (String)Convert.ToString(sigma[2]);

            deltaA[0] = 0.5 * sigma[0] * (double)Convert.ToDouble(label24.Text);
            label59.Text = (String)Convert.ToString(deltaA[0]);
            textBox28.Text = (String)Convert.ToString(deltaA[0]);
            deltaA[1] = 0.5 * sigma[1] * (double)Convert.ToDouble(label25.Text);
            textBox27.Text = (String)Convert.ToString(deltaA[1]);
            label56.Text = (String)Convert.ToString(deltaA[1]);
            deltaA[2] = 0.5 * sigma[2] * (double)Convert.ToDouble(label26.Text);
            textBox26.Text = (String)Convert.ToString(deltaA[2]);
            label55.Text = (String)Convert.ToString(deltaA[2]);

            deltaA[3] = 0.5 * sigma[0] * (double)Convert.ToDouble(label27.Text);
            textBox25.Text = (String)Convert.ToString(deltaA[3]);
            label58.Text = (String)Convert.ToString(deltaA[3]);
            deltaA[4] = 0.5 * sigma[1] * (double)Convert.ToDouble(label28.Text);
            textBox24.Text = (String)Convert.ToString(deltaA[4]);
            label57.Text = (String)Convert.ToString(deltaA[4]);
            deltaA[5] = 0.5 * sigma[2] * (double)Convert.ToDouble(label29.Text);
            textBox23.Text = (String)Convert.ToString(deltaA[5]);
            label60.Text = (String)Convert.ToString(deltaA[5]);

            textBox30.Text = x1.Text;
            textBox29.Text = x2.Text;

            double x66;
            foreach (var element in groupBox2.Controls)
            {
                double a = (float)Convert.ToDouble(x3.Text);
                double b = (float)Convert.ToDouble(x4.Text);
                double c = (float)Convert.ToDouble(x5.Text);
                x66 = (a * ((float)Convert.ToDouble(label68.Text))) + (b * ((float)Convert.ToDouble(label69.Text)) + (c * ((float)Convert.ToDouble(label67.Text))));
                x66 = 1 / (1 + (Math.Exp(-x66)));
                x66 = Math.Round(x66, 4);

                textBox34.Text = (String)Convert.ToString(x66);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
        }
    }
}
