using UnityEngine;

/*
 * This singleton class communicates between player state and SaveDataManager
 */
public class SaveAndLoader : MonoBehaviour
{
    public static SaveAndLoader Instance { get; private set; }

    // TODO: best way to get key downs?
    private static KeyCode saveKey = KeyCode.S;
    private static KeyCode loadKey = KeyCode.L;
    private static KeyCode restartKey = KeyCode.R;

    /*
    * Initialize the singleton instance if it does not already exist.
    * If it exists & is different from current instance, destroy this instance.
    */
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one SaveAndLoader!");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SaveDataManager.initSaveDataManager(); // SaveDataManager is static, we don't need to own it.
        load();
    }

    // TODO: is this really the best way to check for save/load clicks?
    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }

        // Save take prio over load
        if (Input.GetKeyDown(saveKey))
        {
            Debug.Log("SaveAndLoader - Manual save is for debug purposes, not guaranteed to be soft-lock-safe.");
            save();
        }
        else if (Input.GetKeyDown(loadKey))
        {
            load();
        }
        else if (Input.GetKeyDown(restartKey))
        {
            restart();
        }
    }

    /*
     * Create a SaveData based on current player state
     * This should only be called by SaveAndLoader.Instance.save(), which guarantees nonzero HP.
     */
    private SaveData getSaveData()
    {
        return new SaveData(
            PlayerStatus.Instance.HPManager.getCurrentHP(),
            PlayerStatus.Instance.HPManager.getMaximumHP(),
            PlayerStatus.Instance.EnergyManager.getCurrentEnergy(),
            PlayerStatus.Instance.EnergyManager.getMaximumEnergy(),
            PlayerStatus.Instance.UpgradeManager.getUpgrade(),
            PlayerStatus.Instance.LastSaveLocManager.getLastSaveLoc()
        );
    }

    /*
     * Save the game
     *  - player HP, current and max
     *  - player energy, current and max
     *  - player upgrades
     *  - last save loc
     *  - which enemies/bosses dead (might be inferrable from upgrades)
     */
    internal void save() // TODO: This could be public, but for now only SafeArea and self should be calling it.
    {
        if (PlayerStatus.Instance.HPManager.isOutOfHP())
        {
            Debug.Log("SaveAndLoader - Cannot save at 0 HP");
            return;
        }
        Debug.Log("SaveAndLoader - Saving game...");
        var saveData = getSaveData();
        SaveDataManager.writeSaveData(saveData);
    }

    /*
     * Load the game from latest save
     */
    public void load()
    {
        Debug.Log("SaveAndLoader - Loading game...");
        var saveData = SaveDataManager.readSaveData();

        // TODO: need to sanitize this at all? e.g. 0 hp. Probably need to do more processing than just setting, e.g. teleport to location, set camera.
        // But those can be done by Managers.
        // Max needs to be set before current.

        // Step 1: Load raw values. Some UI updates happen as a consequence of these, e.g. setMaximumHP calls updateUI hook.
        Debug.Log("SaveAndLoader - Loading raw values...");
        PlayerStatus.Instance.HPManager.setMaximumHP(saveData.playerMaxHP);
        PlayerStatus.Instance.HPManager.setCurrentHP(saveData.playerCurrentHP);
        PlayerStatus.Instance.EnergyManager.setMaximumEnergy(saveData.playerMaxEnergy);
        PlayerStatus.Instance.EnergyManager.setCurrentEnergy(saveData.playerCurrentEnergy);
        PlayerStatus.Instance.UpgradeManager.setUpgrade(saveData.playerUpgrades); // TODO: Hide earned upgrades when loading.
        PlayerStatus.Instance.LastSaveLocManager.setLastSaveLoc(saveData.lastSaveLoc);
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(saveData.lastSaveLoc); // When loading a save, last safe loc = last save loc, as fallback.

        // Step 2: Non-raw-value updates, such as moving the player, hide/showing upgrade items, respawn enemies.
        SceneLoader.Instance.loadScene(saveData.lastSaveLoc);
        PlayerStatus.Instance.UpgradeManager.applyUpgradeItemState();
        // TODO: respawn enemies.
    }

    /*
     * Resets save data for a new game
     * TODO: "Are you sure" prompt, will override old save
     */
    internal void restart()
    {
        Debug.Log("SaveAndLoader - Restarting game...");
        SaveDataManager.writeNewGameSaveData();
        load();
    }
}