using System.IO;
using UnityEngine;

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
        writeNewGameSaveData();
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
        writeSaveData(SaveAndLoad.getNewSave());
    }
}
