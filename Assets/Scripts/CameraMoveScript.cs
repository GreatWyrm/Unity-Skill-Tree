using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float friction;
    private Vector3 speed;
    private void FixedUpdate()
    {
        bool isMoving = false;
        if (Input.GetKey("w"))
        {
            speed.y += acceleration;
            isMoving = true;
        }
        if (Input.GetKey("a"))
        {
            speed.x -= acceleration;
            isMoving = true;
        }
        if (Input.GetKey("s"))
        {
            speed.y -= acceleration;
            isMoving = true;
        }
        if (Input.GetKey("d"))
        {
            speed.x += acceleration;
            isMoving = true;
        }
        if(isMoving)
        {
            if(speed.x > maxSpeed)
            {
                speed.x = maxSpeed;
            } else if(speed.x < -1 * maxSpeed) {
                speed.x = -1 * maxSpeed;
            }
            if (speed.y > maxSpeed)
            {
                speed.y = maxSpeed;
            }
            else if (speed.y < -1 * maxSpeed)
            {
                speed.y = -1 * maxSpeed;
            }
        } else
        {
            speed.x = applyFriction(speed.x);
            speed.y = applyFriction(speed.y);
        }
        transform.position += speed;
    }
    private float applyFriction(float speed)
    {
        if(speed < 0)
        {
            if (speed + friction > 0)
            {
                speed = 0;
            }
            else
            {
                speed += friction;
            }
        } else
        {
            if(speed - friction < 0)
            {
                speed = 0;
            } else
            {
                speed -= friction;
            }
        }
        return speed;
    }
}
