using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    private Transform Player;

    public float BoundX = 0.3f;

    public float BoundY = 0.15f;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;

    }

    private void LateUpdate()
    {
        Vector3 camera = Vector3.zero;

        float cameraX = Player.position.x - transform.position.x;
        if (cameraX > BoundX || cameraX  < -BoundX)
        {
            if (transform.position.x < Player.position.x)
            {
                camera.x = cameraX - BoundX;
            }
            else
            {
                camera.x = cameraX + BoundX;
            }
        }

        float cameraY = Player.position.y - transform.position.y;
        if (cameraY > BoundY || cameraY < -BoundY)
        {
            if (transform.position.y < Player.position.y)
            {
                camera.y = cameraY - BoundY;
            }
            else
            {
                camera.y = cameraY + BoundY;
            }
        }

        transform.position += new Vector3(camera.x, camera.y , 0);
    }
}