using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chomanager : MonoBehaviour
{
    public static chomanager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    //private AudioManager theAudio;

    private string question;
    private List<Dialogue.Chat.Answer> answerList;

    public GameObject go; // 평소에 비활성화 시킬 목적으로 선언. setActive.

    public Text question_Text;
    public Text[] answer_Text;  //화면 answer
    public GameObject[] answer_Panel;

    public Animator anim;

    //    public string keySound;
    //    public string enterSound;

    public bool choiceIng; // 대기. ()=> !choiceIng
    private bool keyInput; // 키처리 활성화, 비 활성화.

    private int count;  //선택지 개수
    private int result;

    Dialogue.Chat cur;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        //   theAudio = FindObjectOfType<AudioManager>();
        
        for (int i = 0; i < answer_Text.Length; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";
    }

    public void ShowChoice(Dialogue.Chat c)
    {
        choiceIng = true;
        go.SetActive(true);
        result = 0;
        answerList = c.answers;
        question = c.text;
        
        for (int i = 0; i < answerList.Count; i++)
        {         
            answer_Panel[i].SetActive(true);
            count = i;
        }
        anim.SetBool("Appear", true);
        //      Selection();    고를 때 색깔 활성화
        StartCoroutine(ChoiceCoroutine());
    }

    public int GetResult()
    {
        return result;
    }

    public void ExitChoice()
    {
        StopAllCoroutines();
        question_Text.text = "";

        for (int i = 0; i <= count; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        
        anim.SetBool("Appear", false);
        choiceIng = false;
        
        go.SetActive(false);
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer_0());
        if (count >= 1)
            StartCoroutine(TypingAnswer_1());
        if (count >= 2)
            StartCoroutine(TypingAnswer_2());
        if (count >= 3)
            StartCoroutine(TypingAnswer_3());

        yield return new WaitForSeconds(0.5f);
        keyInput = true;
    }

    IEnumerator TypingQuestion()
    {
        for (int i = 0; i < question.Length; i++)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer_0()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].text.Length; i++)
        {
            answer_Text[0].text += answerList[0].text[i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer_1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answerList[1].text.Length; i++)
        {
            answer_Text[1].text += answerList[1].text[i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer_2()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < answerList[2].text.Length; i++)
        {
            answer_Text[2].text += answerList[2].text[i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer_3()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < answerList[3].text.Length; i++)
        {
            answer_Text[3].text += answerList[3].text[i];
            yield return waitTime;
        }
    }


    public void choice1()
    {
        if (keyInput)
        {
            result = 0;
            keyInput = false;
            ExitChoice();
        }
    }

    public void choice2()
    {
        if (keyInput)
        {
            result = 1;
            keyInput = false;
            ExitChoice();
        }
    }

    public void choice3()
    {
        if (keyInput)
        {
            result = 2;
            keyInput = false;
            ExitChoice();
        }
    }

    public void choice4()
    {
        if (keyInput)
        {
            result = 3;
            keyInput = false;
            ExitChoice();
        }
    }

    /*
    public void Selection()
    {
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for (int i = 0; i <= count; i++)
        {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_Panel[result].GetComponent<Image>().color = color;
    }
    */
}
