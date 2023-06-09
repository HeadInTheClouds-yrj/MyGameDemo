using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ItemControl : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;
    public void Start()
    {
    }
    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);
        Destroy(gameObject);
    }
    public void AddItem(Item newitem)
    {
        item = newitem;
    }
    public void UseItem()
    {
        switch (item.itemkinde)
        {
            case Itemkinde.RangedWeapen:
                PlayerManager.instance.changeWearpon(item.icon, item.value, item.prefeberPath);
                break;
        }
    }
    
}
