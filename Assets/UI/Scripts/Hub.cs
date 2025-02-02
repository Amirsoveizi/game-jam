using UnityEngine;


public class Hub : MonoBehaviour
{
    private static GameObject hud;
    private static GameObject pause;

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
