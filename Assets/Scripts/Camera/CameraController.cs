using static Consts;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float offsetSmoothing = 40f; // Makes the camera move faster to follow the player
    private Vector3 playerPosition;
    private Vector3 clampedPosition;

    public Tilemap tilemap;
    private Bounds bounds;
    private float _xmin;
    private float _xmax;
    private float _ymin;
    private float _ymax;
    
    void Start()
    {
        bounds = tilemap.localBounds;


        _xmin = bounds.min.x + SCENE_X_OFFSET;
        _xmax = bounds.max.x - SCENE_X_OFFSET;
        _ymin = bounds.min.y + SCENE_Y_OFFSET;
        _ymax = bounds.max.y - SCENE_Y_OFFSET;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Debug.Log(playerPosition);

        clampedPosition = new Vector3(
            Utils.ClampFloat(playerPosition.x, _xmin, _xmax),
            Utils.ClampFloat(playerPosition.y, _ymin, _ymax),
            transform.position.z
        );
        Debug.Log(clampedPosition);

        // TODO: need to clamp to scene boundaries
        transform.position = Vector3.Lerp(transform.position, clampedPosition, Time.deltaTime * offsetSmoothing);
    }
}