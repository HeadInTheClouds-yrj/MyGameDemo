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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        player = PlayerManager.instance.transform;
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
