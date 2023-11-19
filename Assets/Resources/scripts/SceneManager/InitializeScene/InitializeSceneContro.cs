using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitializeSceneContro : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private TMP_Text loadingNumber;
    [SerializeField] private Transform Animater;
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
        float num2 = 0;
        string st = "";
        while (num<=100)
        {
            num += Time.deltaTime * 50;
            num2+= Time.deltaTime * 200;
            num2 %= 360;
            st = (int)num+"%";
            Animater.rotation = Quaternion.Euler(new Vector3(0,0,num2));
            loadingNumber.text = st;
            yield return null;
        }
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
    }
}
