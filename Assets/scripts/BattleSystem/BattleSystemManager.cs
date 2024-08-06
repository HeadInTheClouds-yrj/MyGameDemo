using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystemManager : MonoBehaviour
{
    public static BattleSystemManager Instance;
    private BattleState state;
    private Dictionary<string, Transform> allHumanoids;
    private Dictionary<string, Transform> neutral;
    private Dictionary<string, Transform> xingHengZong;
    private Dictionary<string, Transform> zonMen_B;
    private Dictionary<string, Transform> zonMen_C;
    private Dictionary<string, Transform> zonMen_D;
    private Dictionary<string, Transform> zonMen_E;
    private Transform npcParent;
    private void Initialize()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        allHumanoids = new Dictionary<string, Transform>();
        neutral= new Dictionary<string, Transform>();
        xingHengZong= new Dictionary<string, Transform>();
        zonMen_B= new Dictionary<string, Transform>();
        zonMen_C= new Dictionary<string, Transform>();
        zonMen_D= new Dictionary<string, Transform>();
        zonMen_E= new Dictionary<string, Transform>();
    }
    private void SetAllHumanoids()
    {
        allHumanoids.Clear();
        foreach (Transform item in npcParent)
        {
            if (item.TryGetComponent<Humanoid>(out Humanoid humanoid))
            {
                allHumanoids.Add(item.name,item);
            }
        }
    }
    public Transform FindHumanoidByName(string name)
    {
        if (allHumanoids.ContainsKey(name))
        {
            return allHumanoids[name];
        }
        else
        {
            Debug.LogWarning("根据名字寻找本场景npc不存在 错误的参数name："+name);
            return null;
        }
    }
    public void SetZongMenHuanoid()
    {
        xingHengZong.Clear();
        foreach (Transform item in allHumanoids.Values)
        {
            if (item.TryGetComponent<XingHeng>(out XingHeng xingHeng))
            {
                xingHengZong.Add(item.name,item);
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    private void Start()
    {
        
    }
    private void HandlerUpdate()
    {
        
    }
    private void HandlerFixedUpdate()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
