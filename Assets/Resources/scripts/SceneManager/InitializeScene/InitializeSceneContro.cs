using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitializeSceneContro : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingNumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckInitalOver());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator CheckInitalOver()
    {
        float num = 0;
        string st = "";
        while (num<=100)
        {
            num += Time.deltaTime * 50;
            st = (int)num+"%";
            loadingNumber.text = st;
            yield return null;
        }
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
    }
}
