using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraOffset = 9.0f;
    public float cameraSpeed = 5.0f;
    public float cameraYOffset = 0.5f;

    private Vector3 playerPosition;
    private float direction = 1.0f;

    private void Start()
    {
        playerPosition = new Vector3(player.position.x - cameraOffset * direction, player.position.y + cameraYOffset, transform.position.z);
    }

    private void Update()
    {
        if (player.localScale.x > 0)
        {
            direction = -1.0f;
        }

        else if (player.localScale.x < 0)
        {
            direction = 1.0f;
        }

        playerPosition = new Vector3(player.position.x - cameraOffset * direction, player.position.y + cameraYOffset, transform.position.z);

        
        transform.position = Vector3.Lerp(transform.position, playerPosition, cameraSpeed * Time.deltaTime);
    }
}

