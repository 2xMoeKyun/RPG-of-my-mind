using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Move : MonoBehaviour
{
    public static Animator playerAnimator;
    public static Animation playerAnimation;
    public static Rigidbody2D playerRb;
    private BoxCollider2D playerCollider;
    public GameObject pauseMenu;
    //
    public float maxSpeed = 3f;
    public float Jforce = 6f;
    private float Xmove;
    public static int HitForce;
    public int dashForce = 500;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimation = GetComponent<Animation>();
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    public static bool playerDialogue;
    private int getDialogue;
    public static bool fUsed;
    public static bool CanInteract = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.F) && !fUsed && CanInteract)
        {
            fUsed = true;
            StartCoroutine(Fcooldown());
        }
        if(transform.GetChild(0).GetChildCount() == getDialogue)
        {
            playerDialogue = false;
        }
        else if (playerDialogue && !DialogueManager.DialogueEnd )
        {
            DialogueManager.SwitchesCount++;
            Debug.Log(DialogueManager.SwitchesCount);
            if (DialogueManager.SwitchesCount == 2 )
            {
                DialogueManager.SwitchTo = "Rock";
            }
            transform.GetChild(0).GetChild(getDialogue).GetComponent<DialogueTrigger>().TriggerDialogue();
            getDialogue++;
        }
        GroundCheck();
        WallCheck();
        if ((Input.GetKeyDown(KeyCode.LeftControl) || isSitting) && CanSit)
        {
            MoveingSit();
        }
        if (!finish)
        {

            if (Input.GetKeyDown(KeyCode.J) && isGrounded && CanAttack)
            {
                Attack();
            }
            if (CanMove)
            {
                MoveX();
            } 

           
            if (CanJump && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_end"))
            {
                if (!isGrounded && !IsJump)
                {
                    Debug.Log("flu");
                    RealoadSit();
                    CanAttack = false;
                    startJump = true;
                }
                Jump();
            }
            if (CanUse)
            {
                UseThing();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
            {
                Dash();
            }
            else if(isDashing)
            {
                transform.Translate(Vector2.right * (playerDirection * dashSpeed) * Time.deltaTime);
            }
        }


    }


    private IEnumerator Fcooldown()
    {
        yield return new WaitForSeconds(0.1f);
        fUsed =false;
    }

    public static bool SetAblePlayer = true;
    public void DisablePlayer()
    {
        // P.S. Если в момент срабатывания функции песронаж прыгнул, то запрет прыжка находится в функции, которая вызывается в конце прыжка
        // Т.к. функция прыжка не может работать без апдейта
        if (!IsJump && !startJump)
        {
            CanJump = false;
        }
        RealoadSit();
        playerAnimator.SetBool("MovingRight", false);
        CanAttack = false;
        CanMove = false;
        CanSit = false;
        CanUse = false;
        CanDash = false;    
    }

    public void AblePlayer()
    {
        if (SetAblePlayer)
        {
            CanJump = true;
        }
        CanAttack = true;
        CanMove = true;
        CanSit = true;
        CanUse = true;
        CanDash = true;
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

    #region Get hit
    public void GetHitAnimation()
    {
        GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(CD());
    }

    private IEnumerator CD()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }
    #endregion

    public GameObject DeathUI;
    #region Death
    public void Death()
    {
        DeathUI.SetActive(true);
        gameObject.SetActive(false);
    }



    #endregion

    #region Dash
    public static bool CanDash = true;
    public static bool isDashing;
    private bool airDash = true;
    public float dashSpeed;
    public void Dash()
    {
        DisablePlayer();
        CanMove = true;
        isDashing = true;
        airDash = false;
        playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;
        playerAnimator.SetTrigger("DashStart");
        StartCoroutine(DashCD());
    }

    private IEnumerator DashCD()
    {
        yield return new WaitForSeconds(1.5f);
        playerRb.constraints = RigidbodyConstraints2D.None;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        AblePlayer();
        CanDash = false;
        isDashing = false;
        playerRb.simulated = true;
        playerAnimator.SetTrigger("DashEnd");
        StartCoroutine(DashCoolDown());
    }


    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(2f);
        CanDash = true;
        airDash = true;
    }

    #endregion

    public static bool CanJump = true;
    public static bool IsJump = false;
    private bool SuperJump;
    #region Jump
    bool onAirVal = false;// для определения, что перс был ввоздухе
    bool startJump = false;// для определения окончания стадии подготовки пржыка
    public void Jumping()//Вызывается в анимации подготовки прыжка
    {
        if (SuperJump)
        {
            Jforce += 0.5f;
            SuperJumpWorks = true;
        }
        playerRb.velocity = new Vector2(playerRb.velocity.x, Jforce);
        
    }
    public void StartingJumpAnimEnd()// вызывается в конце анимации подготовки прыжка
    {
        
        if (SuperJump)
        {
            Jforce -= 0.5f;
            SuperJump = false;
        }
        startJump = true;
        IsJump = false;
    }


    public void Jump()// вызывается в апдейте
    {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !startJump && isGrounded )
        {
            IsJump = true;
            RealoadSit();
            CanAttack = false;
            CanDash = false;
            playerAnimator.Play("StartingJump");// Подготовка к прыжку
            
        }
        else if (!isGrounded && startJump) //В воздухе
        {
            
            if (airDash)
            {
                Debug.Log("on air");
                CanDash = true;
            }
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
        CanAttack = true;
        if (!SetAblePlayer)
        {
            CanJump = false;
        }
        
    }
    #endregion


    #region Attack

    //for attack registration
    public Collider2D[] HitEnemies;
    public LayerMask enemyLayer;
    public Transform attackRange;
    public Transform AR_right;
    public Transform AR_left;
    private float atkRad = 0.4f;

    //

    public void AttackReg()
    {
        if(playerDirection == 1)
        {
            attackRange.position = AR_right.position;
        }
        else
        {
            attackRange.position = AR_left.position;
        }
        HitEnemies = Physics2D.OverlapCircleAll(attackRange.position, atkRad, enemyLayer);
        foreach (Collider2D enemyy in HitEnemies)
        {
            Damage d = GetComponent<Damage>();
            d.Hit(enemyy);
        }
        Array.Clear(HitEnemies, 0, HitEnemies.Length);
    }

    public static bool CanAttack = true;
    private int CurrentAtk = 0;
    public void Attack()
    {
        if (CurrentAtk == 0)
        {
            RealoadSit();
            CanSit = false;
            CanAttack = false;
            CanJump = false;
            CanMove = false;
            CanDash = false;
        }
        playerRb.velocity = Vector2.zero;
        CurrentAtk++;
        if (CurrentAtk >= 3)
        {
            CanAttack = false;
            playerAnimator.SetTrigger("AtkEnd");
        }
        playerAnimator.SetInteger("Atk", CurrentAtk);
        StartCoroutine(AttackTimer());
        StartCoroutine(AttackPause());
    }

    IEnumerator AttackPause()
    {
        yield return new WaitForSeconds(0.1f);
        CanAttack = true;
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1f);
        
        playerAnimator.SetTrigger("AtkEnd");
    }


    IEnumerator AfterAttack()
    {
        yield return new WaitForSeconds(0.3f);
        CanAttack = true;
    }

    public void CanGo()
    {
        CanSit = true; 
        CanJump = true;
        CanMove = true;
        CanDash = true;
        CurrentAtk = 0;
        playerAnimator.SetInteger("Atk", CurrentAtk);
    }
    #endregion


    #region Sit mechanics
    public static bool finish;//from script: Finish
    private float XSit;
    public static bool isSitting;
    public static bool CanSit = true;
    public GameObject AlternativeHitBox;

    private void RealoadSit()
    {
        Coins.isSit = false;
        Coins.isStay = true;
        playerCollider.enabled = true;
        AlternativeHitBox.SetActive(false);
        playerAnimator.SetBool("IdleSit", false);
        playerAnimator.SetBool("Sit", false);
        isSitting = false;
        CanMove = true;
        
    }

    private bool SuperJumpWorks;
    private IEnumerator ForSuperJump()
    {
        yield return new WaitForSeconds(1f);
        if (!IsJump && !SuperJumpWorks)
        {
            SuperJump = false;
        }
    }

    void MoveingSit()
    {
        Coins.isStay = false;
        Coins.isSit = true;

        if (Input.GetKeyDown(KeyCode.LeftControl) && !CanMove && !finish)
        {
            RealoadSit();
            return;
        }
        SuperJump = true;
        StartCoroutine(ForSuperJump());
        //resizing hit box
        AlternativeHitBox.SetActive(true);
        playerCollider.enabled = false;
        //
        isSitting = true;
        
        CanMove = false;
        playerAnimator.SetBool("IdleSit", true);
        XSit = Input.GetAxis("Horizontal");
        if (XSit == 0)
        {
            playerAnimator.SetBool("Sit", false);
        }
        if (XSit > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("Sit", true);
        }
        if (XSit < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            playerAnimator.SetBool("Sit", true);

        }
        transform.Translate(Vector2.right * XSit * (maxSpeed - 2f) * Time.deltaTime);
    }

#endregion


    public static bool isMoveing;
    public static bool CanMove = true;
    public static int playerDirection = 1;
    void MoveX()
    {
        isMoveing = true;
        Xmove = Input.GetAxis("Horizontal");
        // Check for walls
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
        //
        if (Xmove == 0)
        {
            playerAnimator.SetBool("MovingRight", false);
            isMoveing = false;
        }    
        if (Xmove > 0)
        {
            playerDirection = 1;
            GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("MovingRight", true);
        }
        if (Xmove < 0)
        {
            playerDirection = -1;
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
    public static bool isGrounded;
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
