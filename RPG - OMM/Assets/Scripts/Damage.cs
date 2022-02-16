using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public string Tag;
    public int HitForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            Move move = collision.gameObject.GetComponent<Move>();
            health.TakeHit(damage);
            Move.playerAnimator.Play("TakenDamage");
            move.AfterHit(HitForce);
        }
    }


}
