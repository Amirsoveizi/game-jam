using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIManager;
    private Hub hub;
    private bool isPaused = false;
    void Awake()
    {
        GameObject s1 = Instantiate(UI);
        GameObject s2 = Instantiate(UIManager);
        s2.name = "UIM";
    }

    private void Start()
    {
        hub = FindObjectOfType<Hub>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) 
            {
                hub.GoToPause();
            }
            else
            {
                hub.BackToGame();
            }
            isPaused = !isPaused;
        }
    }

}
