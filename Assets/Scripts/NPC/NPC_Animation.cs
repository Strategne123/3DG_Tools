using UnityEngine;


public class NPC_Animation : MonoBehaviour
{
    

    [SerializeField]
    public AnimationClip[] animation_mas = new AnimationClip[(int)Animations.crawl+1];//публичный список всех анимаций

    private void Start()
    {
        for(int i=0;i<animation_mas.Length;i++)
        {
            if (animation_mas[i]!=null)
            animation_mas[i].name = ((Animations)i).ToString();
        }
    }
}

public enum Animations
{
    stay_netural,//стоять
    stay_sit,//
    stay_sad,//
    sleep1,//лежать
    sleep2,//
    sit1,//сидеть
    sit2,//
    sit3,//
    sit4,//
    sit_stay,//
    crouch,//на присядках
    crouch_stay,//
    walk1,//ходьба
    walk2,//ходьба
    walk3,//ходьба
    run,//бег
    run_slow,//
    lrun,//спринт
    big_jump,//прыжок
    jump_gun, //
    jump_sprint,//
    sneak_run,//красться
    crawl//ползти !!!Остается последней анимацией, при добавлении новых следует вставлять их перед текущей анимацией
}