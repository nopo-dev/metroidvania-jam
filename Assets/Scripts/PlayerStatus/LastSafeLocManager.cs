using UnityEngine;


public class LastSafeLocManager : MonoBehaviour
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
