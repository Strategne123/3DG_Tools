using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IItem
{
    weapon_type wtype;//тип оружия
    float wear_tear;//степень износа в % (где 0% - максимальный износ)

    

    public float wearTear //получить/задать степень износа оружия
    {
        get { return wear_tear; }
        set { if ((float)value > 0 && (float)value < 100) wear_tear = (float)value; else Debug.Log("Incorrect wear_tear: "+value); }
    }

    public void PickUp()
    {
        Weapon_Controller.AddWeapon(this.gameObject);
    }

    public enum weapon_type
    {
        fists,//кулаки
        bat,//бита
        knife,//нож
        cleaver,//тесак
        pistol,//пистолет
        submachine_gun,//автомат
        rocket_launcher,//ракетница
        grenade//граната
    }
}
