using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Animator animator;
    private SpriteRenderer sprite;
    private float xDirection = 0f;
    [SerializeField]private float speed = 4f;
    [SerializeField]private float jumpVel = 7f;
    [SerializeField] private LayerMask ground;
    
    private enum AnimationState { idle, running, jumping, death}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // From IDataPersistence Interface.
    public void LoadData(GameData data)
    {
        // Set player position to the player position of game data.
        this.transform.position = data.playerPos;
    }

    // From IDataPersistence Interface.
    public void SaveData(ref GameData data)
    {
        // set data player position to position of player game object.
        data.playerPos = this.transform.position;
    }

    private void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDirection * speed, rb.velocity.y); // left & right movement.

        if (Input.GetButtonDown("Jump") && GroundCheck())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVel);
        }

        ChangeAnimationState();
    }

    private void ChangeAnimationState()
    {
        AnimationState state;

        // change animation state.
        if (xDirection > 0f) // moving right.
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        else if (xDirection < 0f) // moving left.
        {
            state = AnimationState.running;
            // Make character face opposite direction.
            sprite.flipX = true;
        }
        else // idle
        {
            state = AnimationState.idle;
        }

        // jump animation set seperately so the character won't run or idle in mid-air.
        if(rb.velocity.y > 0.1f)
        {
            state = AnimationState.jumping;
        }

        // cast enum to int so it can be used by animator.
        animator.SetInteger("state",(int)state);
    }

    private bool GroundCheck()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
