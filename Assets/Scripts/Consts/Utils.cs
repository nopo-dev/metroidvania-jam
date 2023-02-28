/*
 * A collection of public utils
 */
public static class Utils
{
    public static int ClampInt(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    public static float ClampFloat(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
