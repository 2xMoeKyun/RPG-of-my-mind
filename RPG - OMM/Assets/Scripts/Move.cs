using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Animator anim;
    public float maxSpeed = 3f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        if (inputx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            anim.Play("Move_right");
        }
        if (inputx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.Play("Move_right");
        }
        transform.Translate(Vector2.right * inputx * maxSpeed * Time.deltaTime);
    }
}
