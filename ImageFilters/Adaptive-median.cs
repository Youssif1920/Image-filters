using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class Adaptive_median
    {
        public static Byte[,] createFilter(Byte[,] imageMatrix, int final_size, int initial_size, int choice)
        {
            int max_size = final_size;
            int window_size = initial_size;
            int indexCount = 2;
            byte[,] filterMatrix = new Byte[window_size, window_size];

            for (int i = 0; i < ImageOperations.GetHeight(imageMatrix); i++)
            {
                for (int j = 0; j < ImageOperations.GetWidth(imageMatrix); j++)
                {
                    for (int x = 0; x < window_size; x++)
                    {
                        if (i + (x - (window_size / 2)) < ImageOperations.GetHeight(imageMatrix) && i + (x - (window_size / 2)) >= 0)
                        {
                            for (int y = 0; y < window_size; y++)
                            {
                                if (i + (x - (window_size / 2)) < ImageOperations.GetHeight(imageMatrix) && i + (x - (window_size / 2)) >= 0)
                                {
                                    if (j + (y - (window_size / 2)) < ImageOperations.GetWidth(imageMatrix) && j + (y - (window_size / 2)) >= 0)
                                    {
                                        filterMatrix[x, y] = imageMatrix[i + (x - (window_size / 2)), j + (y - (window_size / 2))];
                                    }
                                    else
                                    {
                                        filterMatrix[x, y] = 0;
                                    }
                                }
                                else
                                {
                                    filterMatrix[x, y] = 0;
                                }
                            }
                        }
                    }

                    byte[] flatMatrix = Sort.flatten(filterMatrix, window_size);
                    int max = flatMatrix.Length - 1;
                    byte[] sorted_arr;
                    if (choice == 1)
                    {
                        sorted_arr = Sort.CountingSort(flatMatrix);
                    }
                    else
                    {
                        sorted_arr = Sort.QuickSort(flatMatrix,0,max);
                    }

                   
                    int Zmin = sorted_arr[0];
                    int Zmed = sorted_arr[sorted_arr.Length / 2];
                    int Zmax = sorted_arr[sorted_arr.Length - 1];
                    if(Zmin == 0 && Zmed == 0)
                    {
                        Zmin = sorted_arr[sorted_arr.Length - (indexCount * indexCount)];
                        Zmed = sorted_arr[sorted_arr.Length - (indexCount * indexCount) / 2];
                    }
                    
                    int a1 = Zmed - Zmin;
                    int a2 = Zmax - Zmed;
                    if (a1 > 0 && a2 > 0)
                    {

                        int b1 = imageMatrix[i, j] - Zmin;
                        int b2 = Zmax - imageMatrix[i, j];

                        if (b1 <= 0 || b2 <= 0)
                            imageMatrix[i, j] = (byte)Zmed;
                    }
 
                    else
                    {
                        if (window_size < max_size)
                        {  
                            window_size += 2;
                            filterMatrix = new Byte[window_size, window_size];
                            j--;
                            indexCount++;
                        }
                        else
                        {
                            imageMatrix[i, j] = (byte)Zmed;
                        }
                    }
                }
            }
            return imageMatrix;
        }
    }
}
