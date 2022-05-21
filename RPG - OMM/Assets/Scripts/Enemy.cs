using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Type of enemy
    public bool Slime;
    public bool Skeleton;
    public bool isTriggerEnemy;
    //Other
    private Transform target;
    private Animator anim;
    private Rigidbody2D rb;
    //Follow
    public float speed;
    public float followDistance;
   // public float StopfollowDistance; // Stop Follow Distance 
    //Attack
    private bool isAtack = false;
    public float atkDistance;
    public float atkSpeed;
    private bool CanAttack = true;
    //other
    public static float direction = 1;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < followDistance && !isAtack)
        {
            Follow();
        }
        if (Vector2.Distance(transform.position, target.position) < atkDistance && CanAttack)
        {
            //Attack function
            anim.SetBool("Going", false);
            isAtack = true;
            anim.SetTrigger("Atk");            
        }
        else if(Vector2.Distance(transform.position, target.position) > atkDistance + 1f)
        {
            isAtack = false;
        }
    }

    public void HitTake()
    {
        anim.SetTrigger("TakeHit");
    }

    #region for switch color while taken damage
    public void ColorRed()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void ColorTransparent()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    #endregion

    #region attack

    //these functions invokes on animation ivents
    public Collider2D hitPlayer;
    public LayerMask playerLayer;
    public Transform attackRange;
    public Transform AR_right;
    public Transform AR_left;
    private float atkRad = 0.4f;
    public void HitPlayer()
    {
        if (direction == 1)
        {
            attackRange.position = AR_right.position;
        }
        else
        {
            attackRange.position = AR_left.position;
        }
        hitPlayer = Physics2D.OverlapCircle(attackRange.position, atkRad, playerLayer);
        if(hitPlayer == null)
        {
            return;
        }
        Damage d = GetComponent<Damage>();
        d.Hit(hitPlayer);
        hitPlayer = null;
    }

    public void ResetAttack()
    {
        anim.SetTrigger("AtkEnd");
        anim.ResetTrigger("Atk");
        CanAttack = false;
    }

    IEnumerator AfterAttack()
    {
        yield return new WaitForSeconds(atkSpeed + 0.5f);
        CanAttack = true;
    }
    //
    #endregion


    private bool CanTurn = true;
    void Follow()
    {
        anim.SetBool("Going", true);
        if (transform.position.x < target.position.x && CanTurn) // vpravo
        {
            CanTurn = false;
            direction = 1;
            if (Slime)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Skeleton)
            {
                 GetComponent<SpriteRenderer>().flipX = false;
            }
            StartCoroutine(FollowCD());
        }
        else if(transform.position.x > target.position.x && CanTurn)
        {
            CanTurn = false;
            
            direction = -1;
            if (Slime)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Skeleton)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            StartCoroutine(FollowCD());
        }

        transform.position = new Vector2(transform.position.x + (speed * direction) * Time.deltaTime, transform.position.y);
    }

    private IEnumerator FollowCD()
    {
        yield return new WaitForSeconds(0.3f);
        CanTurn = true;
    }


    public void Death()
    {
        if(Skeleton && isTriggerEnemy)
        {
            Boulder.boulderStart = true;
        }
        anim.SetTrigger("Die");
        
    }

    public void AfterDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
