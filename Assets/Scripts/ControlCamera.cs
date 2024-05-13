using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlCamera : MonoBehaviour
{
    public float rotation_speed = 10f;
    public Transform target;
    private Vector3 lastmousebuttonposition;
    void Update()
    {
        // Check if right mouse button is held down
        if (Input.GetMouseButtonDown(1))
        {
            lastmousebuttonposition = Input.mousePosition;
        }

        // Check if right mouse button is being held down
        if (Input.GetMouseButton(1))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - lastmousebuttonposition;

            // Rotate the camera around the target based on mouse movement
            if (target != null)
            {
                transform.RotateAround(target.position, Vector3.up, delta.x * rotation_speed * Time.deltaTime);

                transform.RotateAround(target.position, Vector3.right, delta.y * rotation_speed * Time.deltaTime);

            }
            else
            {
                transform.Rotate(Vector3.up, delta.x * rotation_speed * Time.deltaTime);

                transform.Rotate(Vector3.right, delta.y * rotation_speed * Time.deltaTime);
            }

            lastmousebuttonposition = currentMousePosition;
        }
    }
}

