using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transicion;
    [SerializeField] float timeTransition = 1f;
    public void LoadScene(string scene)
    {
        StartCoroutine(LoadLevel(scene));
    }
    public void LoadScene(int scene)
    {
        GameManager.Instance?.ResetPlayerPosition();

        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        transicion.SetTrigger("Start");

        yield return new WaitForSeconds(timeTransition);
        SceneManager.LoadScene(scene);
    }
    
    IEnumerator LoadLevel(int scene)
    {
        transicion.SetTrigger("Start");

        yield return new WaitForSeconds(timeTransition);
        SceneManager.LoadScene(scene);
    }
}
