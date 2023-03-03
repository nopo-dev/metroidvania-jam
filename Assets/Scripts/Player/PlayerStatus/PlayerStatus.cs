using UnityEngine;
using TMPro;

/*
 * This will be a singleton, there should only be one PlayerStatus
 * Handles PlayerStatus, PlayerMovement, PlayerAnimation.
 * Probably will handle attacks and stuff too.
 */
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    // TODO: these managers should handle status bar stuff too.
    public HPManager HPManager { get; private set; }
    public EnergyManager EnergyManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }
    public LastSaveLocManager LastSaveLocManager { get; private set; }
    public LastSafeLocManager LastSafeLocManager { get; private set; }

    /*
     * Initialize the singleton instance if it does not already exist.
     * If it exists & is different from current instance, destroy this instance.
     */
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one PlayerStatus!");
            Destroy(this);
            return;
        }
        Instance = this;
        this.HPManager = new HPManager();
        this.EnergyManager = new EnergyManager();
        this.UpgradeManager = new UpgradeManager();
        this.LastSaveLocManager = new LastSaveLocManager();
        this.LastSafeLocManager = new LastSafeLocManager();
        DontDestroyOnLoad(gameObject);
    }

    /*
     * TPs player to location - this could live somewhere else ? Maybe in Consts or PlayerMovement ?
     */
    public void teleportPlayer(Location loc)
    {
        Debug.Log($"SaveAndLoader - Teleporting to ({loc.x}, {loc.y})...");
        transform.position = new Vector2(loc.x, loc.y);
    }
}
