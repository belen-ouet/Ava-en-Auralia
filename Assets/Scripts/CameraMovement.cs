using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
        offset.y = 0f; // Mantener la misma altura relativa
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = transform.position.y; // Mantener la altura actual de la cámara
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}