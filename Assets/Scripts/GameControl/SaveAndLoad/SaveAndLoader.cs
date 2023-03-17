using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.Rendering.DebugUI;

/*
 * This singleton class communicates between player state and SaveDataManager
 * TODO: Async save & loading
 */
public class SaveAndLoader : MonoBehaviour
{
    public static SaveAndLoader Instance { get; private set; }

    public LastSaveLocManager LastSaveLocManager { get; private set; }
    public LastSafeLocManager LastSafeLocManager { get; private set; }
    public EnemySaveManager EnemySaveManager { get; private set; }

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
        this.LastSaveLocManager = new LastSaveLocManager();
        this.LastSafeLocManager = new LastSafeLocManager();
        this.EnemySaveManager = new EnemySaveManager();
    }

    private void Start()
    {
        SaveDataManager.initSaveDataManager(); // SaveDataManager is static, we don't need to own it.
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
            this.LastSaveLocManager.getLastSaveLoc(),
            this.EnemySaveManager.getKillList()
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

        // Step 1: Load scene
        SceneLoader.Instance.loadScene(saveData.lastSaveLoc, true, () =>
        {
            // Step 2: Load raw values. Some UI updates happen as a consequence of these, e.g. setMaximumHP calls updateUI hook.

            Debug.Log("SaveAndLoader - Loading raw values...");
            PlayerStatus.Instance.HPManager.setMaximumHP(saveData.playerMaxHP);
            PlayerStatus.Instance.HPManager.setCurrentHP(saveData.playerCurrentHP);
            PlayerStatus.Instance.EnergyManager.setMaximumEnergy(saveData.playerMaxEnergy);
            PlayerStatus.Instance.EnergyManager.setCurrentEnergy(saveData.playerCurrentEnergy);
            PlayerStatus.Instance.UpgradeManager.setUpgrade(saveData.playerUpgrades); // TODO: Hide earned upgrades when loading.
            this.LastSaveLocManager.setLastSaveLoc(saveData.lastSaveLoc);
            this.LastSafeLocManager.setLastSafeLoc(saveData.lastSaveLoc); // When loading a save, last safe loc = last save loc, as fallback.
            this.EnemySaveManager.setKillList(saveData.bossesKilled);
        });
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