using UnityEngine;

public class CharSelectMenu : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject charSelectCanvas;
    void Start()
    {
        Time.timeScale = 0;        
    }

    void BeginGame()
    {
        Time.timeScale = 1;
        scoreCanvas.SetActive(true);
        charSelectCanvas.SetActive(false);
    }
}
