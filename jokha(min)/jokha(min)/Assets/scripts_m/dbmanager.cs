using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dbmanager : MonoBehaviour
{
    [SerializeField] string[] status=null;
    [SerializeField] Text text=null;
    [SerializeField] Text poptext=null;
    [SerializeField] GameObject poppanel=null;
    public GameObject jokpop;
    
    int index = 0;

    void Start()
    {
        text.text = "";
        poptext.text = "";
        poppanel.SetActive(false);
    }

    public void popjok()
    {
        var clone = Instantiate(jokpop, Vector3.back, Quaternion.identity, transform);
        //인벤토리 추가해야함
    }

    public void popstatus (int ind)
    {
        index = ind;
        poptext.text = "\"" + status[ind] + "\" 칭호 획득!";
        poppanel.SetActive(true);
    }

    public void okbutton()
    {
        poppanel.SetActive(false);
        text.text = status[index].ToString();
        Time.timeScale = 1;
    }
}