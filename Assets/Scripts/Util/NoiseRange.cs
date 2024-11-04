using Unity.Mathematics;

namespace Util
{
public class NoiseRange
{
    static public (float, float) GetTemperatureAmpDist(float2 temperatureRange)
    {
        int2 baseTempRange = new(-10, 30);
        var tempRange = ConvertRange(temperatureRange,baseTempRange);
        float TAmp = math.distance(tempRange.x, tempRange.y);
        float Tdist = TAmp * 2 + math.distance(0, tempRange.x) * 2;
        return (TAmp, Tdist);
    }
    static public (float, float) GetHumidityAmpDist(float2 humidityRange)
    {
        int2 baseHumidityRange = new(0, 400);
        var humRange = ConvertRange(humidityRange, baseHumidityRange);
        float HAmp = math.length(humRange);
        float Hdist = HAmp + math.distance(0, humRange.x);
        return (HAmp, Hdist);
    }

    static float2 ConvertRange(float2 value, float2 range)
    {
        value.x = Convert025(value.x, range);
        value.y = Convert025(value.y, range);
        return value;
    }
    static float Convert025(float value, float2 range){
        return (float)((value - range.x) / (range.y - range.x) * 0.25);
    }
}
}