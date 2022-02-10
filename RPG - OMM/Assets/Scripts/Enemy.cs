using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject player;

    //Follow
    float speed = 1f;
    float followDistance = 5f;
    // Atack
    float atackDistance = 1.5f;
    float atackSpeed = 3f;
    // position y
    public float Ydistance = 0;
    // recharge
    float recharge = 0f;
    float startRecharge = 2f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Hero");
    }

    bool isAtack = false;
    void Update()
    {
        float distantToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (!isAtack)
        {

            if (distantToPlayer < followDistance)
            {
                Follow();
            }
            else
            {
                StopFollow();
            }
        }
        if (distantToPlayer < atackDistance)
        {
            Atack();
        }
        else
        {
            
            StopAtack();
        }
    }

    void Atack()
    {
        isAtack = true;

        if (recharge <= 0)
        {
            GroundCheck();
            if (player.transform.position.x < transform.position.x)// идет влево
            {
                rb.velocity = new Vector2(-atackSpeed, Ydistance);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(atackSpeed, Ydistance);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            anim.Play("Atack");
            recharge = startRecharge;
        }
        else
        {

            recharge -= Time.deltaTime;
        }
    }

    void StopAtack()
    {
        isAtack = false;
        anim.SetBool("Atack", false);
    }
    void Follow()
    {
        GroundCheck();
        if (player.transform.position.x < transform.position.x)// идет влево
        {
            rb.velocity = new Vector2(-speed, Ydistance);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, Ydistance);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void StopFollow()
    {
        rb.velocity = new Vector2(0, 0);
    }

    // Ground check
    public Transform GrCheck;
    public LayerMask Ground;
    bool isGrounded;
    float CheckRad = 0.5f;
    GameObject obj;
    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GrCheck.position, CheckRad, Ground);
        obj = GameObject.FindGameObjectWithTag("Ground");
        if (isGrounded == true)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            Ydistance = obj.transform.position.y + 1f;
        }
        if (isGrounded == false)
        {
            
            if(obj.transform.position.y < transform.position.y)
            {

                Ydistance = obj.transform.position.y;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }

    }
    //
}
