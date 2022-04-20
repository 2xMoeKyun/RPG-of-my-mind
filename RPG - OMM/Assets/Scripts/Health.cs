using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public static int HitTaken;
    public void TakeHit(int damage, Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            health -= damage;
            Enemy en = GetComponent<Enemy>();
            en.HitTake();
            if (health <= 0)
            {
                en.Death();
            }
        }
        else if (col.name == "AlternativeHitBox")
        {
            HitTaken = damage;
        }
        else if (col.CompareTag("Player"))
        {
            
            health -= damage;
            Move m = GetComponent<Move>();
            m.GetHitAnimation();
            if (health <= 0)
            {
                m.Death();
            }

        }
        

    }
}
