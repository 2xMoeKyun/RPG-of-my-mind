using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject ObjShow;
    public GameObject InventoryMainObj;

    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObj;
    public Vector2 offset;

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.img;
        if(count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;
        if (invItem.count > 1 && invItem.id != 0)//для стака элементов
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for(var i = 0; i < maxCount; i++)
        {
            GameObject NewItem = Instantiate(ObjShow, InventoryMainObj.transform) as GameObject;
            NewItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObject = NewItem;

            RectTransform rt = NewItem.GetComponent<RectTransform>();
            rt.localPosition = Vector2.zero;
            rt.localScale = Vector2.one;
            NewItem.GetComponentInChildren<RectTransform>().localScale = Vector2.one;

            Button tempButton = NewItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }


    public void UpdateInventory()
    {
        for(var i = 0; i < maxCount; i++)
        {
            if(items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }



    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObj.gameObject.SetActive(true);
            movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            AddInventoryItem(currentID, items[int.Parse(es.currentSelectedGameObject.name)]);

            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            currentID = -1;

            movingObj.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector2 pos = Input.mousePosition + offset;
        //pos.z
        movingObj.position = cam.ScreenToWorldPoint(pos); 
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.id = old.id;
        New.itemGameObject = old.itemGameObject;
        New.count = old.count;
        return New;
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;
}