using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public static int HitTaken;

    private int hitTakecd;
    public void TakeHit(int damage, Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            health -= damage;
            Enemy en = GetComponent<Enemy>();
            if(hitTakecd != 2)
            {
                
                hitTakecd++;
                en.HitTake();
            }
            else
            {
                StartCoroutine(CoolDown());
            }
            if (health <= 0 )
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


    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        hitTakecd = 0;
        
    }
}
