using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public string Tag;
    //public int HitForce;

    public void Hit(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.TakeHit(damage, collision);
            //if (collision.gameObject.tag == "Player")
            //{
            //    Health.HitTaken = true;
            //}
            //else
            //{
            //    Health.HitTakenEnemy = true;
            //}
        }
    }
}
