using Unity.Burst;
using Unity.Collections;
[BurstCompile]
public struct TransferData
{
    public float[,] TransferDataFromNativeArrayTo2DArray(NativeArray<float> nativeArray, int width, int height)
    {
        float[,] array = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                array[i, j] = nativeArray[i * width + j];
            }
        }
        return array;
    }
}