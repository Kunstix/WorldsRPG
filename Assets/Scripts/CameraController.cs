using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Boundaries")]
    public Tilemap map;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfWidth;
    private float halfHeight;

    private void Start()
    {
        player = PlayerController.player.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = Camera.main.aspect * halfHeight;

        bottomLeftLimit = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0F);
        topRightLimit = map.localBounds.max  + new Vector3(-halfWidth, -halfHeight, 0F);

        // Help player staying inside map
        PlayerController.player.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    void LateUpdate()
    {
        // Camera follows player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Camera stays within boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), 
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), 
                                         transform.position.z);
    }
}
