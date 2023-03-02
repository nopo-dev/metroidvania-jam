using UnityEngine;

/*
 * We assume that the last safe location
 * was within the current scene.
 */
public class LastSafeLocManager
{
    private Location lastSafeLoc_;

    public void setLastSafeLoc(Location safeLoc)
    {
        this.lastSafeLoc_ = safeLoc;
    }

    public Location getLastSafeLoc()
    {
        return this.lastSafeLoc_;
    }
}
