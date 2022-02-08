using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public GameObject player;
    public float agroDistance = 5f;
    public float speed = 1f;
    public float Ydistance = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Hero");
    }

    void Update()
    {

        float distantToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distantToPlayer < agroDistance)
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
        GroundCheck();
        if (player.transform.position.x < transform.position.x)// идет влево
        {
            rb.velocity = new Vector2(-speed, Ydistance);
            anim.Play("Fly");
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, Ydistance);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void StopAtack()
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

            Debug.Log(Ydistance);
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
            Debug.Log("-------------------------------");
        }

    }
    //
}
