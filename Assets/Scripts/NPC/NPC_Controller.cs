using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPC_Controller : MonoBehaviour
{
    private State state;                //состояние npc 
    private GameObject slave;           //ведомый объект
    private Emotions emotion;           //эмоция npc
    private Transform staypos;          //точка остановки
    private NavMeshAgent agent;         //агент перемещений
    private GameObject haunted;         //преследуемый объект
    private int destination = 0;        //текущая точка патрулирования

    [SerializeField] private float min_near = 1f;               //минимальное расстояние, при котором точка считается достигнутой
    [SerializeField] private float leadDistance =5f;            //максимальная дистанция между ведущим и ведомым
    [SerializeField] private List<Transform> patrolPoints;           //точки патрулирования
    [SerializeField] private float approximationRatio = 0.1f;   //коэффициент, увеличивающий дальность, при которой точка считается достигнутой


    private void InitVariables()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints = new List<Transform>();
    }

    void Start()
    {
        InitVariables();
        emotion = Emotions.ordinary;  
    }

    void SetEmotion(Emotions e)
    {
        emotion = e;
        if((int)e<2)
        {
            agent.speed = 0;
        }
        else if ((int)e == 2)
        {
            agent.speed = 3;
        }
        else if ((int)e < 5)
        {
            agent.speed = 5;
        }
        else
        {
            agent.speed = 7;
        }
    }

    bool NearPoint(Vector3 point)//достиг ли объект точки
    {
        if (Vector3.Distance(transform.position, point) < min_near+approximationRatio)
        {
            return true;
        }
        else
            return false;
    }

    void Stay()//стоять
    {
        agent.SetDestination(staypos.position);
    }

    void Patrol()//патрулирование по точкам
    {
        if (agent.velocity.magnitude == 0)//если npc не может достичь точки, то он на нее забивает
        {
            min_near += approximationRatio;
        }
        else
        {
            approximationRatio = 0.1f;
        }
        agent.SetDestination(patrolPoints[destination].position);
        if (NearPoint(patrolPoints[destination].position))
        {
            destination++;
        }
        if (destination >= patrolPoints.Count)
        {
            destination = 0;
        }
    }

    void Haunt()//преследование
    {
        agent.SetDestination(haunted.transform.position);
    }

    void Lead()//вести кого-либо
    {
        if (Vector3.Distance(transform.position, slave.transform.position) < leadDistance)
        {
            if (agent.velocity.magnitude == 0)          //если npc не может достичь точки, то он на нее забивает
            {
                approximationRatio *= approximationRatio;
            }
            else
            {
                approximationRatio = 0.1f;
            }
            agent.SetDestination(patrolPoints[destination].position);
            if (NearPoint(patrolPoints[destination].position))
            {
                destination++;
            }
            if (destination >= patrolPoints.Count)
            {
                destination = 0;
            }   
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }



    void Update()
    {
        switch(state)
        {
            case State.stay:
                Stay();
                break;
            case State.patrol:
                Patrol();
                break;
            case State.haunt:
                Haunt();
                break;
            case State.lead:
                Lead();
                break;
        }  
    }
}

public enum State
{
    stay,//стоит
    patrol,//патрулирует
    haunt,//преследует
    lead,//ведет
    atack,//атакует
    wander//бродит
}

public enum Emotions
{
    sleep,//спящий
    death,//мертвый
    depressed,//подавленный
    ordinary,//обычное
    elevated,//приподнятое
    joyful,//радостное
    agony,//агония
    
}


