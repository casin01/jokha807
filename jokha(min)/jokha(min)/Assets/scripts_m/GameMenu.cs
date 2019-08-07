using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject EndPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Opening");
    }

    public void Endings()
    {
        EndPanel.SetActive(true);
    }
}
