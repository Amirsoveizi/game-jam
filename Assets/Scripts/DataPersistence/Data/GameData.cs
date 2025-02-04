using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataPersistence
{
    [Serializable]
    public class GameData
    {
        public string Name;
        public string Level;
        public List<SaveableData> datas;
    }

    [Serializable]
    public class SaveableData : ISaveable
    {
        [field:SerializeField] public float Id { get; set; }
        [field:SerializeField] public string Type { get; set; }
        public Vector3 position;
        public int health;
    }

    public interface ISaveable
    {
        float Id { get; set; }
        public string Type { get; set; }
    }

    public interface IBind<TData> where TData : ISaveable
    {
        float Id { get; set; }
        void Bind(TData data);
    }
}

