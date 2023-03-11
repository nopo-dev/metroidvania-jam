using UnityEngine;

// TODO: a little awkward that peaceul nav lives in NavManager and attack nav lives in Attacker
public abstract class NavManager : MonoBehaviour
{
    public abstract void doPeacefulNav();
}