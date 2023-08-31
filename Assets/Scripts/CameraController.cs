using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float lerpAmount = 0.25f;

    private Vector2 targetPos, currentPos;

    void FixedUpdate()
    {
        targetPos = target.transform.position;
        currentPos = transform.position;

        Vector2 destination = Vector2.Lerp(targetPos, currentPos, lerpAmount);
        transform.position = new Vector3(
            destination.x,
            destination.y,
            transform.position.z);
    }
}
