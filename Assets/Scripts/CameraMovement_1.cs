using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_1 : MonoBehaviour
{

    public Transform target;
    Vector3 offset;
    public float smoothing = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);

    }
}
