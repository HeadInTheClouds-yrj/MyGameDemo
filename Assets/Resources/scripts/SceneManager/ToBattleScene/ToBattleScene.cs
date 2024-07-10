using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class ToBattleScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadSceneAsync(5);
        }
        if (collision.tag.Equals("Player") && SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadSceneAsync(2);
        }
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
