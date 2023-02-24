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
