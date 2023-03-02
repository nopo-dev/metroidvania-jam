using System;
using UnityEngine;

/*
 * Manages current player upgrade / abilities
 */
public class UpgradeManager
{
    private Upgrade currentUpgrade_;

    public Upgrade getUpgrade()
    {
        return this.currentUpgrade_;
    }

    /*
     * Sets upgrade level. Since upgrades are linear,
     * this implies access to all previous upgrades as well.
     */
    public void setUpgrade(Upgrade newUpgrade)
    {
        if (newUpgrade - this.currentUpgrade_ != 1)
        {
            Debug.Log("UpgradeManager - nonconsecutive upgrading. This is common when loading a save.");
        }
        this.currentUpgrade_ = newUpgrade;
    }

    /*
     * Hide UpgradeItems previous to current upgrade (inclusive).
     * Show UpgradeItems after current upgrade (exclusive).
     * TODO: Find a better place for this fn to live.
     */
    public void applyUpgradeItemState()
    {
        Debug.Log("UpgradeManager - Hiding & showing UpgradeItems based on Player's current upgrade level");
        int currentUpgradeIndex = (int) currentUpgrade_;
        for (int i = 1; i < UpgradeItem.numberOfUpgrades; i++) // Loop starts at 1 because there should be no Upgrade 0.
        {
            if (i <= currentUpgradeIndex)
            {
                UpgradeItem.hideUpgradeItem((Upgrade) i);
            }
            else
            {
                UpgradeItem.showUpgradeItem((Upgrade) i);
            }
        }
    }
    
    /*
     * Player has access to all upgrades up to current level.
     */
    public Boolean hasUpgrade(Upgrade upgrade)
    {
        return upgrade <= this.currentUpgrade_;
    }
}
