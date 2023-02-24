/*
 * Struct to store locations, with getXY method.
 * Could be named "PlayerAndCameraLocation" or something more descriptive.
 */
public struct Location
{
    // TODO: not sure if xy is the best way to do it
    public float playerX;
    public float playerY;
    public float cameraX;
    public float cameraY;

    public Location(float playerX, float playerY, float cameraX, float cameraY)
    {
        this.playerX = playerX;
        this.playerY = playerY;
        this.cameraX = cameraX;
        this.cameraY = cameraY;
    }
}
