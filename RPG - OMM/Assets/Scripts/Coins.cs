using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public int coinsCount;
    public Text T;
    public static bool isSit;
    public static bool isStay = true;

    private bool CanTake = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.CompareTag("Coin") && !isSit && isStay && CanTake)
        {
            Debug.Log(1);
            CanTake = false;
            Destroy(collision.gameObject);
            coinsCount++;
            UpdateCoinsCount(coinsCount);
            StartCoroutine(Wait());
        }
        else if(collision.transform.CompareTag("Coin") && isSit && !isStay && CanTake)
        {
            CanTake = false;
            Destroy(collision.gameObject);
            coinsCount++;
            UpdateCoinsCount(coinsCount);
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return null;
        CanTake = true;
    }
     
    public void UpdateCoinsCount(int coinsCount)
    {
        T.text = coinsCount.ToString();
    }
}
