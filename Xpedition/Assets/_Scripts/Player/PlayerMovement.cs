using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Xpedition
{
    public class PlayerMovement : MonoBehaviour
    {  
      [Header("Movement")]
      [SerializeField] private float runSpeed = 20.0f;
      private float horizontal, vertical;
      private Vector3 playerInput;

      [Header("Animation")]
      [SerializeField] private Animator anim;

      [Header("Particles")]
      [SerializeField] private ParticleSystem walkParticles;
      private Vector3 lastPos;

      [Header("References")]
      [SerializeField] private Rigidbody2D rb;


      private void Start()
      {
        StartCoroutine(HandleParticles());
      }

      private void FixedUpdate ()
      {
        if (GameManager.paused) return;

        GetInputs();

        // Calls the MovePlayer function
        MovePlayer();

        // Invert the player on the x axis when moving left
        HandleFlipPlayer();
      }

      private void GetInputs()
      {
        // Get the player's horizontal and vertical inputs for this frame
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 

        //Store user input as a movement vector
        playerInput = new Vector3(horizontal, vertical, 0);
      }

      private void MovePlayer()
      {
        // Adds the horizontal and vertical input to the current player position
        //transform.position += new Vector3(horizontal, vertical, 0) * runSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + playerInput * Time.deltaTime * runSpeed);

        // Handle player walk animation
        if (horizontal != 0 || vertical != 0)
        {
          anim.SetBool("IsWalking", true);
        }
        else
        {
          anim.SetBool("IsWalking", false);
        }
      }

      private void HandleFlipPlayer()
      {
        if (horizontal < 0)
        {
          transform.localScale = new Vector3(-1, 1, 1);
          walkParticles.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
          transform.localScale = new Vector3(1, 1, 1);
          walkParticles.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
      }

      private IEnumerator HandleParticles()
      {
        Renderer particlesLayer = walkParticles.GetComponent<Renderer>();

        while (true)
        {
          // Set the last position the player was in
          lastPos = transform.position;

          yield return new WaitForSeconds(0.05f);
          
          // Handle player walking particles
          if (transform.position != lastPos)
          {
            walkParticles.Play();
          } else if (transform.position == lastPos)
          {
            walkParticles.Stop();
          }

          // Move particles behind player when moving straight down
          if (vertical < 0)
          {
            particlesLayer.sortingOrder = 4;
          }
        }
      }
    }
}
