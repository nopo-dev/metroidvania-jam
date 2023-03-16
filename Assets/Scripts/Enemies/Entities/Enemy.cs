using System;
using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector] public Animator animator;
    [HideInInspector] public Dictionary<string, float> animationDurations;

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
        animator = GetComponent<Animator>();
        animationDurations = new Dictionary<string, float>();
    }

    protected void Start()
    {
        startLoc = transform.position;
        destinationLoc = patrolA;
        _currentHP = type.maximumHP;
        _attacker = type.attacker;
        _navManager = type.navManager;
        player = GameObject.FindWithTag("Player");
        _navManager.startNav(this, startDirection);
        getAnimationTimes();
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
        kill();

        StartCoroutine(dyingAnimation(callback));
    }

    protected virtual IEnumerator dyingAnimation(Action callback)
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(animationDurations["Death"]);
        hide();
    }

    protected override void collisionHandler(Collider2D other)
    {
        if (!_collidable) { return; }
        
        switch(other.tag)
        {
            case "PlayerTrigger":
                // TODO: animate player taking damage / knock-back
                PlayerStatus.Instance.HPManager.damageHP(type.damageOnTouch);
                break;
            case "PlayerMeleeAttack":
                Debug.Log($"{type.name} - Hit by melee attack");
                this.damageHP(Consts.PLAYER_MELEE_ATTACK_DAMAGE);
                PlayerStatus.Instance.EnergyManager.healEnergy(Consts.ENERGY_HEAL_ONHIT);
                break;
            case "PlayerRangedAttack":
                Debug.Log($"{type.name} - Hit by Ranged attack");
                this.damageHP(Consts.PLAYER_RANGED_ATTACK_DAMAGE);
                break;
            default:
                break;
        }
    }

    protected void damageHP(int damage)
    {
        _currentHP -= damage;
    }

    protected virtual void kill()
    {
        _collidable = false;
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

    private void getAnimationTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            animationDurations.Add(clip.name, clip.length);
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