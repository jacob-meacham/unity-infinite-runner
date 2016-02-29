using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_InitalJumpForce = 800f;                  // Amount of force added when the player jumps.
		[SerializeField] private float m_JumpImpulse = 10f;                  // Amount of force added when the player jumps.
		[SerializeField] private float m_MaxJumpHoldTime = 3f;                  // Amount of force added when the player jumps.
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
		private float m_JumpHysteresis = 0f;
		private bool m_Jumped = false;
		private bool m_hasDoubleJumped = false;
		private bool m_InAirReleased = false;
		private float m_curJumpHeldTime = 0f;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

			// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
			// This can be done using layers instead but Sample Assets will not overwrite your project settings.
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}
			m_Anim.SetBool("Ground", m_Grounded);

			if (m_Jumped) {
				m_JumpHysteresis += Time.fixedDeltaTime;
				if (m_JumpHysteresis > 0.1f) {
					// Don't check grounded for a bit after our jump, to clear the ground.		
					if (m_Grounded) {
						m_JumpHysteresis = 0f;
						m_hasDoubleJumped = false;
						m_Jumped = false;
						m_InAirReleased = false;
						m_curJumpHeldTime = 0f;
					}
				}
			}

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(bool jump)
		{
			float moveSpeed = Mathf.Lerp(1f, 5f, transform.position.x / 1000f);
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim.SetFloat("Speed", Mathf.Abs(moveSpeed));

			// Move the character
			m_Rigidbody2D.velocity = new Vector2(moveSpeed*m_MaxSpeed, m_Rigidbody2D.velocity.y);

			// If the input is moving the player right and the player is facing left...
			if (!m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
            
            // If the player should jump...
			if (!m_Jumped && jump && m_Anim.GetBool ("Ground")) {
				m_Jumped = true;

				m_Anim.SetBool ("Ground", false);

				// Add a vertical force to the player.
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_InitalJumpForce));
			} else if (jump && m_InAirReleased && !m_hasDoubleJumped) {
				m_hasDoubleJumped = true;

				m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, 0f);
				// Add another force
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_InitalJumpForce));
			} else if (jump) {
				m_curJumpHeldTime += Time.deltaTime;
				if (m_curJumpHeldTime < m_MaxJumpHoldTime) {
					m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpImpulse));
				}
			}
				
			if (m_Jumped && !jump) {
				// Jump released in mid-air
				m_InAirReleased = true;
			}
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
