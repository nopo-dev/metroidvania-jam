using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Types/Enemy Type")]
public class EnemyType : ScriptableObject
{
    [SerializeField] public Attacker attacker = null;
    [SerializeField] public NavManager navManager = null;
    [SerializeField] public int maximumHP;
    [SerializeField] public int damageOnTouch;
}