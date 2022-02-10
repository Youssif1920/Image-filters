using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;


namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }


        private void Adaptive_Click(object sender, EventArgs e)
        {
            int MaxSize = Convert.ToInt32(textBox1.Text);
            if (MaxSize < 3 || (MaxSize % 2 == 0))
                MaxSize = 3;
            
            Byte[,] NewImage = Sort.add_border(ImageMatrix, 1);
            Adaptive_median.createFilter(NewImage, MaxSize, 3, 1);
            ImageOperations.DisplayImage(NewImage, pictureBox2);
        }
        
       
        private void Adaptive_Quick_Click(object sender, EventArgs e)
        {
            int MaxSize = Convert.ToInt32(textBox1.Text);
            if (MaxSize < 3 || (MaxSize % 2 == 0))
                MaxSize = 3;
            Byte[,] NewImage = Sort.add_border(ImageMatrix, 1);
            Adaptive_median.createFilter(NewImage, MaxSize, 3, 2);
            ImageOperations.DisplayImage(NewImage, pictureBox2);
        }

        private void Alpha_Click(object sender, EventArgs e)
        {
            int windowSize = Convert.ToInt32(textBox2.Text);
            int Tval = Convert.ToInt32(textBox3.Text);
            if (windowSize < 3 || (windowSize %2  == 0))
            {
                windowSize = 3;
            }
            if(Tval * 2 > windowSize * windowSize || Tval < 0)
            { 
                Tval = windowSize;
            }
            Byte[,] paddedImage = Sort.add_border(ImageMatrix, windowSize);
            paddedImage = Alpha_trim.createFilter(paddedImage, windowSize, 1, Tval);
            ImageOperations.DisplayImage(paddedImage, pictureBox3);
        }

        private void Alpha_counting_Click(object sender, EventArgs e)
        {
            int windowSize = Convert.ToInt32(textBox2.Text);
            int Tval = Convert.ToInt32(textBox3.Text);
            if (windowSize < 3 || (windowSize % 2 == 0))
            {
                windowSize = 3;
            }
            if (Tval * 2 > windowSize * windowSize || Tval < 0)
            {
                Tval = windowSize;
            }
            Byte[,] paddedImage = Sort.add_border(ImageMatrix, windowSize);
            paddedImage = Alpha_trim.createFilter(paddedImage, windowSize, 2, Tval);
            ImageOperations.DisplayImage(paddedImage, pictureBox3);
        }

        private void Adapt_graph_Click(object sender, EventArgs e)
        {
            int size = Convert.ToInt32(textBox4.Text);
            if (size < 3 || (size % 2 == 0))
                size = 3;

            double[] window_size = new double[size / 2];
            double[] Counting_val = new double[size / 2];
            double[] Quick_val = new double[size / 2];

            Byte[,] NewImage = Sort.add_border(ImageMatrix, 1);
            int count = 0;
            for (int i = 3; i <= size; i += 2)
            {
                //counting time
                window_size[count] = i;
                float before_C = System.Environment.TickCount;
                Adaptive_median.createFilter(NewImage, i, 3, 1);
                float after_C = System.Environment.TickCount;
                float Time_C = (after_C - before_C) / 1000;
                Counting_val[count] = Time_C;
                

                //Quicksort time
                float before_Q = System.Environment.TickCount;
                Adaptive_median.createFilter(NewImage, i, 3, 2);
                float after_Q = System.Environment.TickCount;
                float Time_Q = (after_Q - before_Q) / 1000;
                Quick_val[count] = Time_Q;
                

                count++;
            }
            ZGraphForm ZGF = new ZGraphForm("Adaptive Graph", "Window size", "Time");
            ZGF.add_curve("counting sort", window_size, Counting_val, Color.Red);
            ZGF.add_curve("Quick sort", window_size, Quick_val, Color.Blue);
            ZGF.Show();
        }

        private void alpha_graph_Click(object sender, EventArgs e)
        {
            int size = Convert.ToInt32(textBox5.Text);
            int Tval = Convert.ToInt32(textBox3.Text);
            if ((size < 3 ) || (size % 2 == 0))
            {
                size = 3;
                Tval = 1;
            }
            if (Tval*2 > size*size || Tval < 0)
                Tval = 1;

                double[] window_size = new double[size / 2];
            double[] Counting_val = new double[size / 2];
            double[] Kth_element_val = new double[size / 2];

            Byte[,] NewImage = Sort.add_border(ImageMatrix, 1);
            int count = 0;
            for (int i = 3; i <= size; i += 2)
            {
                if (Tval*2 >= i*i)
                {
                    //counting time
                    window_size[count] = i;
                    float before_C = System.Environment.TickCount;
                    Alpha_trim.createFilter(NewImage, i, 1, i);
                    float after_C = System.Environment.TickCount;
                    float Time_C = (after_C - before_C) / 1000;
                    Counting_val[count] = Time_C;



                    //Kth Element time
                    float before_K = System.Environment.TickCount;
                    Alpha_trim.createFilter(NewImage, i, 1, i);
                    float after_K = System.Environment.TickCount;
                    float Time_K = (after_K - before_K) / 1000;
                    Kth_element_val[count] = Time_K;
                }
                else
                {
                    //counting time
                    window_size[count] = i;
                    float before_C = System.Environment.TickCount;
                    Alpha_trim.createFilter(NewImage, i, 1, Tval);
                    float after_C = System.Environment.TickCount;
                    float Time_C = (after_C - before_C) / 1000;
                    Counting_val[count] = Time_C;



                    //Kth Element time
                    float before_K = System.Environment.TickCount;
                    Alpha_trim.createFilter(NewImage, i, 1, Tval);
                    float after_K = System.Environment.TickCount;
                    float Time_K = (after_K - before_K) / 1000;
                    Kth_element_val[count] = Time_K;
                }
               
                

                count++;
            }
            ZGraphForm ZGF = new ZGraphForm("Alpha Trim Graph", "Window size", "Time");
            ZGF.add_curve("counting sort", window_size, Counting_val, Color.Red);
            ZGF.add_curve("Kth Element", window_size, Kth_element_val, Color.Blue);
            ZGF.Show();
        }   
    }
}