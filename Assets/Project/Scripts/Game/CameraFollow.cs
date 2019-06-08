using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0f, 2f, -6f);
    public float speed = 5;

    void Start()
    {
    }

    void LateUpdate()
    {
        // Look
        var newRotation = Quaternion.LookRotation(target.transform.position - transform.position + Vector3.up * 2);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);

        // Move
        Vector3 newPosition = target.transform.position + target.transform.forward * offset.z + target.transform.up * offset.y;
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
    }
}
