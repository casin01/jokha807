using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRandom_hj : MonoBehaviour
{
    public static int[] real = new int[3];
 
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            real[i] = Random.Range(1, 4);
            for (int j = 0; j < i; j++)
                if (real[j] == real[i])
                {
                    i--;
                    break;
                }
        }
        Debug.Log("first" + real[0] + " second" + real[1] + " third" + real[2]);

    }
}
