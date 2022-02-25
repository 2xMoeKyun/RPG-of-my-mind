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
    public Vector3 offset;

    public GameObject BackGround;

    private void Start()
    {
        if(items.Count == 0)
        {
            AddGraphics();
        }

        for(var i = 0; i < maxCount; i++)//test
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1, 20));
        }

        UpdateInventory();
    }

    private void Update()
    {
        if(currentID != -1)
        {
            MoveObject();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BackGround.SetActive(!BackGround.activeSelf);
            if (BackGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for(var i = 0; i < maxCount; i++)
        {
            if(items[i].id == item.id)
            {
                if(items[0].count < 128)
                {
                    items[i].count += count;
                    if(items[i].count > 128)
                    {
                        count = items[i].count - 128;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if(count > 0)
        {
            for(var i = 0; i < maxCount; i++)
            {
                if(items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

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
        if (invItem.count > 1 && invItem.id != 0 && currentItem.id != 0)//дл€ стака элементов
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
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            NewItem.GetComponentInChildren<RectTransform>().localScale = Vector3.one;

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



    public void SelectObject( )
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObj.gameObject.SetActive(true);
            movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {

                AddInventoryItem(currentID, II );

                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if(II.count + currentItem.count <= 128)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 128);
                    II.count = 128;
                }

                II.itemGameObject.GetComponentInChildren<Text>().text = II.count.ToString();
            }


            currentID = -1;

            movingObj.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObj.GetComponent<RectTransform>().position.z;
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
    [Header("—юда ниче не надо")]
    public int id;
    public GameObject itemGameObject;
    public int count;
}