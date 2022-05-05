using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    public bool can_shoot;//возможность выстрелить
    public int clip; //число патронов в обойме
    public float recharge_rate;//скорость перезарядки
    public float shooting_speed;//скорость стрельбы
    public float damage;//урон от обычного выстрела
    public float special_shooting_speed;//скорость спец стрельбы
    public float special_damage;//урон от спец. выстрела
    public float shoot_range;//урон от спец. выстрела
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
