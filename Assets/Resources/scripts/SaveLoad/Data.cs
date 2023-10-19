using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private string Name;
    private float maxLingQi;
    private float currentLingQi;
    private float maxHealth;
    private float curenttHealth;
    private float moveSpeed;
    private int killEnimiesCont;
    private SerializableDictionary<string,int> instaillGongFa;
    private SerializableDictionary<string, int> learnedGongFa;
    private SerializableDictionary<string, int> learnedSkill;
    private List<GongFa> currentUsingGongFa;
    public Data()
    {
        Name = "";
        maxLingQi = 1000;
        currentLingQi = 1000;
        maxHealth = 100;
        curenttHealth = 100;
        moveSpeed = 2;
        killEnimiesCont = 0;
        this.instaillGongFa = new SerializableDictionary<string, int>();
        this.learnedGongFa = new SerializableDictionary<string, int>();
        this.learnedSkill = new SerializableDictionary<string, int>();
        this.currentUsingGongFa = new List<GongFa>();
    }
    public Data(Transform transform)
    {
        Name = transform.name;
        maxLingQi = 1000;
        currentLingQi = 1000;
        maxHealth = 100;
        curenttHealth = 100;
        moveSpeed = 2;
        killEnimiesCont = 0;
        this.instaillGongFa = new SerializableDictionary<string, int>();
        this.learnedGongFa = new SerializableDictionary<string, int>();
        this.learnedSkill = new SerializableDictionary<string, int>();
        this.currentUsingGongFa = new List<GongFa>();
    }

    public string Name1 { get => Name; set => Name = value; }
    public float MaxLingQi { get => maxLingQi; set => maxLingQi = value; }
    public float CurrentLingQi { get => currentLingQi; set => currentLingQi = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurenttHealth { get => curenttHealth; set => curenttHealth = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int KillEnimiesCont { get => killEnimiesCont; set => killEnimiesCont = value; }
    public SerializableDictionary<string, int> InstaillGongFa { get => instaillGongFa; set => instaillGongFa = value; }
    public SerializableDictionary<string, int> LearnedGongFa { get => learnedGongFa; set => learnedGongFa = value; }
    public SerializableDictionary<string, int> LearnedSkill { get => learnedSkill; set => learnedSkill = value; }
    public List<GongFa> CurrentUsingGongFa { get => currentUsingGongFa; set => currentUsingGongFa = value; }
}
