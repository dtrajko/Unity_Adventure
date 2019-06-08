using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;
    public Vector3 offset = new Vector3(0f, 2f, -7f);
    public float focusSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate() {
        float cameraDistance = offset.z;
        float cameraHeight = offset.y;
        transform.position = player.transform.position + player.transform.forward * cameraDistance;
        transform.rotation = player.TargetModelRotation; // Quaternion.Lerp(transform.rotation, player.TargetModelRotation, Time.deltaTime * player.rotatingSpeed);
        transform.LookAt(player.transform.position);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z);
        transform.position = newPosition;
        // transform.position = Vector3.Lerp(newPosition, newPosition, Time.deltaTime * focusSpeed);
    }
}
