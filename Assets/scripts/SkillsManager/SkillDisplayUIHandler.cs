using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplayUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    private Dictionary<string, GameObject> skillUIs;
    private void InstantiateSkillUI(string id)
    {
        GameObject gameObject = Instantiate(skillPrefab,content);
        if (skillUIs != null )
        {
            skillUIs = new Dictionary<string, GameObject>();
        }
        SkillInfoSO skillInfoSO = SkiilManager.Instance.GetSkillInfoSOById(id);
    }
}
