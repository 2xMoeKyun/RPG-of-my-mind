using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Animator playerAnimator;
    public static Rigidbody2D playerRb;
    public float maxSpeed = 3f;
    public float Jforce = 6f;
    float Xmove;
    public static int HitForce;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
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
            playerRb.velocity = new Vector2(playerRb.velocity.x, Jforce);
        }
    }
    void MoveX()
    {
        Xmove = Input.GetAxis("Horizontal");
        if (Xmove == 0)
        {
            playerAnimator.SetBool("MoveRight", false);
        }
        if (Xmove > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("MoveRight", true);
        }
        if (Xmove < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            playerAnimator.SetBool("MoveRight", true);
        }
        transform.Translate(Vector2.right * Xmove * maxSpeed * Time.deltaTime);
    }

    // импульс после атаки (для скрипта Damage)
    public void AfterHit(int HitForce)
    {
        playerRb.velocity = Vector2.zero;

        playerRb.AddForce(new Vector2(Enemy.direction * HitForce, 0));
    }

    // Ground check
    public Transform GrCheck;
    public LayerMask Ground;
    bool isGrounded;
    float CheckRad = 0.5f;
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GrCheck.position, CheckRad, Ground);
    }
    //
}
