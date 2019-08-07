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
    public Image image;
    public GameObject fade;
    public Text daytext;

    void Start()
    {
        fade = GameObject.FindWithTag("fadescreen");
        dm = FindObjectOfType<diamanager>();
        daytext.text = "" + Dialogue.dbNode.day;
        dm.setgm(); //게임매니저 불러오기
        if (fade.GetComponent<Animator>().GetBool("fade"))
            StartCoroutine(fadeout2());
        dm.changegra(nextdialog[0]);
        i = 0;    
    }

    IEnumerator fadeout2()
    {
        fade.GetComponent<Animator>().SetBool("fade", false);
        yield return new WaitUntil(() => fade.GetComponent<Image>().color.a == 0);
        fade.SetActive(false);
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
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitUntil( ()=> fade.GetComponent<Image>().color.a ==1);
        oncomplete();
    }

    public void oncomplete()
    {
        //날짜에 따라
        Dialogue.dbNode.day++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ScreenCoroutine()
    {
        if (!fade.GetComponent<Animator>().GetBool("fade"))
        {
            fade.SetActive(true);
            fade.GetComponent<Animator>().SetBool("fade", true);
            yield return new WaitUntil(() => fade.GetComponent<Image>().color.a == 1);
        }
        fade.GetComponent<Animator>().SetBool("fade", false);
        yield return new WaitUntil(() => fade.GetComponent<Image>().color.a == 0);
        fade.SetActive(false);
        dm.ShowDialogue();
    }
}
