using System;
using UnityEngine;

/*
 * Manages current and maximum (player) energy.
 * Current energy is clamped from 0 to maximum energy.
 */
public class EnergyManager
{
    private int currentEnergy_;
    private int maximumEnergy_;

    public EnergyManager(int maximumEnergy = Consts.STARTING_PLAYER_ENERGY)
    {
        this.currentEnergy_ = maximumEnergy;
        this.maximumEnergy_ = maximumEnergy;
        updateUI();
    }

    public int getCurrentEnergy()
    {
        return this.currentEnergy_;
    }

    public void setCurrentEnergy(int newCurrentEnergy)
    {
        this.currentEnergy_ = Utils.Clamp(newCurrentEnergy, 0, this.maximumEnergy_);
    }

    public void healEnergy(int healAmount)
    {
        Debug.Log($"EnergyManager - Recovering for {healAmount}.");
        setCurrentEnergy(this.currentEnergy_ + healAmount);
    }

    public void damageEnergy(int damageAmount)
    {
        Debug.Log($"EnergyManager - Damaging for {damageAmount}");
        setCurrentEnergy(this.currentEnergy_ - damageAmount);
    }

    public int getMaximumEnergy()
    {
        return this.maximumEnergy_;
    }

    public void setMaximumEnergy(int newMaximumEnergy)
    {
        if (newMaximumEnergy <= 0)
        {
            Debug.Log("Max Energy must be a positive int.");
            return;
        }
        this.maximumEnergy_ = newMaximumEnergy;
    }

    public Boolean isOutOfEnergy()
    {
        return this.maximumEnergy_ <= 0; // TODO: If we have variable energy costs or energy cost that doesn't evenly divide total energy, this needs to change.
    }

    private void updateUI()
    {
        PlayerStatus.Instance.energyText.text = $"{this.currentEnergy_} / {this.maximumEnergy_}";
    }
}
