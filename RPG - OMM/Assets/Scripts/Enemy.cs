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
    public static float direction;
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
            anim.SetBool("Going", false);
            isAtack = true;
            anim.SetTrigger("Atk");            
        }
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

    void Follow()
    {
        anim.SetBool("Going", true);
        if (transform.position.x < target.position.x) // vpravo
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
