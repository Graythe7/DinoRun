using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController player;
    private Vector3 direction;

    public float gravity = 9.8f; // you could have this value by using the rigid body yet that would mean complex overall physic
    public float jumpForce = 8f;

    private void Awake()
    {
        player = GetComponent<CharacterController>(); // it's going to search for this component in the object that has been assigned to
    }

    private void OnEnable()
    {
        direction = Vector3.zero; // the direction reset first the code is enables (like after game over)
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime; // can't use vector3.right as dino is not moving, only the BG moves 

        if (player.isGrounded)
        {
            direction += Vector3.down;
            if (Input.GetButtonDown("Jump")) //using unity built in input 
            {
                direction = Vector3.up * jumpForce;
            }
        }
        player.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
