using TMPro;
using UnityEngine;

public class LocalizedName : MonoBehaviour
{
    [SerializeField] private string key;

    private LocalizationManager localizationManager;
    private string objName;

    void Awake()
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
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
        objName = localizationManager.GetLocalizedValue(key);
    }

    public string GetName()
    {
        return objName;
    }
}