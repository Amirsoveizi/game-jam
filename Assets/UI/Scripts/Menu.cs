using UnityEngine;
using UnityEngine.Localization.Settings;

public class Menu : MonoBehaviour
{
    private GameObject menu;
    private GameObject credits;
    private GameObject controlls;
    private Utils utils;

    private void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
        credits = GameObject.FindGameObjectWithTag("Credits");
        controlls = GameObject.FindGameObjectWithTag("Controlls");
        utils = GetComponent<Utils>();
        credits.SetActive(false);
        controlls.SetActive(false);
    }
    public void OnCreditsClick()
    {
        credits.SetActive(true);
        menu.SetActive(false);
        utils.UpdateFont(LocalizationSettings.SelectedLocale);
    }
    public void OnCreditsExitClick()
    {
        credits.SetActive(false);
        controlls.SetActive(false);
        menu.SetActive(true);
    }

    public void OnControllsClick()
    {
        controlls.SetActive(true);
        menu.SetActive(false);
    }
}
