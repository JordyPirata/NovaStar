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
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < depth; x++)
                {
                    newArray[x, y] = array[(y * width) + x];
                }
            }
            return newArray;
        }
        public static string GetLastDirectory(string path)
        {
            // split when '/' && '\'is found
            string[] directories = path.Split('/', '\\');
            return directories[^1];
        }
    }
}