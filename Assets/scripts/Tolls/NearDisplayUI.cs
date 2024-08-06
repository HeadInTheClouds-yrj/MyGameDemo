using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDisplayUI : MonoBehaviour
{
    private static  NearDisplayUI Instance;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layer;
    private Collider2D collider;
    private bool playerIsNear = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player = PlayerManager.instance.transform;
    }


}
