using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public string StartScene;
    public string GameScene;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(StartScene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(GameScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
