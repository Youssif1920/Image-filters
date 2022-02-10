using System;
using System.Collections.Generic;
using System.Text;
using ImageFilters;

namespace ImageFilters
{
    class Alpha_trim
    {
        public static Byte[,] createFilter(Byte[,] imageMatrix, int windowSize,int choice, int Tval)
        {
            Byte[,] filterMatrix = new Byte[windowSize, windowSize];
            
            for (int i = windowSize/2; i < ImageOperations.GetHeight(imageMatrix) - windowSize / 2; i++)
            {
                for (int j = windowSize / 2; j < ImageOperations.GetWidth(imageMatrix) - windowSize / 2; j++)
                {
                    for (int x = 0; x < windowSize; x++)
                    {
                        for (int y = 0; y < windowSize; y++)
                        {
                            filterMatrix[x, y] = imageMatrix[i + (x - windowSize / 2), j + (y - windowSize / 2)];   
                        }
                        
                    }
                    byte[] flatMatrix = ImageFilters.Sort.flatten(filterMatrix, windowSize);
                    if (choice == 1)
                    {
                        for (int z = 0; z < Tval; z++)
                        {
                            Sort.Kth_element(ref flatMatrix);
                        }
                        
                        byte newPixel = Sort.Average(flatMatrix, 0, flatMatrix.Length);
                        imageMatrix[i, j] = newPixel;
                    }
                    else
                    {
                        byte[] sortedMatrix = ImageFilters.Sort.CountingSort(flatMatrix);
                        byte newPixel = Sort.Average(sortedMatrix, Tval, (flatMatrix.Length - Tval));
                        imageMatrix[i, j] = newPixel;
                    }                    
                } 
            }
            return imageMatrix;
        }
    }
}
