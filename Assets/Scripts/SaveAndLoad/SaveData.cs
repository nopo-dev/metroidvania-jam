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
    public Location lastSaveLoc; // TODO: verify serializable

    public SaveData(int playerCurrentHP, int playerMaxHP, int playerCurrentEnergy, int playerMaxEnergy, Upgrade playerUpgrades, Location lastSaveLoc)
    {
        this.playerCurrentHP = playerCurrentHP;
        this.playerMaxHP = playerMaxHP;
        this.playerCurrentEnergy = playerCurrentEnergy; 
        this.playerMaxEnergy = playerMaxEnergy;
        this.playerUpgrades = playerUpgrades;
        this.lastSaveLoc = lastSaveLoc;
    }

    public override string ToString()
    {
        return $"HP: {this.playerCurrentHP} / {this.playerMaxHP}, energy: {this.playerMaxHP} / {this.playerMaxEnergy}, upgrades: {this.playerUpgrades}, lastSaveLoc: {this.lastSaveLoc}"; // TODO: reads to nice string ?
    }
}