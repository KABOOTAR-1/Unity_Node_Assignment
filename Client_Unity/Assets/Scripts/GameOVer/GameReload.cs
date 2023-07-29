using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameReload : MonoBehaviour
{
    
    void Start()
    {
        //Invoke("GameOver",5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("Called");
        Debug.Log(SceneManager.GetSceneByBuildIndex(0).name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); ;
    }

    public void GameExit()
    {
      #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
      #else

       Application.Quit();

      #endif
    }
 }
