using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform Playerobj;
    public Rigidbody rb;
    Vector3 offset;
    public GameObject target;
    public float angleX;
    public float angleY;
    public float radius = 10;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        offset = transform.position - target.transform.position;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            Playerobj.forward = Vector3.Slerp(Playerobj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);


    }

    private void LateUpdate()
    {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + (rotation * offset);
        transform.LookAt(target.transform);
    }
}
