using System;
using UnityEngine;

/**
 * Used for Save Areas. Relies on Collider+RigidBody setup.
 */
public class SaveArea : MonoBehaviour
{
    [SerializeField] private Boolean isStartingSaveArea; // This should be toggleable in inspector.
    [SerializeField] private float spawnLocationX_; // These are set in inspector. We could also use the center of the Save Area, but that might not work in all cases.
    [SerializeField] private float spawnLocationY_;

    internal static SaveArea StartingSaveArea { get; private set; } // There can only be one !
    

    private void Awake()
    {
        if (this.isStartingSaveArea)
        {
            if (StartingSaveArea != null && StartingSaveArea != this)
            {
                Debug.Log("SafeArea - Can only have one starting save area!");
                Destroy(this);
                return;
            }
            StartingSaveArea = this;
        }
    }
    internal Location getSpawnLocation()
    {
        return new Location(this.spawnLocationX_, this.spawnLocationY_);
    }

    private void doSave()
    {
        Debug.Log("SaveArea - Marking last save loc...");
        PlayerStatus.Instance.LastSaveLocManager.setLastSaveLoc(this.getSpawnLocation());
        Debug.Log("SaveAndLoader - Healing to full...");
        PlayerStatus.Instance.HPManager.healToFull();
        SaveAndLoader.Instance.save();
    }
    private void OnTriggerEnter2D(Collider2D other) // TODO: currently using built-in collision detection, this can be updated.
    {
        doSave();
    }
}
