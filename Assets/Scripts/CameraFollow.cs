using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(-0.41f, 0.03f, 0);
    public Transform target;

    Vector3 rightBoundary = new Vector3(0, 0, 4.05f);
    Vector3 leftBoundary = new Vector3(0, 0, 4.25f);

    void Update()
    {
        transform.position = target.position + offset;
    }
}
