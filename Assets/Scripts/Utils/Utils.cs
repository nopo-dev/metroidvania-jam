/*
 * A collection of public utils
 */
public static class Utils
{
    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    public static int posMod(int x, int m)
    {
        if (m < 0) { m = -m; }
        x = x % m;
        return x < 0 ? x + m : x;
    }
}
