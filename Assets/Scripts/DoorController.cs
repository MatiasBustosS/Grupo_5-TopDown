using System;
using System.Collections;
using GameKits.InventorySystem.ScriptableObjects;
using GameKits.InventorySystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Scenes _scenes;
    [SerializeField] private bool _needKey;
    [SerializeField] private int _doorId;
    [SerializeField] private ItemData _key;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    public int DoorID => _doorId;
    
    private LevelLoader levelLoader;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        levelLoader = FindFirstObjectByType<LevelLoader>().GetComponent<LevelLoader>();

    }

    IEnumerator OpenDoor()
    {
        _spriteRenderer.enabled = true;
        _animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.51f);
        if(levelLoader != null)
            levelLoader.LoadScene(_scenes.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.instance.TryConsume(_key) && _needKey)
            {
                GameManager.Instance.SavePlayerPosition(other.transform.position);
                GameManager.Instance.lastDoorID = _doorId;
                StartCoroutine(OpenDoor());
            }
            
            else if(!_needKey)
            {
                GameManager.Instance.SavePlayerPosition(other.transform.position);
                GameManager.Instance.lastDoorID = _doorId;
                levelLoader.LoadScene(_scenes.ToString());
            }
        }
    }
    
}
