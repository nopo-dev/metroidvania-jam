/*
 * Enum of player upgrades.
 * These need to be listed in the order they are obtained (we assume linear).
 * Assumed to be backed by int for upgradeID in UpgradeItem.
 */
public enum Upgrade
{
    // TODO: finalize upgrades
    Base,
    MeleeAttack,
    RangedAttack
}