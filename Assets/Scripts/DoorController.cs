using System;
using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private int _doorId;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private LevelLoader ll;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    IEnumerator OpenDoor()
    {
        _spriteRenderer.enabled = true;
        _animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.51f);
        ll = FindFirstObjectByType<LevelLoader>().GetComponent<LevelLoader>();
        if(ll != null)
            ll.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor());
        }
    }
}
