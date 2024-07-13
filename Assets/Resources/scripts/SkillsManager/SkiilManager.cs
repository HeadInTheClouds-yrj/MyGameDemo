using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiilManager : MonoBehaviour
{
    public static SkiilManager Instance;
    private Dictionary<string, SkillInfoSO> skillMap;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        skillMap = CreateSkillMap();
    }
    private void OnEnable()
    {
        //EventManager.Instance.InputEvent.OnGetLeftMouseDown += SwordInstantiate;
    }
    private void OnDisable()
    {
        //EventManager.Instance.InputEvent.OnGetLeftMouseDown -= SwordInstantiate;
    }
    private Dictionary<string, SkillInfoSO> CreateSkillMap()
    {
        SkillInfoSO[] skillInfoSOs = Resources.LoadAll("AllSkills") as SkillInfoSO[];
        skillMap = new Dictionary<string, SkillInfoSO>();
        foreach (SkillInfoSO info in skillInfoSOs)
        {
            if (!skillMap.ContainsKey(info.id))
            {
                skillMap.Add(info.id,info);
            }
        }
        return skillMap;
    }
    public SkillInfoSO GetSkillInfoSOById(string id)
    {
        if (skillMap.ContainsKey(id))
        {
            return skillMap[id];
        }
        else
        {
            Debug.LogWarning("根据id找术法找不到:id == "+id);
            return null;
        }
    }
    public void SwordInstantiate(Transform parent)
    {
        //GameObject sword = Object.Instantiate<GameObject>(flySword,parent.position,Quaternion.Euler(new Vector3(0,0,180)));
        //if (sword.transform.TryGetComponent<TrackingSword>(out TrackingSword trackingSword))
        //{
        //    //trackingSword.InitializedSword(transform,NpcManager.Instance.GetNpcs()[Random.Range(0,2)].transform,LayerMask.GetMask("Npc"),100f,4000f);
        //}
    }
}
