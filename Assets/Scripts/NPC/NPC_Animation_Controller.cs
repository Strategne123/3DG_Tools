using UnityEngine;
using UnityEngine.AI;


public class NPC_Animation_Controller : MonoBehaviour
{
    Animator animator;
    NPC_Controller npc;
    NavMeshAgent agent;
    float animtime;
    Animations anistate;

    public Animations Ani_State//вернуть или задать анимацию npc
    {
        get { return (Animations)anistate; }
        set { anistate = value; }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        npc = GetComponent<NPC_Controller>();
        Ani_State = Animations.stay_netural;
        animtime = Time.time;
        
    }

    void TryAnim(Animations a)
    {
        if (a!=Ani_State)
        {
            if (Time.time - animtime > 0.5f)
            {
                animator.CrossFade(((Animations)a).ToString(), 0.5f);
                Ani_State = a;
                animtime = Time.time;
                print(animtime);
            }
        }
    }

    void Run()
    {
        if(agent.velocity.magnitude<3.5)
        {
            TryAnim(Animations.run_slow);
        }
        else if (agent.velocity.magnitude < 5.5)
        {
            TryAnim(Animations.run);
        }
        else
        {
            TryAnim(Animations.lrun);
        }
    }

    private void Update()
    {
       Run();
        

    }
}
    
