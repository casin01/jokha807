using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private int nextSceneToLoad;

    public void NextScene()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneToLoad);
    }

    public void PreviousScene()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(nextSceneToLoad);
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
