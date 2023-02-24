using System;
using UnityEngine;

/**
 * Used for Save Areas. Relies on Collider+RigidBody setup.
 */
public class SaveArea : MonoBehaviour
{
    public Boolean isStartingSaveArea; // This should be toggleable in inspector.
    internal static SaveArea StartingSaveArea { get; private set; } // There can only be one !
    internal Location spawnLocation;

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

    private void doSave()
    {
        PlayerStatus.Instance.LastSaveLocManager.setLastSaveLoc(this.spawnLocation);
        SaveAndLoader.Instance.save();
    }
    private void OnTriggerEnter2D(Collider2D other) // TODO: currently using built-in collision detection, this can be updated.
    {
        doSave();
    }
}
