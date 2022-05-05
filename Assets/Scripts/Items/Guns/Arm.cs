using System;
using System.Collections;
using UnityEngine;

public class Arm : RangedWeapon
{
    
    //public GameObject camera;

    void Awake()
    {
       /* camera = GameObject.Find("Main Camera");
        if (GetComponent<Camera>() == null)
            throw new NullReferenceException("Не нашелся объект Main Camera");*/
    }

    void Update()
    {
       // Debug.DrawRay(camera.GetComponent<Camera>().transform.position, camera.GetComponent<Camera>().transform.forward * shoot_range);
    }

    public virtual void Shoot()//стрельба
    {
        if (can_shoot)
        {
            Shoot();
        }
    }

    private IEnumerator ShootStamina()
    {
        can_shoot = false;
        yield return new WaitForSeconds(shooting_speed);
        can_shoot = true;
    }

    public void Shot()//конкретный выстрел
    {
        RaycastHit hit;
        if (Physics.Raycast(GetComponentInParent<Camera>().transform.position, GetComponentInParent<Camera>().transform.forward, out hit, shoot_range))
        {
            if (hit.collider.gameObject.tag == "robot")
            {
                //print("Нанесли урон противнику");
            }
            else
            {
                //print("Попали не туда");
            }
        }
        StartCoroutine(ShootStamina());
    }
}
