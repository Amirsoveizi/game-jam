using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    [SerializeField] public TMP_FontAsset fontEn;
    [SerializeField] public TMP_FontAsset fontFa;
    private bool isSetLanguageActive = false;

    private void Awake()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        UpdateFont(LocalizationSettings.SelectedLocale);
    }
    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }

    private void OnLocaleChanged(Locale locale)
    {
        UpdateFont(locale);
    }

    public void UpdateFont(Locale locale)
    {
        TextMeshProUGUI[] textMeshProComponents = FindObjectsOfType<TextMeshProUGUI>();

        if (locale.Identifier.Code == "en")
        {
            foreach (TextMeshProUGUI textComponent in textMeshProComponents)
            {
                if (textComponent.tag == "Text")
                {
                    textComponent.font = fontEn;
                }
            }
        }
        else
        {
            foreach (TextMeshProUGUI textComponent in textMeshProComponents)
            {
                if (textComponent.tag == "Text")
                {
                    textComponent.font = fontFa;
                }
            }
        }
    }

    public void OnChangeLanguage()
    {
        if (isSetLanguageActive) return;
        StartCoroutine(SetLanguage());
        isSetLanguageActive = false;
    }

    IEnumerator SetLanguage()
    {
        isSetLanguageActive = true;
        yield return LocalizationSettings.InitializationOperation;
        string language = LocalizationSettings.SelectedLocale.name;
        int id;
        if (language[0] == 'E')
        {
            id = 1;
        }
        else
        {
            id = 0;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
