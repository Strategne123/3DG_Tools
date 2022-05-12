using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private Camera cameraView;
    private int activeWeapon;
    private IItem[] components;
    private RaycastHit hit;

    private void Start()
    {
        cameraView = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        CheckItem();
        if(Input.GetButtonDown("Interract"))
        {
            Interract();
        }
    }

    private void CheckItem()
    {
        Ray ray = cameraView.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            components = hit.transform.gameObject.GetComponents<IItem>();
            if (components.Length > 0)
            {
                InfoUI.ShowItem(hit.transform.gameObject);
            }
            else
            {
                InfoUI.HideItem();
            }
        }
    }

    private void Interract()
    {
        if (components.Length > 0)
        {
            hit.transform.gameObject.GetComponent<IItem>().PickUp();
        }
    }
 
}
