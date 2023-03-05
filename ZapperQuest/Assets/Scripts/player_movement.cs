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

    private void FixedUpdate(){
        bool success_x = TryMove(new Vector2(movementInput.x, 0));
        bool success_y = TryMove(new Vector2(0, movementInput.y));

        if(success_x){
            if(movementInput.x < 0){spriteRenderer.flipX = true;}
            else if(movementInput.x > 0){spriteRenderer.flipX = false;}
            animator.SetBool("isMovingSide", success_x);

        }
        else{
            animator.SetBool("isMovingSide" , false);
        }
        if(success_y){
            if(!success_x){
                if(movementInput.y < 0){animator.SetBool("isMovingDown", success_y);}
                if(movementInput.y > 0) {animator.SetBool("isMovingUp", success_y);}
            }
        }
        else{
            animator.SetBool("isMovingDown", false);
            animator.SetBool("isMovingUp", false);
        }
        TryMove(movementInput);
    }
    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if(count == 0 ){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {return false;}
         } else {
            return false;
         }
    }
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
    
}
