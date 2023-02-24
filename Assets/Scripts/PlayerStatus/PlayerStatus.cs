using UnityEngine;
using TMPro;

/*
 * This will be a singleton, there should only be one PlayerStatus
 * (It probably doesn't matter too much but I'm playing around w/ design patterns)
 * Handles the following
 *  - HP
 *  - Energy
 *  - Upgrades
 *  - Last save loc
 */
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    // TODO: these managers should handle status bar stuff too.
    public HPManager HPManager { get; private set; }
    public EnergyManager EnergyManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }
    public LastSaveLocManager LastSaveLocManager { get; private set; }

    // TODO: this is temporary, for testing/debugging
    public TMP_Text healthText;
    public TMP_Text energyText;

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
    }
}
