using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public static bool HitTaken;
    public static bool HitTakenEnemy;
    public void TakeHit(int damage, Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            health -= damage;
            Enemy en = GetComponent<Enemy>();
            en.HitTake();
            if(health <= 0)
            {
                en.Death();
            }
        }
        else if (col.CompareTag("Player"))
        {
            health -= damage;
            
        }
        
    }
}
