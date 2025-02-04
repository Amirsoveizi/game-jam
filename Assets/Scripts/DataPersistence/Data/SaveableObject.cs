using DataPersistence;
using UnityEngine;

public class SaveableObject : MonoBehaviour,IBind<SaveableData>
{
    [field:SerializeField] public float Id { get; set; }
    private Health health;

    public void Bind(SaveableData data)
    {
        if(health != null)
        {
            health.Bind(data);
        }
    }

    public SaveableData GetData()
    {
        return health.GetData();
    }

    private void Awake()
    {
        health = gameObject.GetComponent<Health>();
    }

    private void Start()
    {
        GetData();
    }
}
