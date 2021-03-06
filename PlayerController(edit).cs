﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    float velX;
    float velY;
    bool facingRight = true;
    Rigidbody2D rigBody;
    Animator anim;
    bool isRunning = false;
    public float jumpForce = 280f;
    public LayerMask theGround;
    public Transform groundCheck;
    bool onTheGround = false;
    public static int whichWeapon = 1;
    public int lives = 3;



    public GameObject bulletToRight, bulletToLeft, gameOverText, restartButton, blood, blastToRight,
                      blastToRight1, blastToRight2, blastToLeft, blastToLeft1, blastToLeft2;    

    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;

   

    // Use this for initialization
    void Start()
    {        
        gameOverText.SetActive(false);
        restartButton.SetActive(false);

        rigBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
        velX = Input.GetAxisRaw("Horizontal");
        velY = rigBody.velocity.y;
        rigBody.velocity = new Vector2(velX * moveSpeed, velY);

        if (velX != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        anim.SetBool("isRunning", isRunning);
        onTheGround = Physics2D.Linecast(transform.position, groundCheck.position, theGround);
        anim.SetBool("onTheGround", onTheGround);
        if (onTheGround && Input.GetButtonDown("Jump"))
        {
            SoundManagerScript.PlaySound("jump");
            velY = 0f;
            rigBody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            SoundManagerScript.PlaySound("fire");
            nextFire = Time.time + fireRate;
            fire();
        }

        
    }

    void LateUpdate()
    {
        Vector3 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;
        }
        else if (velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    

    void fire()
    {
        bulletPos = transform.position;
        if (facingRight)
        {
            bulletPos += new Vector2(+1f, 0.7f);
            if (whichWeapon == 1)
            {
                Instantiate(bulletToRight, bulletPos, Quaternion.identity);
            }
            else
            {                
                Instantiate(blastToRight, bulletPos, Quaternion.identity);
                Instantiate(blastToRight1, bulletPos, Quaternion.identity);
                Instantiate(blastToRight2, bulletPos, Quaternion.identity);
            }
        }
        else
        {
            bulletPos += new Vector2(-1f, 0.7f);
            if (whichWeapon == 1)
            {
                Instantiate(bulletToLeft, bulletPos, Quaternion.identity);
            }
            else
            {
                Instantiate(blastToLeft, bulletPos, Quaternion.identity);
                Instantiate(blastToLeft1, bulletPos, Quaternion.identity);
                Instantiate(blastToLeft2, bulletPos, Quaternion.identity);
            }
        }
    }

    

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (other.gameObject.name)
        {
            case "enemy":
            case "killzone":
                LoseLife();
                break;
        }

    }


    public void changeWeapon()
    {
        if (whichWeapon == 1)
        {
            whichWeapon = 2;
        }
        else
        {
            whichWeapon = 1; 
        }

            
    }

    void LoseLife()
    {

        lives--;
        LivesText.text = ("Lives :" + lives);
        if (lives == 0)
        {
            
            SoundManagerScript.PlaySound("playerDeath");
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            Instantiate(blood, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

        }
    }
}
    

