using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDisplayUI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask layer;
    private Collider2D collider;
    private bool playerIsNear = false;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnEnable() {
        EventManager.Instance.inputEvent.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable () {
        EventManager.Instance.inputEvent.onSubmitPressed -= SubmitPressed;
    }
    void Update()
    {
        collider = Physics2D.OverlapCircle(player.position, 0.1f, layer);
        if (collider != null)
        {
            playerIsNear = true;
        }else{
            playerIsNear = false;
        }
    }
    private void SubmitPressed(){
        if (playerIsNear){
            collider.GetComponent<Interactives>().EnterBuilding();
        }
    }
}
