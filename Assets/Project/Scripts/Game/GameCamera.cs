using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0f, 2f, -7f);
    public float focusSpeed = 6f;

    public GameObject temporaryTarget;
    public float temporaryFocusTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        temporaryTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetFinal = offset;

        if (temporaryTarget != null) {
            // Debug.Log("GameCamera focusing on Temporary target - Treasure!");
            transform.position = Vector3.Lerp(transform.position, temporaryTarget.transform.position + offsetFinal, Time.deltaTime * focusSpeed);
        } else if (target != null) {
            if (target.GetComponent<Player>() != null)
            {
                Player player = target.GetComponent<Player>();
                offsetFinal.z += player.distanceFromCamera;
                // Debug.Log("GameCamera focusing on main target - Player!");
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offsetFinal, Time.deltaTime * focusSpeed);
                if (player.JustTeleported)
                {
                    transform.position = player.transform.position + offsetFinal;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, transform.position = target.transform.position + offsetFinal, Time.deltaTime * focusSpeed);
            }
        }
    }

    public void FocusOn(GameObject target) {
        temporaryTarget = target;
        StartCoroutine(FocusOnRoutine());
        Debug.Log("GameCamera::FocusOn() is complete!");
    }

    private IEnumerator FocusOnRoutine() {
        yield return new WaitForSeconds(temporaryFocusTime);
        Debug.Log("GameCamera::FocusOnRoutine() temporaryTarget = null");
        temporaryTarget = null;
    }
}
