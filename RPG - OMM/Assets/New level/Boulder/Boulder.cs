using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startPosY;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosY = transform.GetChild(0).position.y;
    }
    private void Update()
    {
        transform.GetChild(0).position = new Vector2(transform.position.x - 0.6f, startPosY);
        transform.GetChild(0).rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0,-90) * Time.deltaTime);
        rb.velocity = new Vector2(1, 0);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Destroy")
        {
            Destroy(collision.gameObject);
        }
    }

}
