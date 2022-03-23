using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public static Animator playerAnimator;
    public static Rigidbody2D playerRb;
    public float maxSpeed = 3f;
    public float Jforce = 6f;
    float Xmove;
    public static int HitForce;
    public int dashForce = 500;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        WallCheck();
        MoveX();
        if (CanJump)
        {
            Jump();
        }
        
        if (CanUse)
        {
            UseThing();
        }
        Dash();
    }

    #region for moveing Platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
    #endregion
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerRb.velocity = Vector2.zero;

            playerRb.AddForce(new Vector2(Xmove * dashForce, 0));
        }
    }

    public static bool CanJump = true;
    public static bool IsJump = false;
    #region Jump
    bool onAirVal = false;// для определения, что перс был ввоздухе
    bool startJump = false;// для определения окончания стадии подготовки пржыка
    public void Jumping()//Вызывается в анимации подготовки прыжка
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, Jforce);
    }
    public void StartingJumpAnimEnd()// вызывается в конце анимации подготовки прыжка
    {
        startJump = true;
        IsJump = false;
    }
    
    void Jump()// вызывается в апдейте
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isGrounded && !startJump)
        {
            playerAnimator.Play("StartingJump");// Подготовка к прыжку
            IsJump = true;
        }
        else if (!isGrounded && startJump) //В воздухе
        {
            playerAnimator.SetBool("OnAir", true);
            onAirVal = true;
        }
        else if (isGrounded && onAirVal && startJump)// на земле
        {
            playerAnimator.SetBool("OnGround", true);
            onAirVal = false;
        }
    }
    public void JumpEnd()// сброс к анимации покоя
    {
        playerAnimator.SetBool("OnAir", false);
        playerAnimator.SetBool("OnGround", false);
        startJump = false;
    }
    #endregion 




    void MoveX()
    {
        Xmove = Input.GetAxis("Horizontal");
        if (rightWall)
        {
            if(Xmove > 0)
            {
                Xmove = 0;
            }
        }
        if (LeftWall)
        {
            if (Xmove < 0)
            {
                Xmove = 0;
            }
        }
        if (Xmove == 0)
        {
            playerAnimator.SetBool("MovingRight", false);
        }
        if (Xmove > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("MovingRight", true);
        }
        if (Xmove < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            playerAnimator.SetBool("MovingRight", true);

        }

        transform.Translate(Vector2.right * Xmove * maxSpeed * Time.deltaTime);
    }

    public static bool CanUse = false;
    public static bool isUsed = false;
    void UseThing()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnimator.SetTrigger("Use");
            isUsed = true;
        }
    }


    // импульс после атаки (для скрипта Damage)
    public void AfterHit(int HitForce)
    {
        playerRb.velocity = Vector2.zero;

        playerRb.AddForce(new Vector2(Enemy.direction * HitForce, 0));
    }

    #region Ground Check
    public Transform GrCheck;
    public LayerMask Ground;
    bool isGrounded;
    float CheckRad = 0.2f;
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GrCheck.position, CheckRad, Ground);
    }
    #endregion

    #region Wall Check
    //Создание точек для проверки
    float CheckRadius = 0.01f;
    public Transform WC_right;
    public Transform WC_left;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(WC_right.position, CheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(WC_left.position, CheckRadius);
    }
    // обнаружение стен
    public LayerMask Wall;
    bool rightWall;
    bool LeftWall;
    private void WallCheck()
    {
        rightWall = Physics2D.OverlapPoint(WC_right.position, Wall);
        LeftWall = Physics2D.OverlapPoint(WC_left.position, Wall);
    }
    #endregion
}
