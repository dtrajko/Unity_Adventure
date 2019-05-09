using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 5, -15);
    public float focusSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            transform.position = Vector3.Lerp(transform.position, transform.position = target.transform.position + offset, Time.deltaTime * focusSpeed);
        }
    }
}
