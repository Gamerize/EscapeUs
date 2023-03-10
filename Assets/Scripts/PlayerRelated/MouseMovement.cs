using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//with reference to brackeys

public class MouseMovement : MonoBehaviour
{
    public float Sensitivity = 200.0f;
    public Transform player;

    float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f); //preventing the X-axis from going over -90 to 90

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //y-axis rotation
        player.Rotate(Vector3.up * mouseX); //X-axis rotation
    }
}
