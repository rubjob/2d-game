using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseUtil : MonoBehaviour {
    public new Camera camera;
    public float GetMouseAngle() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.nearClipPlane;

        Vector3 worldPosition = camera.ScreenToWorldPoint(mousePos);
        Vector3 lookDirection = worldPosition - transform.position;

        return Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }
}
