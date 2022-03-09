using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public int coinsCount;
    Text t;

    private void Start()
    {
        t = GameObject.FindGameObjectWithTag("Coin").GetComponentInChildren<Text>();
    }

    private void Update()
    {
        t.text = coinsCount.ToString();
    }
}
