using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataPersistence
{
    public class DataService : IDataService
    {
        private ISerializer _serializer;
        private string dataPath;
        private const string ext = "json";

        public DataService(ISerializer serializer) 
        {
            _serializer = serializer;
            dataPath = Application.persistentDataPath;
        }

        private string GetPathToFile(string fileName)
        {
            return Path.Combine(dataPath,string.Concat(fileName,".",ext));
        }

        public void Save(GameData data)
        {
            string filePath = GetPathToFile(data.Name);
            File.WriteAllText(filePath, _serializer.Serialize(data));
        }
        public GameData Load(string name)
        {
            string filePath = GetPathToFile(name);

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("game data not found !!!");
            }

            return _serializer.Deserialize<GameData>(File.ReadAllText(filePath));

        }
        public IEnumerable<string> GetAllSaves()
        {
            foreach (string file in Directory.GetFiles(dataPath))
            {
                if (Path.GetExtension(file) == ext)
                {
                    yield return Path.GetFileNameWithoutExtension(file);
                }
            }
        }
        public void DeleteAll()
        {
            foreach (string file in Directory.GetFiles(dataPath))
            {
                if (Path.GetExtension(file) == ext)
                {
                    File.Delete(file);
                }
            }
        }
    }
}