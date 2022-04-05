using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int Damage;
    public int PushAwayForce;
    public GameObject playerCollider2D;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Health h = collision.gameObject.GetComponent<Health>();
            h.TakeHit(Damage, playerCollider2D.transform.GetComponent<Collider2D>());
            Move.playerRb.AddForce(new Vector2(0, collision.transform.position.y + PushAwayForce));
            Debug.Log(11);
        }
    }
}
