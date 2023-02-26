using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Takes input and turns into player movement
public class player_movement : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    
    public float collisionOffset = 0.05f;

    Rigidbody2D rb;
    
    List <RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update called x times per second (great for physics related models)
    private void FixedUpdate(){
        // If movement input is not 0, try to move
        if(movementInput != Vector2.zero){
            int count = rb.Cast(movementInput, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if(count==0){
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
}
