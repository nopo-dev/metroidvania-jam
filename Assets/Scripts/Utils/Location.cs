/*
 * Struct to store locations, with getXY method.
 * Could be named "PlayerAndCameraLocation" or something more descriptive.
 */
[System.Serializable]
public struct Location
{
    public float x;
    public float y;
    public string sceneName;

    public Location(float x, float y, string sceneName)
    {
        this.x = x;
        this.y = y;
        this.sceneName = sceneName; // This will be exposed to inspector but should not be changed by it.
    }
}
