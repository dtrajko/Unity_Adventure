using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject target;
    public float playerFocusHeight = 3f;
    public float playerFocusDepth = -7f;
    public float cameraFocusDistanceForward = 30f;
    public float focusSpeed = 20f;

    public GameObject temporaryTarget;
    public float temporaryFocusTime = 10f;

    private Vector3 targetOffset = new Vector3(0, 1.5f, -7f);

    // Start is called before the first frame update
    void Start()
    {
        temporaryTarget = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (temporaryTarget != null) {
            transform.position = Vector3.Lerp(transform.position, temporaryTarget.transform.position + targetOffset, Time.deltaTime * focusSpeed);

            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                0,
                transform.localEulerAngles.z
            );
            // transform.LookAt(temporaryTarget.transform.position);
        }
        else if (target != null) {
            if (target.GetComponent<Player>() != null)
            {
                Player player = target.GetComponent<Player>();

                Vector3 playerTargetPosition = player.transform.position + Vector3.up * playerFocusHeight + player.model.transform.forward * playerFocusDepth;
                transform.position = Vector3.Lerp(transform.position, playerTargetPosition, Time.deltaTime * focusSpeed);
                if (player.JustTeleported)
                {
                    transform.position = playerTargetPosition;
                }

                transform.localEulerAngles = new Vector3(
                    transform.localEulerAngles.x,
                    player.model.transform.localEulerAngles.y,
                    transform.localEulerAngles.z
                    );

                transform.LookAt(player.transform.position + player.model.transform.forward * cameraFocusDistanceForward);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, transform.position = target.transform.position + targetOffset, Time.deltaTime * focusSpeed);
            }
        }
    }

    public void FocusOn(GameObject target) {
        temporaryTarget = target;
        StartCoroutine(FocusOnRoutine());
    }

    private IEnumerator FocusOnRoutine() {
        yield return new WaitForSeconds(temporaryFocusTime);
        temporaryTarget = null;
    }
}
