using System;
using UnityEngine;

/**
 * Used for Save Areas.
 */
public class SaveArea : CollidableArea
{
    [SerializeField] private Boolean isStartingSaveArea; // This should be toggleable in inspector.
    [SerializeField] private Location spawnPoint_;

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

    private void Start()
    {
        // sceneName will be exposed in inspector but we don't need to change it
        spawnPoint_.sceneName = SceneLoader.Instance.getCurrentSceneName();
    }

    internal Location getSpawnLocation()
    {
        return this.spawnPoint_;
    }

    private void doSave()
    {
        Debug.Log("SaveArea - Marking last save loc...");
        PlayerStatus.Instance.LastSaveLocManager.setLastSaveLoc(this.spawnPoint_);
        Debug.Log("SaveAndLoader - Healing to full...");
        PlayerStatus.Instance.HPManager.healToFull();
        SaveAndLoader.Instance.save();
    }
    protected override void collisionHandler(Collider2D other)
    {
        doSave();
    }
}
