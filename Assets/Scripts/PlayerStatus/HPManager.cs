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

    public HPManager(int maximumHP = Consts.STARTING_PLAYER_HP)
    {
        this.currentHP_ = maximumHP;
        this.maximumHP_ = maximumHP;
        updateUI();
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
        this.currentHP_ = Utils.Clamp(newCurrentHP, 0, this.maximumHP_);
        updateUI();

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
            Debug.Log("HPManager - Max HP must be a positive int.");
            return;
        }
        this.maximumHP_ = newMaximumHP;
        updateUI();
    }

    public Boolean isOutOfHP()
    {
        return this.currentHP_ <= 0; // really this could just check equality, but ig it's safer this way.
    }
    // TODO: if we want to show % we should have a method to get it.

    // TODO: Following fns can live in some UI class
    private void updateUI()
    {
        PlayerStatus.Instance.healthText.text = $"{this.currentHP_} / {this.maximumHP_}";
    }
}
