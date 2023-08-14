using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;
    public Vector3 positionOffset;

    [Range(0, 1)]
    public float smoothTime;

    [Header ("Axis Limitation")]
    public Vector2 xlimit; //X Axis Limitation
    public Vector2 ylimit; //Y Axis Limitation

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xlimit.x, xlimit.y), Mathf.Clamp(targetPosition.y, ylimit.x, ylimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref velocity, smoothTime);
    }
}
