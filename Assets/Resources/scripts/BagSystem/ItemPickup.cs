using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour,IDataPersistence
{
    [SerializeField]
    private Item item;
    private bool playerIsNear; 
    void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
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
