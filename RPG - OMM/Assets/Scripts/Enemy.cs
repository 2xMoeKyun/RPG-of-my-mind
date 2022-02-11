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
    public float speed = 1f;
    public float followDistance = 5f;
    public float _followDistance = 1f; // Stop Follow Distance 
    //Atack
    bool isAtack = false;
    float atkDistance = 1f;
    float atkSpeed = 6f;
    float atkCurve;
    // Atk reload
    float recharge = 0f;
    float startRecharge = 2f;
    //other
    float direction;
    float atkMove;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if((Vector2.Distance(transform.position, target.position) < followDistance) && !isAtack)
        {
            Follow();
        }
        if(Vector2.Distance(transform.position, target.position) < atkDistance)
        {
            Atack();
        }
        else
        {
            isAtack = false;
        }
    }

    private int lunge = 5000;
    void Atack()
    {
        isAtack = true;

        rb.AddForce(new Vector2(direction, 0) * lunge);

        Debug.Log(lunge);

        if (recharge <= 0)
        {
            anim.Play("Atack");
            recharge = startRecharge;
        }
        else
        {

            recharge -= Time.deltaTime;
        }
    }
    void Follow()
    {
        if (Vector2.Distance(transform.position, target.position) > _followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
        }
        if (transform.position.x < target.position.x) // vpravo
        {
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
