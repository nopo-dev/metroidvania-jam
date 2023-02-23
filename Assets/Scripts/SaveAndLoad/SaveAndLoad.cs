using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
internal struct SaveData
{
    public int playerHP;
    public int playerEnergy;
    public int playerUpgrades;
    public int lastSaveLoc;

    public SaveData(int playerHP, int playerEnergy, int playerUpgrades, int lastSaveLoc)
    {
        this.playerHP = playerHP;
        this.playerEnergy = playerEnergy;
        this.playerUpgrades = playerUpgrades;
        this.lastSaveLoc = lastSaveLoc;
    }

    public override string ToString()
    {
        return $"HP: {this.playerHP}, energy: {this.playerEnergy}, upgrades: {this.playerUpgrades}, lastSaveLoc: {this.lastSaveLoc}";
    }
}

/*
 * This class communicates SaveData to/from file system.
 */
internal static class SaveDataManager
{
    private static string saveFile;

    public static void initSaveDataManager()
    {
        saveFile = UnityEngine.Application.persistentDataPath + "/gamedata.json";
        Debug.Log("Save file location at " + saveFile);

        if (!File.Exists(saveFile))
        {
            Debug.Log("Save file does not exist, creating a new one...");
            writeNewGameSaveData();
        }
    }

    /*
     * Generates a SaveData instance for a new game.
     */
    private static SaveData getNewSave()
    {
        // TODO: default new save data
        // TODO: defaults should be defined somewhere else
        return new SaveData(100, 100, 0, 0);
    }

    /*
     * Returns a SaveData out of saveFile location, assuming it exists.
     */
    public static SaveData readSaveData()
    {
        if (!File.Exists(saveFile))
        {
            Debug.Log("Save file does not exist, unexpected state."); // TODO: crash here ?
        }
        string fileContents = File.ReadAllText(saveFile);
        SaveData saveData = JsonUtility.FromJson<SaveData>(fileContents);
        Debug.Log("Loaded data " + saveData);
        return saveData;
    }

    /*
     * Writes saveData into saveFile as a JSON string
     */
    public static void writeSaveData(SaveData saveData)
    {
        string saveJsonString = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFile, saveJsonString);
        Debug.Log("Wrote data " + saveData);
    }

    /*
     * Writes a new game SaveData
     */
    public static void writeNewGameSaveData()
    {
        Debug.Log("Writing save data for a new game.");
        writeSaveData(getNewSave());
    }
}

/*
 * This class communicates between player state and SaveDataManager
 * TODO: can this be static ? maybe depends how player access goes.
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
    // TODO: this is temp for getting player data
    [SerializeField] public int HP = 100;
    [SerializeField] public int energy = 100;
    [SerializeField] public int upgrades = 0;
    [SerializeField] public int lastSaveLoc = 0;
    //private FakePlayer player = new FakePlayer();
    /*
     * A set of functions to return player save data
     * TODO: real functions for these. Some might get stored in other ways,
     *       but should still be serializable / to-int-able
     */
    private int getPlayerHP()
    {
        return HP;
    }
    private int getPlayerEnergy()
    {
        return energy;
    }
    private int getPlayerUpgrades()
    {
        return upgrades;
    }
    private int getLastSaveLoc()
    {
        return lastSaveLoc;
    }

    /*
     * Create a SaveData based on current player state
     */
    private SaveData getSaveData()
    {
        return new SaveData(
            getPlayerHP(),
            getPlayerEnergy(),
            getPlayerUpgrades(),
            getLastSaveLoc()
        );
    }

    /*
     * Save the game
     *  - player HP
     *  - player energy ?
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

        // TODO: apply the save data to the player
        HP = saveData.playerHP;
        energy = saveData.playerEnergy;
        upgrades = saveData.playerUpgrades;
        lastSaveLoc = saveData.lastSaveLoc;
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
}