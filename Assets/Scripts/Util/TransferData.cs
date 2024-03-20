// Purpose: Contains the TransferData struct, which contains a method to transfer a 1D array to a 2D array.
namespace Util
{
    public struct TransferData
    {
        public static float[,] TransferDataFromArrayTo2DArray(float[] array, int width, int height)
        {
            float[,] newArray = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    newArray[i, j] = array[i * width + j];
                }
            }
            return newArray;
        }

    }
}