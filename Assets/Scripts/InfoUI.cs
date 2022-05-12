using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    private static InfoUI self;
    [SerializeField] private TextMeshProUGUI itemText;

    private void Start()
    {
        if (self == null)
        {
            self = this;
        }
    }

    public static void ShowItem(GameObject item)
    {
        if (!self.itemText.gameObject.activeInHierarchy)
        {
            self.itemText.gameObject.SetActive(true);
            self.itemText.text = self.itemText.text + Input.GetAxis("Interract");
        }
    }

    public static void HideItem()
    {
        if (self.itemText.gameObject.activeInHierarchy)
        {
            self.itemText.gameObject.SetActive(false);
        }
    }


}
