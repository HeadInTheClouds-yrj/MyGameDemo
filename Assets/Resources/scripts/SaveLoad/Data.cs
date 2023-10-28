using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    private string id;
    private string name;
    private long lingShi;
    private int maxAge;
    private int currentAge;
    private bool survival;
    private float maxLingQi;
    private float currentLingQi;
    private float regenerateLingQi;
    private float maxHealth;
    private float curenttHealth;
    private float moveSpeed;
    private int killEnimiesCont;
    private string[] installOrderGongFaIds;
    private SerializableDictionary<string,int> itemIds;
    private SerializableDictionary<string,int> instaillGongFas;
    private SerializableDictionary<string, int> learnedGongFas;
    private SerializableDictionary<string, int> learnedSkills;

    public Data(Transform transform)
    {
        id = transform.name;
        name = "";
        lingShi = 0;
        maxAge = 100;
        currentAge = 18;
        survival = true;
        maxLingQi = 1000;
        currentLingQi = 1000;
        regenerateLingQi = 1f;
        maxHealth = 100;
        curenttHealth = 100;
        moveSpeed = 2;
        killEnimiesCont = 0;
        installOrderGongFaIds = new string[9];
        itemIds = new SerializableDictionary<string, int>();
        this.instaillGongFas = new SerializableDictionary<string, int>();
        this.learnedGongFas = new SerializableDictionary<string, int>();
        this.learnedSkills = new SerializableDictionary<string, int>();
    }

    public string Name { get => name; set => name = value; }
    public float MaxLingQi { get => maxLingQi; set => maxLingQi = value; }
    public float CurrentLingQi { get => currentLingQi; set => currentLingQi = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurenttHealth { get => curenttHealth; set => curenttHealth = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int KillEnimiesCont { get => killEnimiesCont; set => killEnimiesCont = value; }
    public SerializableDictionary<string, int> InstaillGongFas { get => instaillGongFas; set => instaillGongFas = value; }
    public SerializableDictionary<string, int> LearnedGongFas { get => learnedGongFas; set => learnedGongFas = value; }
    public SerializableDictionary<string, int> LearnedSkills { get => learnedSkills; set => learnedSkills = value; }
    public bool Survival { get => survival; set => survival = value; }
    public string Id { get => id; set => id = value; }
    public SerializableDictionary<string, int> ItemIds { get => itemIds; set => itemIds = value; }
    public long LingShi { get => lingShi; set => lingShi = value; }
    public int MaxAge { get => maxAge; set => maxAge = value; }
    public int CurrentAge { get => currentAge; set => currentAge = value; }
    public float RegenerateLingQi { get => regenerateLingQi; set => regenerateLingQi = value; }
    public string[] InstallOrderGongFaIds { get => installOrderGongFaIds; set => installOrderGongFaIds = value; }
}
