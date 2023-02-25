using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] private float spawnLocationX_; 
    [SerializeField] private float spawnLocationY_;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(new Location(spawnLocationX_,spawnLocationY_));
    }
}   
