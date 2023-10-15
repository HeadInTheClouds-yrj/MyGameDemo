using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour,IDataPersistence
{
    [SerializeField]
    public Item item;
    void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        Destroy(gameObject);
    }
    public void isenter()
    {
        if ((PlayerManager.instance.PlayerTransform.position - transform.position).magnitude <= 1f)
        {
            Pickup();
        }
    }
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isenter();
    }

    public void LoadGame(GameData gameData)
    {
        foreach (string itemId in gameData.BagItemsId)
        {
            if (itemId == item.id)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}
