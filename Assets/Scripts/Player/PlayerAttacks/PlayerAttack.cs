using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    protected bool _enabled;
    public Upgrade upgrade { get; protected set; } = Upgrade.Base;

    public virtual void enable(bool enabled)
    {
        _enabled = enabled;
    }
}