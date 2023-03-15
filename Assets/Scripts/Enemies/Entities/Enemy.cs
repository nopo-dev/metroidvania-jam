using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : CollidableArea
{
    [SerializeField] public EnemyType type = null;
    [SerializeField] public Vector2 patrolA;
    [SerializeField] public Vector2 patrolB;
    [SerializeField] protected Vector2 startDirection;
    [SerializeField] protected string _id;
    [SerializeField] protected bool _respawns = true;

    // kinda gross that these live here, they're only used by patroller navigator
    [HideInInspector] public Vector2 startLoc;
    [HideInInspector] public Vector2 destinationLoc;

    [HideInInspector] protected Attacker _attacker = null;
    [HideInInspector] protected NavManager _navManager = null;
    [HideInInspector] protected int _currentHP;
    [HideInInspector] protected bool _collidable;

    [HideInInspector] public GameObject player;
    [HideInInspector] public EnemyMove mover;
    [HideInInspector] public Fly flyer;


    protected void Awake()
    {
        _collidable = true;
        flyer = GetComponent<Fly>();
        mover = GetComponent<EnemyMove>();
    }

    protected void Start()
    {
        startLoc = transform.position;
        destinationLoc = patrolA;
        _currentHP = type.maximumHP;
        _attacker = type.attacker;
        _navManager = type.navManager;
        player = GameObject.FindWithTag("Player");
        ignorePlayerCollision();
        _navManager.startNav(this, startDirection);
    }

    public bool inAttackRange()
    {
        return _attacker.inRange(this);
    }

    public void attack(Action callback)
    {
        _attacker.attack(this, callback);
    }

    public void doPeacefulNav()
    {
        _navManager.doPeacefulNav(this);
    }

    public void chargePlayer(float chargeSpeed)
    {
        _navManager.chargePlayer(this, chargeSpeed);
    }

    public void standStill()
    {
        _navManager.standStill(this);
    }

    public void chasePlayer(float chargeSpeed)
    {
        _navManager.chasePlayer(this, chargeSpeed);
    }

    public void moveTowards(Vector3 target, float speed=1)
    {
        _navManager.moveTowards(this, target, speed);
    }

    public void facePlayer()
    {
        _navManager.facePlayer(this);
    }

    public bool noHP()
    {
        return _currentHP <= 0;
    }

    public void playDyingAnimation(Action callback)
    {
        StopAllCoroutines();
        _collidable = false;

        StartCoroutine(dyingAnimation(callback));
    }

    protected virtual IEnumerator dyingAnimation(Action callback)
    {
        throw new NotImplementedException("This enemy has no dying animation.");
    }

    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag != "Player" || !_collidable) { return; }

        PlayerStatus.Instance.HPManager.damageHP(type.damageOnTouch);

        // TODO: handle player attacks
    }

    protected void damageHP(int damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            kill();
        }
    }

    protected virtual void kill()
    {
         if (!_respawns)
        {
            SaveAndLoader.Instance.EnemySaveManager.markEnemyKilled(_id);
        }
    }

    protected void hide()
    {
        GetComponent<Renderer>().enabled = false;
        this.gameObject.SetActive(false);
    }

    private void ignorePlayerCollision()
    {
        var playerColliders = player.GetComponents<Collider2D>();
        var ownColliders = GetComponents<Collider2D>();
        foreach(Collider2D playerCollider in playerColliders)
        {
            foreach(Collider2D ownCollider in  ownColliders)
            {
                Physics2D.IgnoreCollision(playerCollider, ownCollider);
            }
        }
    }

    /*
     * Enemies should naturally respawn.
     * We just need to manually hide defeated bosses.
     */
    public static void hideEnemies(string[] ids)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (ids.Contains(enemy._id))
            {
                enemy.hide();
            }
        }
    }
}