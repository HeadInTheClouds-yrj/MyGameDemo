using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject DialogBox;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] GameObject playerDialogIcon;
    [SerializeField] GameObject npcDialogIcon;
    [SerializeField] int lettersPerSecond;
    [SerializeField] GameObject chooseButton;
    [SerializeField] GameObject chooseBox;
    public Interactives dialogingCharacter;
    [SerializeField] GameObject contentUI;
    private float showDialogWaitTime = 0.1f;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
    }
    List<string> dialog;
    List<Sprite> avatar;
    int currentLine = 0;
    public bool isTyping = false;
    public bool isKeyDown = false;
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isKeyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Skip();
        }
        if (isKeyDown && !isTyping)
        {
            isKeyDown = false;
            currentLine++;
            if (currentLine < dialog.Count)
            {
                StartCoroutine(TypeDialog(dialogingCharacter, dialog[currentLine],avatar));
            }
            else
            {
                DialogBox.SetActive(false);
                EventManager.Instance.dialogEvent.HideDailog();
                currentLine = 0;
            }
        }
    }
    public IEnumerator ShowDialog(Interactives character, List<string> dialog,List<Sprite> avatar)
    {
        yield return new WaitForEndOfFrame();
        EventManager.Instance.dialogEvent.ShowDailog();
        this.dialog = dialog;
        this.avatar= avatar;
        this.dialogingCharacter= character;
        DialogBox.SetActive(true);
        StartCoroutine(TypeDialog(character,dialog[0], avatar));
    }
    IEnumerator TypeDialog(Interactives character, string lines,List<Sprite> avatar)
    {
        isTyping = true;
        dialogText.text = "";
        string mark = lines.Split(":")[0];
        DailogSetCharacterLogic(mark,avatar);

        //============================================

        if (mark.Equals("choose"))
        {
            string[] allChooseDialog = lines.Split(":")[1].Split("|");
            chooseBox.SetActive(true);
            ShowChooseButton(allChooseDialog);
        } 
        else
        {
            foreach (var letter in lines.Split(":")[1].ToCharArray())
            {
                dialogText.text += letter;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isTyping = true;
                    Skip();
                    yield break;
                }
                yield return new WaitForSeconds(showDialogWaitTime / lettersPerSecond);
            }
            isTyping = false;
        }
    }
    private void Skip()
    {
        isKeyDown = false;
        currentLine++;
        for (int i = currentLine; i < dialog.Count; i++)
        {
            if (currentLine < dialog.Count)
            {
                SkipDialog(dialogingCharacter, dialog[currentLine], avatar);
            }
        }
        isTyping = false;
        DialogBox.SetActive(false);
        EventManager.Instance.dialogEvent.HideDailog();
        currentLine = 0;

    }
    private void SkipDialog(Interactives character, string lines, List<Sprite> avatar)
    {
        isTyping = true;
        dialogText.text = "";
        string mark = lines.Split(":")[0];
        DailogSetCharacterLogic(mark, avatar);

        //============================================

        if (mark.Equals("choose"))
        {
            string[] allChooseDialog = lines.Split(":")[1].Split("|");
            chooseBox.SetActive(true);
            ShowChooseButton(allChooseDialog);
        }

    }
    private void DailogSetCharacterLogic(string characterName, List<Sprite> avatar)
    {
        foreach (var sprite in avatar)
        {
            if (characterName.Equals(sprite.name))
            {

                if ("Beastmaster".Equals(characterName))
                {
                    playerDialogIcon.GetComponent<Image>().sprite = sprite;
                    npcDialogIcon.SetActive(false);
                    playerDialogIcon.SetActive(true);

                }
                else
                {
                    npcDialogIcon.GetComponent<Image>().sprite = sprite;
                    npcDialogIcon.SetActive(true);
                    playerDialogIcon.SetActive(false);
                }
            }
            else
            {
                //playerDialogIcon.SetActive(false);
                npcDialogIcon.SetActive(false);
            }
        }
    }
    private void ShowChooseButton(string[] allChooseDialog)
    {
        int i = 0;
        foreach (string choose in allChooseDialog)
        {
            GameObject obj = Instantiate(chooseButton,contentUI.transform);
            TMP_Text tMP_Text = obj.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            tMP_Text.text = choose;
            UnityAction action = () =>
            {
                dialogingCharacter?.AddEventId(i);
                Transform[] transforms = contentUI.GetComponentsInChildren<Transform>();
                foreach (Transform transform1 in transforms)
                {

                    if (transform1.name.Equals(contentUI.name))
                    {
                        Debug.Log(transform1.name);
                    }
                    else
                    {
                        Destroy(transform1.gameObject);
                    }
                }
                chooseBox.SetActive(false);
                isKeyDown = true;
                isTyping = false;
                
                
            };
            obj.GetComponent<Button>().onClick.AddListener(action);
            i++;
        }
    }
}
