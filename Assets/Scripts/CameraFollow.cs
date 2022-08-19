﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(-2, 0.65f, 0);
    public Transform target;
    //public float SmoothSpeed = 1.0f;

    void Update()
    {
        //Vector3 DesiredPosition = target.position + offset;

        //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);
        //transform.position = SmoothedPosition;

        //transform.LookAt(target);

        transform.position = target.position + offset;
    }
}
