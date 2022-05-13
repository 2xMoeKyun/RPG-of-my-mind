using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTrader : MonoBehaviour
{
    public GameObject tradeUI;
    public GameObject mainThings;
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
        Debug.Log("Trade Active!");
        tradeUI.SetActive(true);
        mainThings.SetActive(true);
        mainThings.transform.SetParent(tradeUI.transform);
        fts.Fill();
        coinText.text = coins.coinsCount.ToString();
        move.DisablePlayer();
    }


    public void BackButtonClick()
    {
        tradeUI.SetActive(false);
        mainThings.SetActive(false);
        fts.UnFill();
        DTriggerObject.reloadtrade = false;
        NPC.trade = false;
        move.AblePlayer();
    }
}
