using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject UIManager;
    void Awake()
    {
        GameObject s1 = Instantiate(UI);
        GameObject s2 = Instantiate(UIManager);
        s2.name = "UIM";
    }

}
