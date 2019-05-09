using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Player player;
    public Vector3 offset = new Vector3(0, 5, -15);
    public float focusSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            transform.position = Vector3.Lerp(transform.position, transform.position = player.transform.position + offset, Time.deltaTime * focusSpeed);

            if (player.JustTeleported) {
                transform.position = player.transform.position + offset;
            }
        }
    }
}
