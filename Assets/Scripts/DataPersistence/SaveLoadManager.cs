using DataPersistence;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public IDataService dataService;
    public static GameData gameData;

    public GameObject player;
    public GameObject comrade;
    public GameObject tank;
    public GameObject enemy;

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(gameData == null)
        {
            NewGame();
        }
        SaveGame();
    }

    private void GetSaveableDatas()
    {
        var objs = FindObjectsOfType<SaveableObject>();
        gameData.datas = objs.Select(obj => obj.GetData()).ToList();
    }


    public void NewGame()
    {
        gameData = new GameData { Name = "Game", Level = "Menu" };
        //SceneManager.LoadScene(gameData.Level);
    }

    public void SaveGame(string saveName = "Game")
    {
        gameData.Name = saveName;
        gameData.Level = SceneManager.GetActiveScene().name;
        GetSaveableDatas();
    }

    public void LoadScene(string gameName)
    {
        gameData = dataService.Load(gameName);
        SceneManager.LoadScene(gameData.Level);
    }

    public void Respawn()
    {
        Bind<SaveableObject, SaveableData>(gameData.datas);
    }

    public void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
    {
        var entities = FindObjectsOfType<T>();


        foreach (var data in datas)
        {
            var obj = entities.FirstOrDefault(d => d.Id == data.Id) as GameObject;

            //if (obj == null)
            //{
            //    switch (data.Type)
            //    {
            //        case "Player":
            //            obj = Instantiate(player);
            //            break;
            //        case "Comrade":
            //            obj = Instantiate(comrade);
            //            break;
            //        case "Tank":
            //            obj = Instantiate(tank);
            //            break;
            //        case "Enemy":
            //            obj = Instantiate(enemy);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            if(obj != null)
            {
                obj.GetComponent<T>().Bind(data);

            }



        }
    }
}
