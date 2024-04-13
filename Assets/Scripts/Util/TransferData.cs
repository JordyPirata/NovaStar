// Purpose: Contains the TransferData struct, which contains a method to transfer a 1D array to a 2D array.
using Unity.Burst;

namespace Util
{
    [BurstCompile]
    public struct TransferData
    {
        public static float[,] TransferDataFromArrayTo2DArray(float[] array, int width, int depth)
        {
            float[,] newArray = new float[width, depth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < depth; j++)
                {
                    newArray[i, j] = array[(i * width) + j];
                }
            }
            return newArray;
        }

    }
}