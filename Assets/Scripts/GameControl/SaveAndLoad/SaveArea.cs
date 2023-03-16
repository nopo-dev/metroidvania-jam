using UnityEngine;

/**
 * Used for Save Areas.
 */
public class SaveArea : CollidableArea
{
    [SerializeField] private bool isStartingSaveArea; // This should be toggleable in inspector.

    // TODO: expose this to inspector for non-centered spawn points.
    // TODO: standardize notation for private vars, probably _before
    private Location spawnPoint_;
    private Animator _animator;

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
        spawnPoint_.sceneName = SceneLoader.getCurrentSceneName();
        spawnPoint_.x = this.transform.position.x;
        spawnPoint_.y = this.transform.position.y;
        _animator = GetComponent<Animator>();
        if (SaveAndLoader.Instance.LastSaveLocManager.Compare(this.spawnPoint_))
            _animator.SetBool("Last Save", true);
    }

    internal Location getSpawnLocation()
    {
        return this.spawnPoint_;
    }

    private void doSave()
    {
        Debug.Log("SaveArea - Marking last save loc...");
        SaveAndLoader.Instance.LastSaveLocManager.setLastSaveLoc(this.spawnPoint_); 
        _animator.SetBool("LastSave", true);
        Debug.Log("SaveArea - Healing to full...");
        PlayerStatus.Instance.HPManager.healToFull();
        SaveAndLoader.Instance.save();
        _animator.SetTrigger("Activate");
    }
    protected override void collisionHandler(Collider2D other)
    {
        doSave();
    }
}
