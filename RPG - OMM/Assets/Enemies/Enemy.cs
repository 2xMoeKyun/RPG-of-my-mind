using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private WitchVariables witchVariables;
    private bool reloadAtk2;
    //Type of enemy
    public bool Witch;
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
    public float direction = 1;


    void Start()
    {
        if (Witch)
        {
           witchVariables =  GetComponent<WitchVariables>();
        }
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < followDistance && !isAtack)
        {
            Follow();
        }
        if (Witch)
        {
            if (Vector2.Distance(transform.position, target.position) < witchVariables.atk2Distance && CanAttack && !reloadAtk2)
            {
                //Attack2 function

                reloadAtk2 = true;
                anim.SetBool("Going", false);
                isAtack = true;
                anim.SetTrigger("Atk2");
                GameObject mb = Instantiate(witchVariables.magicBall);
                mb.transform.position = new Vector2(transform.position.x + (direction * 1f), transform.position.y + Random.Range(-0.1f, 0.7f));
                mb.SetActive(true);
            }
        }
        if (Vector2.Distance(transform.position, target.position) < atkDistance && CanAttack)
        {
            //Attack function
            anim.SetBool("Going", false);
            isAtack = true;
            anim.SetTrigger("Atk");
        }
        else if (Vector2.Distance(transform.position, target.position) > atkDistance + 1f)
        {
            isAtack = false;
        }

    }

    public void HitTake()
    {
        Debug.Log("Hit taken");
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
        if (hitPlayer == null)
        {
            return;
        }
        Damage d = GetComponent<Damage>();
        d.Hit(hitPlayer);
        hitPlayer = null;
    }

    public void ResetAttack()
    {
        Follow();
        anim.SetTrigger("AtkEnd");
        if (Witch)
        {
            anim.ResetTrigger("Atk2");
        }
        else
        {
            anim.ResetTrigger("Atk");
        }
        CanAttack = false;
    }

    IEnumerator MagicBalCD()
    {
        yield return new WaitForSeconds(3f);
        CanAttack = true;
        reloadAtk2 = false;
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
            if (Slime )
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Skeleton || Witch)
            {
                 GetComponent<SpriteRenderer>().flipX = false;
            }
            StartCoroutine(FollowCD());
        }
        else if(transform.position.x > target.position.x && CanTurn)
        {
            CanTurn = false;
            
            direction = -1;
            if (Slime )
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Skeleton || Witch)
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
        
        anim.SetTrigger("Die");
        if (Witch)
        {
            reloadAtk2 = true;
        }
        GetComponent<BoxCollider2D>().enabled = false;
        followDistance = 0;
    }

    public void AfterDeath()
    {

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        if (Skeleton && isTriggerEnemy)
        {
            Boulder.boulderStart = true;
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

}
