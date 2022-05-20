using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    private static InfoUI self;

    [SerializeField] private TextMeshProUGUI interractText;
    [SerializeField] private TextMeshProUGUI itemInfoText;

    private void Start()
    {
        if (self == null)
        {
            self = this;
        }
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
