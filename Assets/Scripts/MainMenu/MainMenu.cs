using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<MusicBackground>().Play();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
 //     UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
