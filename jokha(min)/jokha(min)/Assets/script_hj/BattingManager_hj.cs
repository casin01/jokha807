using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattingManager_hj : MonoBehaviour
{
    public GameObject selection;
    public GameObject howmuch;

    public static int guess = 0;
    public static int start = 0;
    public static int money;
    int tempMoney;
    [SerializeField] private InputField BattingMoney;

    private void Awake()
    {
        money = 0;
        tempMoney = 0;
        BattingMoney.text = money.ToString();
        selection = GameObject.Find("Canvas").transform.Find("Selection").gameObject;
        howmuch = GameObject.Find("Canvas").transform.Find("HowMuch").gameObject;
    }
    public void Batting(int b)
    {
        guess = b;
        start = 1;
        selection.SetActive(false);
    }

    public void PlusMoney()
    {
        if(tempMoney >= 0 && tempMoney < 20000)
        {
            tempMoney += 1000;
            BattingMoney.text = tempMoney.ToString();
        }
    }

    public void MinusMoney()
    {
        if (tempMoney > 0 && tempMoney <= 20000)
        {
            tempMoney -= 1000;
            BattingMoney.text = tempMoney.ToString();
        }
    }

    public void ConfirmMoney()
    {
        money = tempMoney;
        howmuch.SetActive(false);
        selection.SetActive(true);
    }
}
