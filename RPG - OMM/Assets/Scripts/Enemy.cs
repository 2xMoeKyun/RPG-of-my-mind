using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Other
    private Transform target;
    private Animator anim;
    //Follow
    public float speed = 1f;
    public float followDistance = 5f;
    public float _followDistance = 1f; // Stop Follow Distance 
    //Atack
    public bool isAtack = false;
    public float atkDistance = 1f;
    public float atkSpeed = 3f;
    // Atk reload
    float recharge = 0f;
    float startRecharge = 2f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
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

    void Atack()
    {
        isAtack = true;
       
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
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
