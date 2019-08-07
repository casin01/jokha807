using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpeed_hj : MonoBehaviour
{
    public static float[] speed = new float[5];
    public static int winner = 0;
    public static int min = 0;

    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            speed[i] = Random.Range(0.0f, 0.05f);
            for (int j = 0; j < i; j++)
                if (speed[j] == speed[i])
                {
                    i--;
                    break;
                }
        }

        for(int i = 0; i < 5; i++)
        {
            if (speed[i] < speed[min])
                min = i;
            if (speed[i] > speed[winner])
                winner = i;
        }
        Debug.Log("first" + speed[0] + " second" + speed[1] + " third" + speed[2] + " fourth" + speed[3] + " fifth" + speed[4]);
    }


}
