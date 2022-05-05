using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    public GameObject warior;
    public GameObject[] wariors=new GameObject[9];
    public GameObject robot;

    private void Start()
    {
        for (int i=0; i<3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                wariors[i] = Instantiate(warior);
                Vector3 temp = transform.position;
                temp.x += i;
                temp.z += j;
                wariors[i].transform.position = temp;
                wariors[i].transform.SetParent(transform);
                //Instantiate(robot).GetComponent<NPC_Controller>().staypos = wariors[i].transform;
            }

        }
        
    }

    void Update()
    {


    }
}
