using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerrain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 0, 0);
    public float focusSpeed = 5f;
    public float positionSpeed = 20f;
    public int rotationSpeed = 260;
    public Transform cameraPivot;
    public Transform cameraObject;

    private float cameraPitch;
    private float cameraYaw;
    private Vector2 input;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPitch += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed * -1;
        cameraYaw += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        cameraPivot.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        cameraForward = cameraObject.forward;
        cameraRight = cameraObject.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        cameraPivot.position += (cameraForward * input.y + cameraRight * input.x) * Time.deltaTime * positionSpeed;

        if (Input.GetKey("space"))
        {
            cameraPivot.position += new Vector3(0, 1 * Time.deltaTime * positionSpeed * 0.5f, 0);
        }
        if (Input.GetKey("left shift")) {
            cameraPivot.position += new Vector3(0, -1 * Time.deltaTime * positionSpeed * 0.5f, 0);
        }
    }
}
