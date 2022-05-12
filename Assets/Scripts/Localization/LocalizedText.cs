using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private LocalizationManager localizationManager;
    private TextMeshProUGUI text;

    void Awake()
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        localizationManager.OnLanguageChanged += UpdateText;
    }

    void Start()
    {
        UpdateText();
    }

    private void OnDestroy()
    {
        localizationManager.OnLanguageChanged -= UpdateText;
    }

    virtual protected void UpdateText()
    {
        if (gameObject == null) return;

        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = localizationManager.GetLocalizedValue(key);
    }
}