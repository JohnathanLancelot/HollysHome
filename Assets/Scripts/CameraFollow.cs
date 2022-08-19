using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(-0.35f, 0.16f, 0);
    public Transform target;

    void Update()
    {
        transform.position = target.position + offset;
    }
}
