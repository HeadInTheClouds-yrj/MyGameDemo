using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowControl : MonoBehaviour
{
    public float swordVelocity;
    public Rigidbody2D rb;
    public float playerDamge;
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
        collision.gameObject.GetComponent<NpcCell>().NpcReduceHP(Mathf.Round(playerDamge));
        Destroy(gameObject);
    }
}
