using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class diatrigger : MonoBehaviour
{
    private diamanager dm;
    public Dialogue.DialogueGraph[] nextdialog;
    int i = 0;
    public GameObject s;

    void Start()
    {        
        dm = FindObjectOfType<diamanager>();
        if(s.GetComponent<Animator>().GetBool("fade"))
            StartCoroutine(fadeout2());
    }

    IEnumerator fadeout2()
    {
        s.GetComponent<Animator>().SetBool("fade", false);
        yield return new WaitUntil(() => s.GetComponent<Image>().color.a == 0);
        s.SetActive(false);
    }

    public void showdialog()
    {
        nextdialog[0].Restart();
        dm.ShowDialogue();
    }

    public void changedia()
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
            Fadetolevel();           
        }
    }

    void Fadetolevel()
    {
        s.SetActive(true);
        s.GetComponent<Animator>().SetBool("fade", true);
        oncomplete();
    }

    public void oncomplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ScreenCoroutine()
    {
        s.SetActive(true);
        s.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitForSeconds(1.5f);

        s.GetComponent<Animator>().SetBool("fade", false);
        yield return new WaitForSeconds(1.5f);
        s.SetActive(false);
        dm.ShowDialogue();
    }
}
