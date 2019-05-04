using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500;
    public float lifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Arrow->Start()!");
        // gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            Destroy(gameObject);
        }
    }
}
