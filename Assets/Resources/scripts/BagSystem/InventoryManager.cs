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
        if (Items.Contains(item))
        {
            item.itemCont++;
            PlayerManager.instance.playerData.itemIds[item.id]++;
        }
        else
        {
            Items.Add(item);
        }
    }
    public void ReduceItemCont(Item item)
    {
        if (item.itemCont > 0)
        {
            item.itemCont--;
        }
        else
        {
            RemoveItem(item);
        }
    }
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        PlayerManager.instance.playerData.itemIds.Remove<string,int>(item.id,out item.itemCont);
    }
    private void InitializeAllItems()
    {
        allItems = Resources.LoadAll<Item>("Items");
    }
    public void ListItems()
    {
        itemContent = UIManager.instance.GetUI("Property", "Content_N").transform;
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemCont = obj.transform.Find("cont").GetComponent<TMP_Text>();
            var itemname = obj.transform.Find("Itemname").GetComponent<TMP_Text>();
            var itemicon = obj.transform.Find("Itemicon").GetComponent<Image>();
            var itemrmbutton = obj.transform.Find("RmButton").GetComponent<Button>();
            itemCont.text = item.itemCont.ToString();
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
        Items = new List<Item>();
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
        Items.Clear();
        foreach (string id in gameData.datas[0].itemIds.Keys)
        {
            Item item = GetItemById(id);
            Debug.Log(item == null);
            item.itemCont = gameData.datas[0].itemIds[id];
            AddItem(item);
        }
    }

    public void SaveGame(GameData gameData)
    {
    }
}
