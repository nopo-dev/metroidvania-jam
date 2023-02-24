using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * This will be a singleton, there should only be one PlayerStatus
 * (It probably doesn't matter too much but I'm playing around w/ design patterns)
 */
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }
    public HPManager HPManager { get; private set; }
    public EnergyManager EnergyManager { get; private set; }
    public UpgradeManager UpgradeManager { get; private set; }
    public LastSaveLocManager LastSaveLocManager { get; private set; }

    public TMP_Text healthText;

    /*
     * Initialize the singleton instance if it does not already exist.
     * If it exists & is different from current instance, destroy this instance.
     */
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        this.HPManager = new HPManager();
        this.EnergyManager = new EnergyManager();
        this.UpgradeManager = new UpgradeManager();
        this.LastSaveLocManager = new LastSaveLocManager();
    }

    // TODO: These are for debug/testing purposes. Necessary? Can HPManager be accessed from inspector ?
    public int playerCurrentEnergy_debug = 100;
    public int playerMaxEnergy_debug = 100;
    public Upgrade playerUpgrade_debug;
    public Location lastSaveLoc_debug; // TODO: this one doesn't show in inspector.
    private void Update()
    {
        this.healthText.text = "HP: " + HPManager.getCurrentHP() + "/" + HPManager.getMaximumHP();

        EnergyManager.setCurrentEnergy(playerCurrentEnergy_debug);
        EnergyManager.setMaximumEnergy(playerMaxEnergy_debug);
        if (UpgradeManager.getUpgrade() != playerUpgrade_debug)
        {
            UpgradeManager.setUpgrade(playerUpgrade_debug);
        }
        LastSaveLocManager.setLastSaveLoc(lastSaveLoc_debug);
    }
}
