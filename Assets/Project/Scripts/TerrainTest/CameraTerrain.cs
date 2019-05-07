using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerrain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 0, 0);
    public float focusSpeed = 5f;
    public float rotateSpeed = 2f;

    private float X;
    private float Y;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position = target.transform.position + offset, Time.deltaTime * focusSpeed);
        }

        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -rotateSpeed, Input.GetAxis("Mouse X") * rotateSpeed, 0));
        X = transform.rotation.eulerAngles.x;
        Y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(X, Y, 0);
    }
}
