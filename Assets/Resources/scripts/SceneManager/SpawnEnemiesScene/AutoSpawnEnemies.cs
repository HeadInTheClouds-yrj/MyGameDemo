using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AutoSpawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            InvokeRepeating("Factory", 1f, 5f);
        }
    }
    private void Factory()
    {
        for (int i = 0; i < 4; i++)
        {
            NpcManager.instance.factoryNpc();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SceneManager.LoadSceneAsync(6);
        }
    }
}
