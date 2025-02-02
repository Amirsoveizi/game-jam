using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class Hub : MonoBehaviour
{
    private GameObject hud;
    private GameObject pause;

    private void Awake()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        pause =  GameObject.FindGameObjectWithTag("Pause");
        BackToGame();
    }
    public void BackToGame()
    {
        hud.SetActive(true);
        pause.SetActive(false);
        UnPauseGame();
    }
    public void GoToPause()
    {
        Debug.Log("Pressed");
        hud.SetActive(false);
        pause.SetActive(true);
        PauseGame();
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
