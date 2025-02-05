using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBossDie : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var utils = FindObjectOfType<Utils>();
            utils.LoadSceneAsync("Hub");
        }
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public static T Find<T>() where T : Component
    {
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            T component = FindInChildren<T>(root.transform);
            if (component != null)
            {
                component.gameObject.SetActive(true); // Activate the GameObject
                Debug.Log($"{component.gameObject.name} is now active.");
                return component;
            }
        }
        return null;
    }

    private static T FindInChildren<T>(Transform parent) where T : Component
    {
        T component = parent.GetComponent<T>();
        if (component != null)
        {
            return component;
        }

        foreach (Transform child in parent)
        {
            component = FindInChildren<T>(child);
            if (component != null)
            {
                return component;
            }
        }

        return null;
    }
}
