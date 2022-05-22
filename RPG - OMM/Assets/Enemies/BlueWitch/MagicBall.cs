using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float speed;

    private GameObject player;
    private float direction;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if (player.transform.position.x > transform.position.x)
        {
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        StartCoroutine(CoolDown());
    }
    private void Update()
    {
        transform.Translate(Vector2.right * (direction * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Damage>().Hit(collision);
            Destroy(gameObject);

        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
