using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isenter();
    }
}
