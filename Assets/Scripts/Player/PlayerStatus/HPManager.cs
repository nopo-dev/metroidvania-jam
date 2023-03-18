using System;
using UnityEngine;

/*
 * Manages current and maximum (player) HP.
 * Current HP is clamped from 0 to maximum HP.
 * TODO: player vs enemy HP, could extend from same class ?
 * TODO:'s on this also apply to EnergyManager.
 */
public class HPManager
{ 
    private KnockBack _knockback;
   
    // Maybe these could be uint, or byte or smth.
    private int maximumHP_;
    private int currentHP_; // We could use set/get here but I don't like the format, I'd rather call setCurrentHP. Makes it clearer that it might do some processing on the input.

    public HPManager(int maximumHP = Consts.STARTING_PLAYER_HP)
    {
        this.currentHP_ = maximumHP;
        this.maximumHP_ = maximumHP;
        _knockback = GameObject.FindWithTag("Player").GetComponent<KnockBack>();
    }

    public int getCurrentHP()
    {
        return this.currentHP_;
    }

    /*
     * Sets current HP, including UI and out-of-HP check
     */
    public void setCurrentHP(int newCurrentHP)
    {
        this.currentHP_ = (int) Utils.Clamp(newCurrentHP, 0, this.maximumHP_);
        UIDisplay.Instance.updateHP(this.currentHP_);

        if (isOutOfHP())
        {
            // TODO 
            Debug.Log("HPManager - Out of HP, loading latest save.");
            SaveAndLoader.Instance.load();
        }
    }

    public void healHP(int healAmount) // TODO: We could get heal power from player status ?
    {
        Debug.Log($"HPManager - Healing for {healAmount}.");
        setCurrentHP(this.currentHP_ + healAmount);
    }

    public void damageHP(int damageAmount, Vector2 damager)
    {
        Debug.Log($"HPManager - Damaging for {damageAmount}");
        AudioManager.Instance.PlaySound("PlayerDamage");
        setCurrentHP(this.currentHP_ - damageAmount);
        _knockback.knockPlayer(damager);
    }

    public void healToFull()
    {
        setCurrentHP(this.maximumHP_);
    }

    public int getMaximumHP()
    {
        return this.maximumHP_;
    }

    public void setMaximumHP(int newMaximumHP)
    {
        if (newMaximumHP <= 0)
        {
            Debug.Log("HPManager - Max HP must be a positive int.");
            return;
        }
        this.maximumHP_ = newMaximumHP;
    }

    public bool isOutOfHP()
    {
        return this.currentHP_ <= 0; // really this could just check equality, but ig it's safer this way.
    }
    // TODO: if we want to show % we should have a method to get it.
}
