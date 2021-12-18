using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float CheckRadius;
    public int maxJumpCount;
    public static Rigidbody2D rb;

    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    private float tempHealth = 100;
   
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();

    }

    void Start(){
        jumpCount = maxJumpCount;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    // Update is called once per frame
    void Update(){
        /*if (DialogueManager.GetInstance().dialogueIsPlaying){
            return;
        }*/
        ProcessInput();
        AnimateFlip();

        if (HitPlayer.playerInAttack == true && HitPlayer.hittable == true){
            TakeDamage(10);
            HitPlayer.hittable = false;
        }

        if (tempHealth > currentHealth){
            tempHealth -= 0.1f;
            healthBar.SetHealth(tempHealth);
        } else if (tempHealth < currentHealth){
            tempHealth += 0.1f;
            healthBar.SetHealth(tempHealth);
        }
        
    }

    private void FixedUpdate(){
        /*if (DialogueManager.GetInstance().dialogueIsPlaying){
            return;
        } */
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, groundObjects);
        if (isGrounded){
            jumpCount = maxJumpCount;
            //Debug.Log(jumpCount);
        }
        Move();
    }


    private void ProcessInput(){
        moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount > 0){
            isJumping = true;
            isGrounded = false;
        } 

        if (Input.GetButtonDown("Heal") && currentHealth < maxHealth){
            Healing(10);
        } else if (Input.GetButtonDown("Heal") && currentHealth == maxHealth){
            Debug.Log("Brother you are full health vat ze fuk");
        }
    }
    private void AnimateFlip(){
        if (moveDirection > 0 && !facingRight){
            FlipCharacter();
        } else if (moveDirection < 0 && facingRight){
            FlipCharacter();
        }
    }
    private void Move(){
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (isJumping){
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }

        isJumping = false;
    }
    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public static void Stop(){
        rb.velocity = new Vector3(0,0,0);
    }

    public void TakeDamage(float damage){
        
        if (currentHealth - damage >= 0){
            currentHealth -= damage;
        } else {
            currentHealth = 0;
        }
    }

    public void Healing(float heal){
        if (currentHealth + heal <= maxHealth){
            currentHealth += heal;
        } else {
            currentHealth = maxHealth;
        }
    }
}
