using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private Camera camera;
    private int activeWeapon;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interract();
        }
    }

    private void Interract()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            var components = hit.transform.gameObject.GetComponents<IItem>();
            if (components.Length > 0)
            {
                hit.transform.gameObject.GetComponent<IItem>().PickUp();
            }
        }
    }
 
}
