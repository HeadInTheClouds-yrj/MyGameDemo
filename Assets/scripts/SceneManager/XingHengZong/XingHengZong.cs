using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class XingHengZong : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private TMP_Text tmpText;
    private bool near =false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        tmpText = Instantiate(text, transform).GetComponent<TMP_Text>();

        tmpText.text = "V" ;
        
        near= true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tmpText.IsDestroyed())
        {
            tmpText = Instantiate(text, transform).GetComponent<TMP_Text>();

            tmpText.text = "V";
        }
        Destroy(tmpText.gameObject);
        near= false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)&&near)
        {
            SceneManager.LoadSceneAsync(3);
        }
    }
}
