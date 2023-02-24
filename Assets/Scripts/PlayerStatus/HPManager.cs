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
    // Maybe these could be uint, or byte or smth.
    private int currentHP_; // We could use set/get here but I don't like the format, I'd rather call setCurrentHP. Makes it clearer that it might do some processing on the input.
    private int maximumHP_;

    public HPManager(int maximumHP = 100)
    {
        this.currentHP_ = maximumHP;
        this.maximumHP_ = maximumHP;
    }

    public int getCurrentHP()
    {
        return this.currentHP_;
    }

    public void setCurrentHP(int newCurrentHP)
    {
        this.currentHP_ = Utils.Clamp(newCurrentHP, 0, this.maximumHP_);
    }

    public void healHP(int healAmount) // TODO: We could get heal power from player status ?
    {
        Debug.Log($"HPManager - Healing for {healAmount}.");
        setCurrentHP(this.currentHP_ + healAmount);
    }

    public void damageHP(int damageAmount)
    {
        Debug.Log($"HPManager - Damaging for {damageAmount}");
        setCurrentHP(this.currentHP_ - damageAmount);
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
            Debug.Log("Max HP must be a positive int.");
            return;
        }
        this.maximumHP_ = newMaximumHP;
    }

    public Boolean isOutOfHP()
    {
        return this.currentHP_ <= 0; // really this could just check equality, but ig it's safer this way.
    }
    // TODO: if we want to show % we should have a method to get it.
}
