using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform cameraTarget;

    //Gather input data and sensitivity
    public float sensitivity = 10;
    public bool invertY = false;
    public bool invertZoomControl = false;

    //Set variables for camera zoom
    public float distFromTarget = 5;
    public float zoomIncrement = 0.5f;
    public Vector2 zoomLimit = new Vector2(5, 30);

    //Set variables for clamping pitch (x-axis rotation)
    public Vector2 pitchLimit = new Vector2(-45, 60);
    float pitch;
    float yaw;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    //Adjust camera position after other objects have moved in the Update method
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            RotateCamera();
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            int zoomAdjust = (int)(-1 * Input.mouseScrollDelta.y / Mathf.Abs(Input.mouseScrollDelta.y));

            if (invertZoomControl)
            {
                zoomAdjust *= -1;
            }

            distFromTarget = Mathf.Clamp(distFromTarget + (zoomAdjust * zoomIncrement), zoomLimit.x, zoomLimit.y);
            transform.position = cameraTarget.position - (cameraTarget.forward * distFromTarget);
        }
    }

    void RotateCamera()
    {
        //Gather mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;

        float pitchAdjust = Input.GetAxis("Mouse Y") * sensitivity;
        if (invertY)
        {
            pitchAdjust *= -1;
        }
        pitch -= pitchAdjust;
        pitch = Mathf.Clamp(pitch, pitchLimit.x, pitchLimit.y);

        //Adjust camera pivot's position, then camera itself
        //Smoother than adjusting both position and rotation only on camera
        cameraTarget.eulerAngles = new Vector3(pitch, yaw);
        transform.position = cameraTarget.position - (cameraTarget.forward * distFromTarget);
        transform.eulerAngles = cameraTarget.eulerAngles;
    }
}
