using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Takes input and turns into player movement
public class player_movement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;
    
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    
    SpriteRenderer spriteRenderer; 

    Rigidbody2D rb;
    
    Animator animator;

    List <RaycastHit2D> castCollisions = new List<RaycastHit2D>();



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update called x times per second (great for physics related models)
    private void FixedUpdate(){
        // If movement input is not 0, try to move
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput); 
            if(!success){
                    success = TryMove(new Vector2(0, movementInput.y));
                    if(!success){success = TryMove(new Vector2(movementInput.x, 0));}
             }
        } 
        if(movementInput.x != 0){
            animator.SetBool("isMovingSide", true);}  
        else {
            animator.SetBool("isMovingSide", false);
        }
        if(movementInput.x < 0){
        spriteRenderer.flipX = true; }
        if(movementInput.x > 0){
        spriteRenderer.flipX = false;}
    }

    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(movementInput, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if(count == 0 ){
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {return false;}
         }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
}
