using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image healthBarImage;
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            healthBarImage = GetComponent<Image>();
        }
    }
    public void TakeHit(int damage)
    {
        health -= damage;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            healthBarImage.fillAmount -= 0.1f;
        }
        if (health <= 0)
        {
            
            //Destroy(gameObject);
        }
    }

}
