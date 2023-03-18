using System.Collections.Generic;
using System.Linq;

/*
 * Only meant to track non-respawning enemies
 * for respawn purposes
 */
public class EnemySaveManager
{
    private List<string> _killedEnemies;

    public void markEnemyKilled(string enemyID)
    {
        _killedEnemies.Add(enemyID);
    }

    public void setKillList(string[] killList)
    {
        _killedEnemies = killList.ToList();
    }

    public string[] getKillList()
    {
        return _killedEnemies.ToArray();
    }

    public bool bossKilled()
    {
        return _killedEnemies.Contains("snailman");
    }
}
