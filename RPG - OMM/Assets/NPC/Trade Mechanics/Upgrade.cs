using System;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;

    public Text buffText;

    public int buffAtk;
    public int buffMaxHp;
    public float buffMaxSpeed;
    private string[] atkBuff = { "Red", "Pants" };
    private bool[] isAtkBuff;
    private string[] maxHPBuff = { "Crisps", "Donut" };
    private bool[] isMaxHPBuff;
    private string[] maxSpeed = { "Blue", "Disk" };
    private bool[] isMaxSpeed;

    private bool[] mayDeletable;

    public GameObject tips;

    public SlotManager sm;
    public GameObject player;
    private Bag bag;

    private void Start()
    {
        bag = GameObject.FindGameObjectWithTag("Bag").GetComponent<Bag>();
        mayDeletable = new bool[bag.BagSlots.Length];
        isAtkBuff = new bool[atkBuff.Length];
        isMaxHPBuff = new bool[maxHPBuff.Length];
        isMaxSpeed = new bool[maxSpeed.Length];
    }

    public void TipsEnter()
    {
        tips.SetActive(true);
    }

    public void TipsExit()
    {
        tips.SetActive(false);
    }


    private bool minus3uhu;
    public void UpgradeButton()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                return;
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            var child = slots[i].transform.GetChild(0).GetComponent<BagItemUI>();
            for (int j = 0; j < atkBuff.Length; j++)
            {
                if (child.itemName == atkBuff[j])
                {
                    isAtkBuff[j] = true;
                }
                else if(child.itemName == maxHPBuff[j])
                {
                    isMaxHPBuff[j] = true;
                }
                else if (child.itemName == maxSpeed[j])
                {
                    isMaxSpeed[j] = true;
                }
            }
        }

        for (int i = 0; i < atkBuff.Length; i++)
        {
            if (isAtkBuff[i] == true && isAtkBuff[i + 1] == true)
            {
                buffText.text = "Attack: +" + buffAtk.ToString();
                player.GetComponent<Damage>().damage += buffAtk;
                DeleteAllObjects();
                isAtkBuff[i] = false;
                isAtkBuff[i + 1] = false;
                break;
            }
            else if (isMaxHPBuff[i] == true && isMaxHPBuff[i + 1] == true)
            {
                buffText.text = $"Max HP: +" + buffMaxHp.ToString();
                player.GetComponent<Health>().maxHealth += buffMaxHp;
                player.GetComponent<Health>().health = player.GetComponent<Health>().maxHealth;
                DeleteAllObjects();
                isMaxHPBuff[i] = false;
                isMaxHPBuff[i+1] = false;
                break;
            }
            else if (isMaxSpeed[i] == true && isMaxSpeed[i + 1] == true)
            {
                buffText.text = $"Max Speed: +" + buffMaxSpeed.ToString();
                player.GetComponent<Move>().maxSpeed += buffMaxSpeed;
                DeleteAllObjects();
                isMaxSpeed[i] = false;
                isMaxSpeed[i+1] = false;
                break;
            }

        }
    }



    private void DeleteAllObjects()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(slots[i].transform.GetChild(0).gameObject);
            isFull[i] = false;
           
        }
        for (int i = 0; i < bag.BagSlots.Length; i++)
        {
            if (mayDeletable[i] == true)
            {
                mayDeletable[i] = false;
                Destroy(bag.BagSlots[i].transform.GetChild(1).gameObject);
                bag.isBagFull[i] = false;
            }
        }
        
    }

    public void SetButton()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            for (int j = 0; j < SellItem.sellItem.Length; j++)
            {
                if(isFull[i] == false && SellItem.isActive[j] == true)
                {
                    isFull[i] = true;

                    mayDeletable[j] = true;

                    SellItem.sellItem[j].transform.GetChild(0).SetParent(slots[i].transform);
                    slots[i].transform.GetChild(0).position = slots[i].transform.position;

                    sm.isSlotFull[j] = false;
                    SellItem.isPresseds[j] = 3;
                    //
                    SellItem.sellItem[j].GetComponent<Image>().color = new Color32(91, 88, 88, 255);
                    SellItem.sellItem[j] = null;
                    //
                    SellItem.isActive[j] = false;
                }
            }
        }
    }

    public void ResetButton()
    {
        if (isFull[0] == true)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                for (int j = 0; j < SellItem.sellItem.Length; j++)
                {
                    if (isFull[i] == true && sm.isSlotFull[j] == false)
                    {
                        isFull[i] = false;

                        slots[i].transform.GetChild(0).SetParent(sm.Slots[j].transform);
                        sm.Slots[j].transform.GetChild(0).position = sm.Slots[j].transform.position;

                        sm.isSlotFull[j] = true;
                    }
                }
            }
        }
    }
}
