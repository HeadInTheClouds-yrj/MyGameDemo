using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items;
    public Transform itemContent;
    public GameObject inventoryItem;
    public Toggle rmToggle;
    public ItemControl[] itemControlsList;
    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void RemoveItem(Item item)
    {
        Debug.Log(item);
        Items.Remove(item);
    }
    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemname = obj.transform.Find("Itemname").GetComponent<TMP_Text>();
            var itemicon = obj.transform.Find("Itemicon").GetComponent<Image>();
            var itemrmbutton = obj.transform.Find("RmButton").GetComponent<Button>();
            itemname.text = item.itemname;
            itemicon.sprite = item.Bagicon;
            if (rmToggle.isOn)
            {
                itemrmbutton.gameObject.SetActive(true);
            }
        }
        setItemcontrol();
    }
    public void EnableItemRemove()
    {
        if (rmToggle.isOn)
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RmButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RmButton").gameObject.SetActive(false);
            }
        }
    }
    public void setItemcontrol()
    {
        itemControlsList = itemContent.GetComponentsInChildren<ItemControl>();
        if (itemControlsList.Length == Items.Count)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                itemControlsList[i].AddItem(Items[i]);
            }
        }
        else
        {
            for (int i = 0; i < Items.Count; i++)
            {
                itemControlsList[i+ itemControlsList.Length - Items.Count].AddItem(Items[i]);
            }
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
