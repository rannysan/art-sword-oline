using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float vel = 5.0f;
    public float velrotation = 50.0f;
    public float gravity = 30.0f;
    public Vector3 moveDir = Vector3.zero;
    public CharacterController control;
    public Animator anim;
    //Animator anim = new Animator();

    void Start()
    {

    }

    void Update()
    {
        if (control.isGrounded)
        {
            moveDir = new Vector3(0, 0, Input.GetAxis("Vertical3D"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= vel;
        }

        moveDir.y -= gravity * Time.deltaTime;
        control.Move(moveDir * Time.deltaTime);

        float rotation = Input.GetAxis("Horizontal3D") * velrotation;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (control.isGrounded)
        {
            if(moveDir.y != 0)
            {
                anim.SetBool("Stop", false);
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Stop", true);
                anim.SetBool("Run", false);
            }
        }
    }
}
