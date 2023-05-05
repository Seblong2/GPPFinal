using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{

    [Header("Limit Rot")]
    public float maxXRot = 20f;
    public float minXRot = -20f;
    


    private float x;
    private float y;
    public float sens = -1f;
    private Vector3 rotate;
    private Transform localTrans;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        localTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x, y * sens, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
        LimitXRot();
    }

    private void LimitXRot()
    {
        Vector3 playerEulerAngles = localTrans.rotation.eulerAngles;

        playerEulerAngles.x = (playerEulerAngles.x > 180) ? playerEulerAngles.x - 360 : playerEulerAngles.x;
        playerEulerAngles.x = Mathf.Clamp(playerEulerAngles.x, minXRot, maxXRot);
       

        transform.localRotation = Quaternion.Euler(playerEulerAngles);

        
        
    }
}
