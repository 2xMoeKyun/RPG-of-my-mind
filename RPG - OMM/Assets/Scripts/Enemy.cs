using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Other
    private Transform target;
    private Animator anim;
    private Rigidbody2D rb;
    //Follow
    public float speed;
    public float followDistance;
    public float _followDistance; // Stop Follow Distance 
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
        if (Health.HitTakenEnemy)
        {
            anim.SetTrigger("TakeHit");
            Health.HitTakenEnemy = false;
        }
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
    public Collider2D Hitplayer;
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
        Hitplayer = Physics2D.OverlapCircle(attackRange.position, atkRad, playerLayer);
        Damage d = GetComponent<Damage>();
        d.Hit(Hitplayer);
        Hitplayer = null;
    }

    public void ResetAttack()
    {
        anim.SetTrigger("AtkEnd");
        anim.ResetTrigger("Atk");
        CanAttack = false;
    }

    IEnumerator AfterAttack()
    {
        yield return new WaitForSeconds(atkSpeed + 1f);
        CanAttack = true;
    }
    //
    #endregion

    void Follow()
    {
        anim.SetBool("Going", true);
        if (transform.position.x < target.position.x) // vpravo
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    public void Death()
    {
        anim.SetTrigger("Die");
        
    }

    public void AfterDeath()
    {
        Destroy(gameObject);
    }
}
