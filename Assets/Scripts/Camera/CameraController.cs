using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float offsetSmoothing = 40f; // Makes the camera move faster to follow the player
    private Vector3 playerPosition;
    
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // TODO: need to clamp to scene boundaries
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * offsetSmoothing);
    }
}