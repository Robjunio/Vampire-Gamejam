using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float smoothSpeed = 3.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3 (target.position.x, target.position.y, transform.position.z),
            smoothSpeed * Time.deltaTime);
    }
}
