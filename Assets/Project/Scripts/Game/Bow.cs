using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack() {
        GameObject arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.position = playerModel.transform.position + playerModel.transform.forward + Vector3.up * 1.2f;
        arrowObject.transform.forward = playerModel.transform.forward;
    }
}
