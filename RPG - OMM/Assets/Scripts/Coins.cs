using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public int coinsCount;
    public Text T;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinsCount++;
            UpdateCoinsCount(coinsCount);
            collision.gameObject.SetActive(false);
        }
    }

    public void UpdateCoinsCount(int coinsCount)
    {
        T.text = coinsCount.ToString();
    }
}
