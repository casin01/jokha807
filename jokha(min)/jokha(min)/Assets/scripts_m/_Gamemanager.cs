using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _Gamemanager : MonoBehaviour
{
    private diamanager dm;
    [SerializeField] Dialogue.DialogueGraph[] nextdialog=null;
    int i = 0;
    public GameObject roul = null;
    private ScreenManager sm;

    void Start() {
        dm = FindObjectOfType<diamanager>();
        sm = FindObjectOfType<ScreenManager>();
        sm.dayupdate();
        dm.setgm(); //게임매니저 불러오기
        dm.changegra(nextdialog[0]);
        if (roul != null) roul.SetActive(false);
        i = 0;
        StartCoroutine(fadeout2());
    }

    IEnumerator fadeout2()  //씬 시작할 때 fade in
    {
        ScreenManager.isfinished = false;
        StartCoroutine(sm.Fadein());
        yield return new WaitUntil(() => ScreenManager.isfinished);
        yield return new WaitForSeconds(0.5f);
        showdialog();
    }

    public void showdialog()
    {
        nextdialog[0].Restart();
        dm.ShowDialogue();
    }

    public void changedia() //다음 대화로
    {        
        i++;
        if (i < nextdialog.Length)
        {
            StartCoroutine(ScreenCoroutine());

            nextdialog[i].Restart();
            dm.changegra(nextdialog[i]);
        }
        else            //모든 대화 끝
        {
            StartCoroutine(Fadetolevel());
        }
    }

    IEnumerator Fadetolevel()
    {
        ScreenManager.isfinished = false;
        StartCoroutine(sm.Fadeout());
        yield return new WaitUntil(() => ScreenManager.isfinished);
        oncomplete();
    }

    public void oncomplete()
    {
        //날짜에 따라
        Dialogue.dbplayer.Day_++;
        int x = Dialogue.dbplayer.Day_;

        if ((x == 12) || (x == 13)) SceneManager.LoadScene("mtscene");
        else if (x==18) SceneManager.LoadScene("festival");
        else if ( (x==2) || (x == 8) || (x == 15) || (x == 22)) //2 8 15 22 day1
        {
            SceneManager.LoadScene("Daily1");
        }
        else if ((x == 3) || (x == 9) || (x == 16) || (x == 23)) //3 9 16 23 day2
        {
            SceneManager.LoadScene("Daily2");
        }
        else if ((x == 4) || (x == 10) || (x == 17) || (x == 24)) //4 10 17 24 day3
        {
            SceneManager.LoadScene("Daily3");
        }
        else if ((x == 5) || (x == 11) || (x == 19) || (x == 25)) //5 11 19 25 26 day4
        {
            SceneManager.LoadScene("Daily4");
        }
        //엔딩일 경우 메인 씬으로
    }

    IEnumerator ScreenCoroutine()   //한 대화가 끝나면 fade out & fade in
    {
        ScreenManager.isfinished = false;
        StartCoroutine(sm.Fadeout());
        yield return new WaitUntil(() => ScreenManager.isfinished);

        ScreenManager.isfinished = false;
        StartCoroutine(sm.Fadein());
        yield return new WaitUntil(() => ScreenManager.isfinished);

        dm.ShowDialogue();
    }
}
