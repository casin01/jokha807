using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndingClear : MonoBehaviour
{
    public int number;

    void Start()
    {
        GameManager.Instance.clear[number-1] = 1;
    }


}
