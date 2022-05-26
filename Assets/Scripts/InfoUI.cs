using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    private static InfoUI self;

    [SerializeField] private TextMeshProUGUI interractText;
    [SerializeField] private TextMeshProUGUI itemInfoText;
    [SerializeField] private Slider stamina;
    [SerializeField] private Slider fatigue;

    private Movement movement;

    private void Start()
    {
        if (self == null)
        {
            self = this;
        }
        movement = GetComponent<Movement>();
        stamina.maxValue = movement.refStamina;
        fatigue.maxValue = movement.refStamina;
    }

    private void Update()
    {
        var staminaActive = movement.stamina < movement.fatigue ? true : false;
        stamina.gameObject.SetActive(staminaActive);
        stamina.value = movement.stamina;
        fatigue.value = movement.fatigue;
    }

    public static void ShowItem(GameObject item)
    {
        if (!self.itemInfoText.gameObject.activeInHierarchy)
        {
            self.itemInfoText.gameObject.SetActive(true);
            self.itemInfoText.text = item.GetComponent<LocalizedName>().GetName() + " " + self.interractText.text;
        }
    }

    public static void HideItem()
    {
        if (self.itemInfoText.gameObject.activeInHierarchy)
        {
            self.itemInfoText.gameObject.SetActive(false);
        }
    }


}
