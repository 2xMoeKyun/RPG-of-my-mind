using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTrader : MonoBehaviour
{
    public GameObject tradeUI;
    public FillTheSlots fts;


    public void TradeTrigger()
    {
        tradeUI.SetActive(true);
        fts.Fill();
    }
}
