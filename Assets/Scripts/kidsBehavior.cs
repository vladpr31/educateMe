using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidsBehavior : MonoBehaviour
{
    Rigidbody2D rb; //Rigidbody holder
    Animator anim; //animation of kid
    float dirX = 5f; //movement speed
    float moveSpeed = 5f; //movementspeed
    bool isDead; //dead player
    int kidsHealth = 1; //player's health not used for now if ever.
    bool facingRight = true; //facing direction. true is right flase is left.
    Vector3 localScale;

    void Start() //on start we initiate the variables.
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }
    void Update() //updates every frame.
    {
        if(Input.GetButtonDown("Jump") && !isDead && rb.velocity.y==0) //if player is not dead and velocity is 0 which means player on ground
                                                                       //then allow player to jump
        {
            rb.AddForce(Vector2.up * 400f); //400f is jump height.
        }
        setAnimationState(); //Animation state function which changes the state of player Jump state,Walk state,Dead state.
        if(!isDead)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); //if not dead allow walking
            transform.position += movement*Time.deltaTime*moveSpeed; //position of walking
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        }
    }
    void LateUpdate()
    {
        checkFacing();
    }
    void setAnimationState() //for the animator to change states of player.
    {
        if(dirX==0) //if dirx is 0 meaning the kids is mid air and canot walk mid air.
        {
            anim.SetBool("isWalking", false);
        }
        if(rb.velocity.y==0) //on ground ==> not in jump state.
        {
            anim.SetBool("isJumping", false);
        }
        if(Mathf.Abs(dirX)==5 && rb.velocity.y==0) //if not jumping and movment speed is 5 then walking allowed.
        {
            anim.SetBool("isWalking", true);
        }
        if(rb.velocity.y>0) //if velocity of y is greater than 0 means we are in "air" ==> jump state.
        {
            anim.SetBool("isJumping", true);
        }

    }
    void checkFacing()
    {
        if(dirX>0) //if dirX is bigger than 0 on x axis it means we are facing right
        {
            facingRight = true;
        }
        if(dirX<0) //if dirX is samller than 0 on x axis means we are facing left.
        {
            facingRight = false;
        }
        if(((facingRight) && (localScale.x<0)) || ((!facingRight) && (localScale.x>0))) //flipping Kid by multiplying the x value by negative.
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

}
