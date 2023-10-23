using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour,IDataPersistence
{
    public static InventoryManager Instance;
    private List<Item> Items;
    private Item[] allItems;
    [SerializeField]
    private Transform itemContent;
    [SerializeField]
    private GameObject inventoryItem;
    [SerializeField] private Toggle rmToggle;
    private ItemControl[] itemControlsList;
    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
    private void InitializeAllItems()
    {
        allItems = Resources.LoadAll<Item>("Items");
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
    public Item GetItemById(string id)
    {
        foreach (Item item in allItems)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
    public List<Item> GetInBagItems()
    {
        return Items;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitializeAllItems();
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(GameData gameData)
    {
        foreach (string itemId in gameData.BagItemsId)
        {
            AddItem(GetItemById(itemId));
        }
    }

    public void SaveGame(GameData gameData)
    {
        foreach (Item item in Items)
        {
            if (!item.id.Equals(gameData.BagItemsId))
            {
                gameData.BagItemsId.Add(item.id);
            }
        }
    }
}
