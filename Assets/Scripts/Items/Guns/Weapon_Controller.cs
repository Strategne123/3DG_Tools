using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    private static int activeWeapon;
    [SerializeField] Transform hand;
    private static Weapon_Controller self;
    public List<GameObject> weapons=new List<GameObject>();
    [SerializeField] private int maxWeaponCount = 2;
    
    private void Start()
    {
        if(!self)
        {
            self = this;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && self.weapons.Count>0)
        {
            self.weapons[activeWeapon].GetComponent<IWeapon>().Shoot();
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Swap(Input.GetAxis("Mouse ScrollWheel"));
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Throw();
        }
    }

    public static void AddWeapon(GameObject weapon)
    {
        if (self.weapons.Count < self.maxWeaponCount)
        {
            self.weapons.Add(weapon);
            Swap(1);
        }
        else
        {
            self.FromHand(self.weapons[activeWeapon]);
            self.weapons[activeWeapon] = weapon;
        }
        self.ToHand(weapon);
    }

    private void ToHand(GameObject weapon)
    {
        //weapon.GetComponent<Rigidbody>().useGravity = false;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.GetComponent<Collider>().isTrigger = true;
        weapon.transform.position = hand.position;
        weapon.transform.rotation = hand.rotation;
        weapon.transform.parent = hand;
    }

    private void FromHand(GameObject weapon)
    {
        //weapon.GetComponent<Rigidbody>().useGravity = true;
        weapon.GetComponent<Collider>().isTrigger = false;
        weapon.GetComponent<Rigidbody>().isKinematic = false;
        weapon.transform.parent = null;
    }

    private static void Throw()
    {
        if (self.weapons.Count > 0)
        {
            var deleteWeapon = self.weapons[activeWeapon];
            self.FromHand(deleteWeapon);
            Swap(-1);
            self.weapons.Remove(deleteWeapon);
            deleteWeapon.SetActive(true);
        }
    }

    private static void Swap(float n)
    {
        if(self.weapons.Count>0)
        {
            self.weapons[activeWeapon].SetActive(false);
            if (n > 0)//если крутим вперед
            {
                if (activeWeapon >= self.weapons.Count - 1)//если дошли до конца прокрутки
                {
                    activeWeapon = 0;
                }
                else
                {
                    activeWeapon++;
                }
                self.weapons[activeWeapon].SetActive(true);
            }
            if (n < 0)//если крутим назад
            {
                if (activeWeapon <= 0)//если дошли до конца прокрутки
                {
                    activeWeapon = self.weapons.Count - 1;
                }
                else
                {
                    activeWeapon--;
                }
                self.weapons[activeWeapon].SetActive(true);
            }
        } 
    }
}
