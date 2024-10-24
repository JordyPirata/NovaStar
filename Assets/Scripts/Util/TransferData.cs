namespace Util
{
    public class TransferData
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
        public static int GetIndex(int x, int y, int width)
        {
            return (y * width) + x;
        }
    }
}