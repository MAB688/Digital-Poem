using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MoveSmoothTime;
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpStrength;
    public float GravityStrength;

    private CharacterController Controller;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;

    private Vector3 CurrentForceVelocity;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {   
        Vector3 PlayerIput = new Vector3 {
            x = Input.GetAxis("Horizontal"),
            y = 0f,
            z = Input.GetAxis("Vertical")
        };

        if(PlayerIput.magnitude > 1f) {
            PlayerIput.Normalize();
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerIput);
        float CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

        CurrentMoveVelocity = Vector3.SmoothDamp (
            CurrentMoveVelocity,
            MoveVector * CurrentSpeed,
            ref MoveDampVelocity,
            MoveSmoothTime
            );

        Controller.Move(CurrentMoveVelocity * Time.deltaTime);

        Ray groundCheck = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(groundCheck, 1.1f)) {
            CurrentForceVelocity.y = -2f;
            if (Input.GetKeyDown(KeyCode.Space)) {
                CurrentForceVelocity.y = JumpStrength;
            }
        } else {
            CurrentForceVelocity.y -= GravityStrength * Time.deltaTime;
        }

        Controller.Move(CurrentForceVelocity * Time.deltaTime);
    }
}
