using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer_hj : MonoBehaviour
{
    
    public void SuccessResult(int a)
    {
        if(MakeRandom_hj.real[a] == 3)
            gameObject.SetActive(true);
    }

    public void FailResult(int b)
    {
        if (MakeRandom_hj.real[b] != 3)
            gameObject.SetActive(true);
    }
}
