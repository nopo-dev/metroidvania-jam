using System;
using UnityEngine;

/*
 * Instance of an Upgrade Item:
 *  - Touched by player to obtain upgrade
 *  - There should only be one UpgradeItem for each Upgrade,
 *    and no Upgrade 0.
 */
public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private Upgrade upgradeID_; // set in inspector.

    public static int numberOfUpgrades = Enum.GetNames(typeof(Upgrade)).Length;
    private static UpgradeItem[] upgrades_ = new UpgradeItem[numberOfUpgrades];

    /*
     * Set up static upgrades_ array so we can access UpgradeItems by Upgrade
     */
    private void Awake()
    {
        if (upgrades_ == null)
        {
            Debug.Log("UpgradeItem - upgrades were not initialized by constructor. Unexpected state, returning early...");
            return;
        }
        if (upgrades_[(int)this.upgradeID_] != null && upgrades_[(int)this.upgradeID_] != this) // Theoretically this is vulnerable to a race condition,
                                                                                  // if two of the same upgradeID are registered at the same time.
                                                                                  // Should be ok since this is a safety net anyways.
        {
            Debug.Log($"UpgradeItem - Cannot have multiple instances of UpgradeID {this.upgradeID_}. Keeping first instance.");
            return;
        }
        if (this.upgradeID_ == 0) // There shouldn't be an item for Upgrade 0, assumed to be Base.
        {
            Debug.Log($"UpgradeItem - Unexpected instance of Upgrade 0 {this.upgradeID_} not saved to upgrades_ array.");
            return;
        }
        upgrades_[(int)this.upgradeID_] = this;
    }


    /*
     * When triggered, we need to set the new upgrade level on PlayerStatus, and hide this UpgradeItem.
     * TODO: Show UI message on how to use upgrade ?
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"UpgradeItem - Obtaining upgrade {this.upgradeID_}");
        PlayerStatus.Instance.UpgradeManager.setUpgrade(this.upgradeID_);
        hide();
    }

    private void hide()
    {
        GetComponent<Renderer>().enabled = false;
        this.gameObject.SetActive(false);
    }

    // TODO: Should unearned upgradeItems always be shown/active ?
    private void show()
    {
        GetComponent<Renderer>().enabled = true;
        this.gameObject.SetActive(true);
    }    

    /*
     * Once an upgrade is obtained, use this to hide it.
     */
    public static void hideUpgradeItem(Upgrade upgradeID)
    {
        UpgradeItem upgradeItem = upgrades_[(int) upgradeID];
        if (upgradeItem == null)
        {
            Debug.Log($"UpgradeItem - Cannot hide missing UpgradeItem instance for Upgrade {upgradeID}");
            return;
        }
        else
        {
            upgradeItem.hide();
        }
    }
    
    public static void showUpgradeItem(Upgrade upgradeID)
    {
        UpgradeItem upgradeItem = upgrades_[(int)upgradeID];
        if (upgradeItem == null)
        {
            Debug.Log($"UpgradeItem - Cannot show missing UpgradeItem instance for Upgrade {upgradeID}");
            return;
        }
        else
        {
            upgradeItem.show();
        }
    }
}
