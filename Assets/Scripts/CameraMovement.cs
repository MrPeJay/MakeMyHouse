using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float dragSpeed = 6f;
    public float fastDragSpeed = 12f;

    private PauseMenu PauseMenuScript;
    private ObjectSelect ObjectSelectScript;

    private void Start()
    {
        PauseMenuScript = gameObject.GetComponent<PauseMenu>();
        ObjectSelectScript = gameObject.GetComponent<ObjectSelect>();
    }


    void Update()
    {
        if (!PauseMenuScript.PauseMenuEnabled)
        {
            if (Input.GetButton("Mouse Xd"))
            {
                transform.position += transform.right * Time.deltaTime * dragSpeed;
            }

            else if (Input.GetButton("Mouse Xa"))
            {
                transform.position -= transform.right * Time.deltaTime * dragSpeed;
            }

            if (Input.GetButton("Mouse Yw"))
            {
                transform.position += transform.forward * Time.deltaTime * dragSpeed;
            }

            else if (Input.GetButton("Mouse Ys"))
            {
                transform.position -= transform.forward * Time.deltaTime * dragSpeed;
            }

            if (Input.GetButton("Faster"))
            {
                dragSpeed = fastDragSpeed;
            }
            else
            {
                dragSpeed = 6;
            }

            if (Input.GetMouseButton(2))
            {
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");

                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
        }
    }
}