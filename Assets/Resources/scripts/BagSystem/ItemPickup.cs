using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour,IDataPersistence
{
    [SerializeField]
    private Item item;
    private bool playerIsNear;
    private void Start()
    {
        CheckPickupedItem();
    }
    void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        PlayerManager.instance.playerData.pickupedItemGameObj.Add(this.name);
        PlayerManager.instance.playerData.itemIds.Add(item.id, item.itemCont);
        EventManager.Instance.questEvent.PickUpItem();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.inputEvent.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        EventManager.Instance.inputEvent.onSubmitPressed -= SubmitPressed;
    }

    private void SubmitPressed()
    {
        if (playerIsNear)
        {
            Pickup();
        }
    }

    public void LoadGame(GameData gameData)
    {

    }

    private void CheckPickupedItem()
    {
        if (PlayerManager.instance.playerData.pickupedItemGameObj != null)
        {
            if (PlayerManager.instance.playerData.pickupedItemGameObj.Contains(this.name))
            {
                Destroy(gameObject);
            }
        }
    }
    public void SaveGame(GameData gameData)
    {
        gameData.datas[0].pickupedItemGameObj.Clear();
        foreach (var item in gameData.datas[0].pickupedItemGameObj)
        {
            gameData.datas[0].pickupedItemGameObj.Add(item);
        }
    }
}
