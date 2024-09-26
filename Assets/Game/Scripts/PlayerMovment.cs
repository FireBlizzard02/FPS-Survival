using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 move_direction;
    private float vertical_Velocity;

    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {

        MoveThePlayer();

    }


    private void MoveThePlayer()
    {
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed * Time.deltaTime;
        ApplyGravity();
        characterController.Move(move_direction);
    }

    private void ApplyGravity()
    {
        vertical_Velocity -= gravity * Time.deltaTime;

        //jump
        PlayerJump();

        move_direction.y = vertical_Velocity * Time.deltaTime;
    }
    void PlayerJump()
    {

        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }

    }
}
