using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName ="Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    //private Player player;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += NuevaScenaCargada;   
    }

    private void NuevaScenaCargada(Scene arg0, LoadSceneMode arg1)
    {
        //player = GameObject.FindObjectOfType<Player>();
    }

    public void CambiarEstadoPlayer(bool estado)
    {
        //player.Interactuando = estado;
    }
}
