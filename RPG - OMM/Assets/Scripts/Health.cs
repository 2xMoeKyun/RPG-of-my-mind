using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public static bool HitTaken;
    public void TakeHit(int damage)
    {
        health -= damage;
        HitTaken = true;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
