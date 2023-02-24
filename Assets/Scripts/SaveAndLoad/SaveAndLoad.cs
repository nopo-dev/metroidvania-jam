using UnityEngine;

/*
 * This class communicates between player state and SaveDataManager
 * TODO: can this be static / singleton ? maybe depends how player access goes.
 */
internal class SaveAndLoad : MonoBehaviour
{
    // TODO: best way to get key downs?
    private static KeyCode saveKey = KeyCode.S;
    private static KeyCode loadKey = KeyCode.L;
    private static KeyCode restartKey = KeyCode.R;

    void Start()
    {
        SaveDataManager.initSaveDataManager();
        load();
    }

    // TODO: is this really the best way to check for save/load clicks?
    void Update()
    {
        // Save take prio over load
        if (UnityEngine.Input.GetKeyDown(saveKey))
        {
            save();
        }
        else if (UnityEngine.Input.GetKeyDown(loadKey))
        {
            load();
        }
        else if (UnityEngine.Input.GetKeyDown(restartKey))
        {
            restart();
        }
    }

    /*
     * Create a SaveData based on current player state
     * TODO: HP cannot be 0.
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
    private void save()
    {
        Debug.Log("Saving game...");
        var saveData = getSaveData();
        SaveDataManager.writeSaveData(saveData);
    }

    /*
     * Load the game
     */
    private void load()
    {
        Debug.Log("Loading game...");
        var saveData = SaveDataManager.readSaveData();

        // TODO: need to sanitize this at all? e.g. 0 hp. Probably need to do more processing than just setting, e.g. teleport to location, set camera.
        // But those can be done by Managers.
        PlayerStatus.Instance.HPManager.setCurrentHP(saveData.playerCurrentHP);
        PlayerStatus.Instance.HPManager.setMaximumHP(saveData.playerMaxHP);
        PlayerStatus.Instance.EnergyManager.setCurrentEnergy(saveData.playerCurrentEnergy);
        PlayerStatus.Instance.EnergyManager.setMaximumEnergy(saveData.playerMaxEnergy);
        PlayerStatus.Instance.UpgradeManager.setUpgrade(saveData.playerUpgrades);
        PlayerStatus.Instance.LastSaveLocManager.setLastSaveLoc(saveData.lastSaveLoc);
        PlayerStatus.Instance.playerCurrentEnergy_debug = saveData.playerCurrentEnergy;
        PlayerStatus.Instance.playerMaxEnergy_debug = saveData.playerMaxEnergy;
        PlayerStatus.Instance.playerUpgrade_debug = saveData.playerUpgrades;
        PlayerStatus.Instance.lastSaveLoc_debug = saveData.lastSaveLoc;
    }

    /*
     * Resets save data for a new game
     * TODO: "Are you sure" prompt, will override old save
     */
    private void restart()
    {
        SaveDataManager.writeNewGameSaveData();
        load();
    }

    /*
     * Generates a SaveData instance for a new game.
     */
    public static SaveData getNewSave()
    {
        // TODO: default new save data
        // TODO: defaults should be defined somewhere else
        return new SaveData(100, 100, 100, 100, 0, new Location( 0, 0, 0, 0 ));
    }
}