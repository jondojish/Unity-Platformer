using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed; // [SerializeField] allows variable to be changed within unity
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;


    // awake() is called when the script is initalised
    private void Awake()
    {
        // grab references for rigidboy and animatior from object
        body = GetComponent<Rigidbody2D>(); //GetComponent will check the object for component of type Rigidbody2D and will be stored in body variable
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //input.GetAxis("Horizontal") returns -1 if A/<- is held and 1 if D/-> is held else 0

        // flip player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);



        //set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // wall jump logic
        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space))
                Jump();

        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {

            if (horizontalInput == 0) // if not holding left or right then we jump horizontaly away from wall
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); //Mathf.Sign(x) returns 1 if  x > 0 and -1 if x < 0
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            { // else we jump away and up
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); //Mathf.Sign(x) returns 1 if  x > 0 and -1 if x < 0
            }
            wallJumpCooldown = 0;
        }


    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); // uses a box cast to see if an object with box collider in ground layer is within 0.1 below player 
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // uses a box cast to see if an object with box collider in ground layer is within 0.1 below player 
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }



}


