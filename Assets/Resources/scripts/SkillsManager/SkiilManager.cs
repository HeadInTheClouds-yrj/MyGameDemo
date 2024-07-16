using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public enum State
{
    UI,
    BATTLE,
    DIALOG
}
public class SkiilManager : MonoBehaviour,IDataPersistence
{
    private State state = State.UI;
    private string FLY_SWORD = "FlySword";
    private string GLOBAL_LIGHTNING = "GlobalLightning";
    private string SAMPLE_PROTECT = "SampleProtect";
    private string RAY_ATTACK = "RayAttack";
    private string WINDY_CONTROL = "WindyControl";
    public static SkiilManager Instance;
    private Dictionary<string, SkillInfoSO> skillMap;
    private Dictionary<RectTransform, KeyCode> skillKeyBind;
    private Dictionary<SkillInfoSO, Action> allSkillActions;
    private KeyCode[] skillKeycode = new KeyCode[]{ KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.F,KeyCode.Mouse0,KeyCode.Mouse1,KeyCode.Mouse2,KeyCode.Alpha1, KeyCode.Alpha2 };
    private List<Action> staticSkillGradeAction;
    private List<GameObject> releaseFlySwordSkillData = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        skillMap = CreateSkillMap();
        staticSkillGradeAction = new List<Action>()
        {
            EventManager.Instance.skillEvent.ReleaseSkill_0,
            EventManager.Instance.skillEvent.ReleaseSkill_1,
            EventManager.Instance.skillEvent.ReleaseSkill_2,
            EventManager.Instance.skillEvent.ReleaseSkill_3,
            EventManager.Instance.skillEvent.ReleaseSkill_4,
            EventManager.Instance.skillEvent.ReleaseSkill_5,
            EventManager.Instance.skillEvent.ReleaseSkill_6,
            EventManager.Instance.skillEvent.ReleaseSkill_7,
            EventManager.Instance.skillEvent.ReleaseSkill_8,
            EventManager.Instance.skillEvent.ReleaseSkill_9
        };
        allSkillActions = InitAllSkillAction();
        RegisteSkillKeyBind(PropertyManuCtrl.instance.GetSkillRectTransforms());
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.Instance.gameStateEvent.OnChangeGameState += ChangeGameState;
    }

    private void ChangeGameState(State obj)
    {
        state= obj;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded-= OnSceneLoaded;
        EventManager.Instance.gameStateEvent.OnChangeGameState-= ChangeGameState;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        releaseFlySwordSkillData.Clear();
    }

    private void Update()
    {

        if (State.BATTLE == state&&skillKeyBind != null)
        {
            int i = 0;
            foreach (KeyCode item in skillKeyBind.Values)
            {

                if (Input.GetKeyDown(item))
                {
                    if (staticSkillGradeAction[i] != null)
                    {
                        staticSkillGradeAction[i]();
                    }
                    Debug.Log(item);
                }
                i++;
            }
        }  
    }
    private void Init()
    {
        //索引传递
        for (int i = 0; i < PlayerManager.instance.playerData.skillKey.Length; i++)
        {
            skillKeycode[i] = (KeyCode)PlayerManager.instance.playerData.skillKey[i];
        }
        UpdateSkillKeyBind(skillKeycode);
    }
    private Dictionary<SkillInfoSO,Action> InitAllSkillAction()
    {
        Dictionary<SkillInfoSO, Action> allSkillActions = new Dictionary<SkillInfoSO, Action>();
        allSkillActions.Add(GetSkillInfoSOById(FLY_SWORD), ReleaseSkill_FlySword);
        allSkillActions.Add(GetSkillInfoSOById(RAY_ATTACK), ReleaseSkill_RayAttack);
        allSkillActions.Add(GetSkillInfoSOById(GLOBAL_LIGHTNING), ReleaseSkill_GlobalLightning);
        return allSkillActions;
    }
    private Action GetRealesAction(string id)
    {
        SkillInfoSO temp = GetSkillInfoSOById(id);
        if (temp != null)
        {
            return allSkillActions[temp];
        }
        return null;
    }
    private Action GetRealesAction(SkillInfoSO info)
    {
        if (allSkillActions.ContainsKey(info))
        {
            return allSkillActions[info];
        }
        return null;
    }
    public void SkillBindGrade(int gradeIndex,SkillInfoSO info)
    {
        if (GetRealesAction(info) != null)
        {
            staticSkillGradeAction[gradeIndex] = GetRealesAction(info);
        }
    }
    public void SkillRemoveBindGrade(int gradeIndex, SkillInfoSO info)
    {
        if (GetRealesAction(info) != null && staticSkillGradeAction[gradeIndex] != null)
        {
            staticSkillGradeAction[gradeIndex] -= GetRealesAction(info);
        }
    }
    /// <summary>
    /// this is release flysword skill（释放飞剑法宝） action
    /// </summary>
    private void ReleaseSkill_FlySword()
    {
        for (int i = 0; i < 5; i++)
        {
            releaseFlySwordSkillData.Add(Instantiate(GetSkillInfoSOById(FLY_SWORD).skillEffectPrefab, PlayerManager.instance.transform.position, new Quaternion()));
        }
    }
    /// <summary>
    /// 全局闪电攻击技能未实现，未实现
    /// 思路：创建Battle.cs，里面包含玩家与敌人的关系集合池，按场景存
    /// 接着用协程给每一个敌人实例化一个闪电预制体，过两秒自动delete
    /// </summary>
    private void ReleaseSkill_GlobalLightning()
    {
        // NpcCell可以换一个通用的敌人接口，暂时懒得换了
        foreach (NpcCell item in NpcManager.instance.getAllNpcCell().Values)
        {
            GameObject gameObj = Instantiate(GetSkillInfoSOById(GLOBAL_LIGHTNING).skillEffectPrefab, item.transform);
            if (item != null && !item.IsDestroyed())
            {
                gameObj.GetComponent<ParticleSystem>().Play();
                item.NpcReduceHP(8);
            }
            Destroy(gameObj, 1f);
        }
    }
    /// <summary>
    /// 射线技能
    /// </summary>
    private void ReleaseSkill_RayAttack()
    {
        GameObject gameObject = Instantiate(GetSkillInfoSOById(RAY_ATTACK).skillEffectPrefab,new Vector3(0,0,0),new Quaternion(),Camera.main.transform);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<MagicRayVFX>().PlayerShoot();
    }
    public void RemoveFlySwordSkillDataByGObj(GameObject gObj)
    {
        releaseFlySwordSkillData.Remove(gObj);
    }
    private Dictionary<string, SkillInfoSO> CreateSkillMap()
    {
        SkillInfoSO[] skillInfoSOs = Resources.LoadAll<SkillInfoSO>("AllSkills");
        Dictionary<string,SkillInfoSO> keyValuePairs= new Dictionary<string,SkillInfoSO>();

        foreach (SkillInfoSO info in skillInfoSOs)
        {
            keyValuePairs.Add(info.id, info);
        }
        return keyValuePairs;
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
    public void RegisteSkillKeyBind(List<RectTransform> skillObj)
    {
        if (skillKeyBind == null)
        {
            skillKeyBind = new Dictionary<RectTransform, KeyCode>();
            for (int i = 0; i < skillObj.Count; i++)
            {
                skillKeyBind.Add(skillObj[i], skillKeycode[i]);
            }
        }
        else
        {
            skillKeyBind.Clear();
            for (int i = 0; i < skillObj.Count; i++)
            {
                skillKeyBind.Add(skillObj[i], skillKeycode[i]);
            }
        }
    }
    private void UpdateSkillKeyBind(KeyCode[] keycodes)
    {
        if (skillKeyBind != null)
        {
            List<RectTransform> skillObj = new List<RectTransform>();
            foreach (var item in skillKeyBind.Keys)
            {
                skillObj.Add(item);
                
            }
            for (int j = 0; j < skillKeyBind.Count; j++)
            {
                skillKeyBind[skillObj[j]] = keycodes[j];
            }
        }

    }
    public KeyCode GetSkillBindKeyByRectTransform(RectTransform rt)
    {
        if (skillKeyBind.ContainsKey(rt))
        {
            return skillKeyBind[rt];
        }
        return KeyCode.None;
    }
    public KeyCode GetSkillBindKeyByIndex(int i)
    {
        List<KeyCode> list = new List<KeyCode>();
        foreach (KeyCode item in skillKeyBind.Values)
        {
            list.Add(item);
        }
        if (i>=0 && i< list.Count)
        {
            return list[i];

        }
        return KeyCode.None;
    }
    public bool ChangeSkillKeyBind(RectTransform rt, KeyCode keycode)
    {
        if (!skillKeyBind.ContainsKey(rt))
        {
            return false;
        }
        if (skillKeyBind.ContainsValue(keycode))
        {
            return false;
        }
        int index = 0,i = 0;
        foreach (RectTransform item in skillKeyBind.Keys)
        {
            if (item == rt)
            {
                index = i;
            }
            i++;
        }
        skillKeyBind[rt] = keycode;
        skillKeycode[index] = keycode;
        PlayerManager.instance.playerData.skillKey[index] = (int)keycode;
        return true;
    }
    public bool ChangeSkillKeyBind(int index, KeyCode keycode)
    {
        if (index < 0 || index > skillKeyBind.Count)
        {
            return false;
        }
        if (skillKeyBind.ContainsValue(keycode))
        {
            return false;
        }
        List<RectTransform> list = new List<RectTransform>();
        foreach (RectTransform item in skillKeyBind.Keys)
        {
            list.Add(item);
        }
        skillKeycode[index] = keycode;
        skillKeyBind[list[index]] = keycode;
        PlayerManager.instance.playerData.skillKey[index] = (int)keycode;
        return true;
    }
    public void SwordInstantiate(Transform parent)
    {
        //GameObject sword = Object.Instantiate<GameObject>(flySword,parent.position,Quaternion.Euler(new Vector3(0,0,180)));
        //if (sword.transform.TryGetComponent<TrackingSword>(out TrackingSword trackingSword))
        //{
        //    //trackingSword.InitializedSword(transform,NpcManager.Instance.GetNpcs()[Random.Range(0,2)].transform,LayerMask.GetMask("Npc"),100f,4000f);
        //}
    }

    public void LoadGame(GameData gameData)
    {
        Init();
        for (int i = 0; i < gameData.datas[0].installOrderSkillIds.Length; i++)
        {
            if (!"empty".Equals(gameData.datas[0].installOrderSkillIds[i]))
            {
                SkillBindGrade(i, GetSkillInfoSOById(gameData.datas[0].installOrderSkillIds[i]));
            }
        }
    }

    public void SaveGame(GameData gameData)
    {
        for (int i = 0; i < skillKeycode.Length; i++)
        {
            PlayerManager.instance.playerData.skillKey[i] = (int)skillKeycode[i];
        }
    }
}
