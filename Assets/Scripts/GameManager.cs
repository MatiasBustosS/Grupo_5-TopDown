using System.Collections.Generic;
using UnityEngine;
using static Enums;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Scenes currentScene;
    public int lastDoorID;
    
    public Dictionary<Scenes, Vector3> savedPositions = new Dictionary<Scenes, Vector3>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SavePlayerPosition(Vector3 position)
    {
        savedPositions[currentScene] = position;
    }

    public bool TryGetSavedPosition(Scenes map, out Vector3 position)
    {
        return savedPositions.TryGetValue(map, out position);
    }

    public void ResetPlayerPosition()
    {
        var keys = new List<Scenes>(savedPositions.Keys);

        foreach (Scenes scene in keys)
        {
            savedPositions[scene] = Vector3.zero;
        }
    }
}
