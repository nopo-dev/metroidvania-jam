using static Consts;
using UnityEngine;

[System.Serializable]
/*
 * This is what gets saved
 */
internal struct SaveData
{
    public int playerCurrentHP;
    public int playerMaxHP; // TODO: tied to upgrades ?
    public int playerCurrentEnergy;
    public int playerMaxEnergy; // TODO: tied to upgrades ?
    public Upgrade playerUpgrades;
    public Location lastSaveLoc;

    public SaveData(int playerCurrentHP, int playerMaxHP, int playerCurrentEnergy, int playerMaxEnergy, Upgrade playerUpgrades, Location lastSaveLoc)
    {
        this.playerCurrentHP = playerCurrentHP;
        this.playerMaxHP = playerMaxHP;
        this.playerCurrentEnergy = playerCurrentEnergy; 
        this.playerMaxEnergy = playerMaxEnergy;
        this.playerUpgrades = playerUpgrades;
        this.lastSaveLoc = lastSaveLoc;
    }

    /*
     * Generates a SaveData for a new game.
     */
    public static SaveData getNewGameSaveData()
    {
        // TODO: default new save data
        // TODO: defaults should be defined somewhere else
        var startingSaveArea = SaveArea.StartingSaveArea;
        if (startingSaveArea == null)
        {
            Debug.Log("SaveData - No starting save area set. New game will be saved to (0, 0).");
            return new SaveData(STARTING_PLAYER_HP, STARTING_PLAYER_HP, STARTING_PLAYER_ENERGY, STARTING_PLAYER_ENERGY, Upgrade.Base, new Location(0, 0, ""));
        }
        return new SaveData(STARTING_PLAYER_HP, STARTING_PLAYER_HP, STARTING_PLAYER_ENERGY, STARTING_PLAYER_ENERGY, Upgrade.Base, startingSaveArea.getSpawnLocation());
    }

    public override string ToString()
    {
        return $"HP: {this.playerCurrentHP} / {this.playerMaxHP}, energy: {this.playerMaxHP} / {this.playerMaxEnergy}, upgrades: {this.playerUpgrades}, lastSaveLoc: ({this.lastSaveLoc.x}, {this.lastSaveLoc.y}, {this.lastSaveLoc.sceneName})"; // TODO: reads to nice string ?
    }
}