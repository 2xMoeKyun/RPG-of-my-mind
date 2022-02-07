using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public float maxSpeed = 3f;
    public float Jforce = 6f;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MoveX();
        Jump();
    }
    void Jump()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Jforce);
        }
    }
    void MoveX()
    {
        float Xmove = Input.GetAxis("Horizontal");
        if (Xmove == 0)
        {
            anim.Play("Idle");
        }
        if (Xmove > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.Play("Move_right");
        }
        if (Xmove < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.Play("Move_right");
        }
        transform.Translate(Vector2.right * Xmove * maxSpeed * Time.deltaTime);
    }
    public Transform GrCheck;
    public LayerMask Ground;
    bool isGrounded;
    float CheckRad = 0.5f;
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GrCheck.position, CheckRad, Ground);
    }

}
