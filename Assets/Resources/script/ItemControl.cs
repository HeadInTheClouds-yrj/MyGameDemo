using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);
        Destroy(gameObject);
    }
    public void AddItem(Item item)
    {
        this.item = item;
    }
    public void UseItem()
    {
        item.UseItem(item.itemkinde);
    }
}
