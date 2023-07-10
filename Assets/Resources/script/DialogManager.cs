using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject DialogBox;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject playerDialogIcon;
    [SerializeField] GameObject npcDialogIcon;
    [SerializeField] int lettersPerSecond;
    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    List<string> dialog;
    List<Sprite> avatar;
    int currentLine = 0;
    public bool isTyping = false;
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V) && !isTyping)
        {
            currentLine++;
            if (currentLine < dialog.Count)
            {
                StartCoroutine(TypeDialog(dialog[currentLine],avatar));
            }
            else
            {
                DialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                currentLine = 0;
            }
        }
    }
    public IEnumerator ShowDialog(List<string> dialog,List<Sprite> avatar)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        this.avatar= avatar;
        DialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog[0], avatar));
    }
    IEnumerator TypeDialog(string lines,List<Sprite> avatar)
    {
        isTyping = true;
        dialogText.text = "";
        string mark = lines.Split(":")[0];
        foreach (var sprite in avatar)
        {
            if (mark.Equals(sprite.name))
            {
                
                if ("Beastmaster".Equals(mark))
                {
                    playerDialogIcon.GetComponent<Image>().sprite = sprite;
                    npcDialogIcon.SetActive(false);
                    playerDialogIcon.SetActive(true) ;
                    
                }
                else
                {
                    npcDialogIcon.GetComponent<Image>().sprite = sprite;
                    npcDialogIcon.SetActive(true) ;
                    playerDialogIcon.SetActive(false) ;
                }
            }
            else
            {
                //playerDialogIcon.SetActive(false);
                npcDialogIcon.SetActive(false);
            }
        }
        if (mark.Equals("select"))
        {
            isTyping = false;
        } else
        {
            foreach (var letter in lines.Split(":")[1].ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(1.0f / lettersPerSecond);
            }
            isTyping = false;
        }
    } 
}
