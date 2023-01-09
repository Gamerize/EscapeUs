using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public BoostSystem Boosting;
    public float speed = 12f;

    public void Awake()
    {
        Boosting = GetComponent<BoostSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        if (Input.GetButton("Boost") && Boosting.fuel > 0)
        {
            controller.Move(movement * speed * 3 * Time.deltaTime);
        }
        else
        controller.Move(movement * speed * Time.deltaTime); //movement using controller

    }
}
