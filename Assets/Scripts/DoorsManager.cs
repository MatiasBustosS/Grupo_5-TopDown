using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class DoorsManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HandleDoorSpawn());
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Enum.TryParse(sceneName, out Enums.Scenes map))
        {
            GameManager.Instance.currentScene = map;

            if (GameManager.Instance.TryGetSavedPosition(map, out Vector3 savedPosition))
            {
                Player.Instance.transform.position = savedPosition;
            }
        } 
    }

    
    IEnumerator HandleDoorSpawn()
    {
        DoorController[] doors = FindObjectsByType<DoorController>(FindObjectsSortMode.None);
        
        foreach (DoorController door in doors)
        {
            TilemapCollider2D col = door.GetComponent<TilemapCollider2D>();
            if (col != null)
                col.enabled = false;
            
            Collider2D col1 = door.GetComponent<Collider2D>();
            if (col1 != null)
                col1.enabled = false;
        }
        
        
        yield return new WaitForSeconds(2f);
        
        foreach (DoorController door in doors)
        {
            TilemapCollider2D col = door.GetComponent<TilemapCollider2D>();
            if (col != null)
                col.enabled = true;
            
            Collider2D col1 = door.GetComponent<Collider2D>();
            if (col1 != null)
                col1.enabled = true;
            
        }
    }
}
