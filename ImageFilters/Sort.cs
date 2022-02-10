using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class Sort
    {
        public static void Kth_element(ref Byte[] arr)
        {
            Byte min = 255, max = 0;
            int min_index = 0, max_index = 0;
            Byte num;
            int count = 0;
            Byte[] array = new Byte[arr.Length - 2];

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= max)
                {
                    max = arr[i];
                    max_index = i;
                }

                else if (arr[i] <= min)
                {
                    min = arr[i];
                    min_index = i;
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (i == max_index)
                    continue;
                else if (i == min_index)
                    continue;
                else
                {
                    num = arr[i];
                    array[count] = num;
                    count++;
                }
            }
            arr = array;
        }

        public static Byte[] CountingSort(Byte[] arr) //array(n)    
        {
            Byte[] freqArr = new Byte[256];
            int size = arr.Length;
            for (int i = 0; i < size; i++)
            {
                Byte index = arr[i];
                freqArr[index]++;
            }
            for (int i = 1; i < 256; i++)
            {
                freqArr[i] += freqArr[i - 1];
            }


            for (int i = 255; i > 0; i--)
            {
                freqArr[i] = freqArr[i - 1];
            }
            freqArr[0] = 0;


            Byte[] arranged = new Byte[size];

            for (int i = 0; i < size; i++)
            {
                Byte index = arr[i];
                Byte arrIndex = freqArr[index];
                arranged[arrIndex] = index;
                freqArr[index]++;
            }
            return arranged;
        }


        public static int Partition(Byte[] arr, int min, int max) //array(n)
        {
            int pivot = arr[max];
            int i = (min - 1);

            for (int j = min; j <= max - 1; j++)
            {

                if (arr[j] <= pivot)
                {
                    i++;
                    swap(ref arr[i], ref arr[j]);
                }
            }
            swap(ref arr[i + 1], ref arr[max]);
            return (i + 1);
        }
        public static void swap(ref byte num1, ref byte num2) //C
        {
            byte temp;
            temp = num1;
            num1 = num2;
            num2 = temp;
        }


        public static byte[] QuickSort(Byte[] arr, int min, int max)
        {
            if (min < max)
            {
                int pivot = Partition(arr, min, max);
                QuickSort(arr, min, pivot - 1);
                QuickSort(arr, pivot + 1, max);
            }
            return arr;
        }


        public static void print(byte[] arr, int size)
        {
            int i;
            for (i = 0; i < size; i++)
            {
                Console.Write("{0} ",arr[i]);
            }
        }

        public static byte Average(Byte[] arr, int start, int end)
        {
            int sum = 0;
            for (int i = start; i < end; i++)
            {
                sum += arr[i];
            }
            byte average = (byte)(sum / (end - start));
            return average;
        }

        //convert 2D matrix to 1D matrix
        public static byte[] flatten(byte[,] Array, int dimension)
        {
            byte[] flatArray = new byte[dimension * dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    int index = (dimension * i) + j;
                    flatArray[index] = Array[i, j];
                }
            }
            return flatArray;
        }
        public static Byte[,] add_border(Byte[,] imageMatrix, int windowsize)
        {
            Byte[,] paddedImage = new Byte[ImageOperations.GetHeight(imageMatrix) + (windowsize-1), ImageOperations.GetWidth(imageMatrix) + (windowsize-1)];
            for (int i = 0; i < ImageOperations.GetHeight(imageMatrix); i++)
            {
                for (int j = 0; j < ImageOperations.GetWidth(imageMatrix); j++)
                {
                    paddedImage[i + (windowsize / 2), j + (windowsize / 2)] = imageMatrix[i, j];
                }
            }
            return paddedImage;
        }

    }
}
