using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;
    //stat 정보
    public int intelligence = 50;
    public int strength = 50;
    public int stress = 50;
    public int jockn = 0;

    //endingclear 여부
    public int[] clear = new int[50];


    private void Awake()
    {
        if (instance) //게임 계속 유지하려면 destroy 불필요할수도
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < 50; i++)
        {
            GameManager.Instance.clear[i] = 0;
        }
    }



}
