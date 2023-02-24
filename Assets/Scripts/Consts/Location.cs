/*
 * Struct to store locations, with getXY method.
 * Could be named "PlayerAndCameraLocation" or something more descriptive.
 */
[System.Serializable]
public struct Location
{
    public float x;
    public float y;

    public Location(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
