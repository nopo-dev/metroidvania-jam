using UnityEngine;

/*
 * This is a (maybe temporary) enemy object,
 * for testing HP management and save/load.
 * Be careful about inheriting it.
 */
public class Enemy : CollidableArea
{
    // TODO: enemy hp manager ?
    // TODO: FSM for is hit, attacking, dying, go back to idle, etc. things
    [SerializeField] private int _maximumHP;
    [SerializeField] private int _damage;
    [SerializeField] private bool _respawns = true;

    private int _currentHP;
    private bool _killed; // this won't persist between scenes
    private bool _aggroed;

    private void Awake()
    {
        _currentHP = _maximumHP;
        _killed = false;
        _aggroed = false;
    }

    private void Update()
    {
        // Check for player & aggro
    }

    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        PlayerStatus.Instance.HPManager.damageHP(_damage);

        // TODO: player attacks rather than collide w/ upgrade
        if (PlayerStatus.Instance.UpgradeManager.hasUpgrade(Upgrade.MeleeAttack))
        {
            damageHP(1);
        }
    }

    private void damageHP(int damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            Debug.Log("Enemy died");
            kill();
        }
    }

    private void kill()
    {
        GetComponent<Renderer>().enabled = false;
        this.gameObject.SetActive(false);
        _killed = true;
    }

    private void revive()
    {
        GetComponent<Renderer>().enabled = true;
        this.gameObject.SetActive(false);
        _killed = false;
    }

    public static void respawnEnemies() // TODO: even if we can extend this for regular enemies, not sure abt bosses
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (enemy._killed && !enemy._respawns)
            {
                enemy.kill();
            }
            else
            {
                enemy.revive();
            }
        }
    }

}
