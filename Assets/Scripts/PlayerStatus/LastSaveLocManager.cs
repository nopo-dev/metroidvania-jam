/*
 * Theoretically this & LastSafeLocManager could be the same class,
 * since they do exactly the same thing.
 * But they might differ as we add more implementation, and I think there's
 * more clarity in LastSaveLocManager.getLastSaveLoc() 
 * vs. LastSaveLocManager.getLastLoc().
 */
public class LastSaveLocManager
{
    // We could just make this public.. but I made all the other ones private.
    // So keeping this private for consistent API.
    private Location lastSaveLoc_;

    public void setLastSaveLoc(Location saveLoc)
    {
        this.lastSaveLoc_ = saveLoc;
    }

    public Location getLastSaveLoc()
    {
        return this.lastSaveLoc_;
    }
}
