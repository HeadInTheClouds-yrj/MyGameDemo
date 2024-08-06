using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowControl : MonoBehaviour
{
    public float swordVelocity;
    public Rigidbody2D rb;
    public float playerDamge;
    public Vector2 direction;
    [SerializeField]
    private LayerMask targetLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,2f);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.rotation * Vector3.down * swordVelocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dictionary<string,NpcCell> allNpc = NpcManager.instance.getAllNpcCell();
        NpcCell cell = collision.gameObject.GetComponent<NpcCell>();
        Debug.Log(collision.transform.name);
        Debug.Log(cell == null);
        if (cell != null && !cell.IsDestroyed())
        {
            cell.NpcReduceHP(Mathf.Round(playerDamge));
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dictionary<string, NpcCell> allNpc = NpcManager.instance.getAllNpcCell();
        if (10.Equals(collision.gameObject.layer))
        {
            NpcCell cell = collision.gameObject.GetComponent<NpcCell>();
            if (cell != null && !cell.IsDestroyed())
            {
                if (cell.npcData.survival)
                {
                    cell.NpcReduceHP(Mathf.Round(playerDamge));

                }
            }
        }
        
    }
}
