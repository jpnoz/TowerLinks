using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLook : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public Transform lookTarget;

    [SerializeField] Transform rotationBase;
    [SerializeField] Transform rotationHead;

    // Delete later
    [SerializeField] Transform laserOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookTarget)
        {
            LookAtTarget();
        }

        Debug.DrawLine(laserOrigin.position, laserOrigin.position + laserOrigin.forward * 1000, Color.red);
    }

    void LookAtTarget()
    {
        // Rotate Base first
        Vector3 baseLookDirection = lookTarget.position - rotationBase.position;

        // Base only rotates about y-axis
        float baseLookRotation = Quaternion.LookRotation(baseLookDirection, rotationBase.up).eulerAngles.y;
        Vector3 baseTargetRotation = new Vector3(0, baseLookRotation, 0);

        rotationBase.rotation = Quaternion.Lerp(rotationBase.rotation, Quaternion.Euler(baseTargetRotation), rotationSpeed * Time.deltaTime);

        // Rotate Head after
        // To make laser look at desired point, target point must be adjusted downward
        Vector3 headLookDirection = lookTarget.position - rotationHead.position;
        headLookDirection += rotationHead.position - laserOrigin.position;

        // Head only rotates about x-axis
        float headLookRotation = Quaternion.LookRotation(headLookDirection, rotationHead.up).eulerAngles.x;

        rotationHead.localRotation = Quaternion.Lerp(rotationHead.localRotation, Quaternion.Euler(headLookRotation, 0, 0), rotationSpeed * Time.deltaTime);
    }
}
