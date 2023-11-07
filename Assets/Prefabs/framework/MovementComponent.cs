using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    float turnSpeed;
    public float RotateTowards(Vector3 lookDir)
    {
        float goalAnimTurnSpeed = 0f;

        if(lookDir.magnitude != 0)
        {

        }

        Quaternion prevRot = transform.rotation; // before rotate

        Quaternion nextRot = Quaternion.LookRotation(lookDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, turnSpeed * Time.deltaTime);
        Quaternion newRot = transform.rotation; // after rotate

        float rotationDelta = Quaternion.Angle(prevRot, newRot); // how much we have rotated in this frame.

        float rotateDir = Vector3.Dot(lookDir, transform.right) > 0 ? 1 : -1;

        goalAnimTurnSpeed = rotationDelta / Time.deltaTime * rotateDir;

        return goalAnimTurnSpeed;
    }
}
