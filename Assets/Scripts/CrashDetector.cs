using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay;
    [SerializeField] ParticleSystem crashParticles;
    
    PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            playerControllerScript.DisableControls();
            crashParticles.Play();
            Invoke("ReloadScene", reloadSceneDelay);
            Debug.Log("You crashed :(");
        }
    }

    void ReloadScene()
    { 
        SceneManager.LoadScene(0);
    }
}
