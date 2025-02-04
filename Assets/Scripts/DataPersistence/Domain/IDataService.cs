using System.Collections.Generic;

namespace DataPersistence
{
    public interface IDataService
    {
        void Save(GameData data);
        GameData Load(string name);
        void DeleteAll();
        IEnumerable<string> GetAllSaves();
    }
}
