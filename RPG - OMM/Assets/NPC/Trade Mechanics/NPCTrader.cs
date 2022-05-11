using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTrader : MonoBehaviour
{
    public GameObject tradeUI;
    public FillTheSlots fts;
    public Text coinText;

    private Coins coins;
    private Move move;
    private void Start()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();

        coins = GameObject.FindGameObjectWithTag("Player").GetComponent<Coins>();
    }


    public void TradeTrigger()
    {
        tradeUI.SetActive(true);
        fts.Fill();
        coinText.text = coins.coinsCount.ToString();
        move.DisablePlayer();
    }


    public void BackButtonClick()
    {
        tradeUI.SetActive(false);
        fts.UnFill();
        move.AblePlayer();
    }
}
