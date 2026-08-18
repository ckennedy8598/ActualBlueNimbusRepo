using UnityEngine;

public class NEW_SpawnPoint : MonoBehaviour
{
    [Header("Spawn Point ID")]
    [SerializeField] private string spawnID;

    public string SpawnID
    {
        get
        {
            return spawnID;
        }
    }
}