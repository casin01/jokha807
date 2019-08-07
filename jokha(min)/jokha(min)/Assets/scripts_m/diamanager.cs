using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diamanager : MonoBehaviour
{
    public static diamanager instance;
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

    public Text Name;
    public Text text;
    public GameObject rendererDialogueWindow;

    Dialogue.DialogueGraph xdia=null;
    private diatrigger tri;
    private movecho theChoice;

 //   private GameObject listDialogueWindows;

    public Animator animDialogueWindow;

    //    public string typeSound;
    //    public string enterSound;
    //    private AudioManager theAudio;

    public bool talking = false;
    private bool keyActivated = false;
    private playerStat p;
    _Gamemanager gm;
    ScreenManager thescreen;
    GameObject roul=null;
    enum enum1 {d, enddialog, roul, choice, ending };
    enum1 enddia=enum1.d; 

    void Start()
    {
        Name.text = "";
        text.text = "";
        tri = FindObjectOfType<diatrigger>();     
        //     theAudio = FindObjectOfType<AudioManager>();
        theChoice = FindObjectOfType<movecho>();
        p = FindObjectOfType<playerStat>();
        thescreen = FindObjectOfType<ScreenManager>();
    }

    public void setgm()
    {
        if(!gm)
            gm = FindObjectOfType<_Gamemanager>();
    }

    public void changegra(Dialogue.DialogueGraph grap)
    {
        xdia = grap;
    }

    public void sendresult(int result)
    {
        xdia.AnswerQuestion(result);
    }

    public void ShowDialogue()
    {
        talking = true;

        animDialogueWindow.SetBool("appear", true);
        StartCoroutine(StartDialogueCoroutine());
    }
    public void ExitDialogue()
    {
        Name.text = "";
        text.text = "";

        animDialogueWindow.SetBool("appear", false);
        talking = false;

        switch (enddia)
        {
            case enum1.roul:    //룰렛
                if (gm.roul != null)
                {
                    roul = gm.roul;
                    roul.SetActive(true);
                    StartCoroutine(gm.roul.GetComponent<Roulette>().Roll());
                }
                break;
            case enum1.choice:  //선택지
                theChoice.triggerChoice(xdia.current);
                break;
            case enum1.ending:  //엔딩
                break;
            default:
                gm.changedia();    //대화 끝
                break;
        }

        enddia = 0;
    }

    IEnumerator StartDialogueCoroutine()
    {
        if (xdia.current.changeUI)  //스탯이 바뀌었을 경우
        {
            for (int i = 0; i < 5; i++)
            {
                p.changeShowtext();         //스탯창 스탯 변경      
            }
        }

        if (xdia.current.changeimage)   //배경 이미지 바꾸기
        {
            talking = false;
            animDialogueWindow.SetBool("appear", false);

            ScreenManager.isfinished2 = false;
            StartCoroutine(thescreen.SpritechangeCoroutine(xdia.current.spritename));
            yield return new WaitUntil(() => ScreenManager.isfinished2);

            yield return new WaitForSeconds(1f);
            animDialogueWindow.SetBool("appear", true);
            talking = true;
        }

        if (xdia.current.perce)
        {
            xdia.current.Randomnode();
        }

        if (xdia.current.stop)
        {
            yield return new WaitUntil(() => Time.timeScale == 1);
        }

        if (xdia.current.text == ".")   enddia = enum1.enddialog;
        else if (xdia.current.roulette)    enddia = enum1.roul;
        else if (xdia.current.text == "end") enddia = enum1.ending;
        else    enddia = enum1.d;

        if (enddia > 0)
        {
            ExitDialogue();
        }

        else
        {
            if (xdia.current.character.name == "blank")
            {
                Name.text = "";
            }
            else
            {
                Name.text += xdia.current.character.name;
            }

            for (int i = 0; i < xdia.current.text.Length; i++)
            {
                text.text += xdia.current.text[i]; // 1글자씩 출력.
                if (i % 7 == 1)
                {
                    //                theAudio.Play(typeSound);
                }
                yield return new WaitForSeconds(0.01f);
            }
            keyActivated = true;
        }
    }

    public void displayNextSentence()  {    //continue button
        if (talking && keyActivated)
        {
            keyActivated = false;
            Name.text = "";
            text.text = "";
   //       theAudio.Play(enterSound);            

            StopAllCoroutines();

            if (xdia.current.answers.Count != 0)  {  //선택지
                enddia = enum1.choice;
                ExitDialogue();
            }

            else  {
                if (xdia.current.hasOutput())  {    //대화 진행
                    xdia.AnswerQuestion(0);
                    StartCoroutine(StartDialogueCoroutine());
                }
                else  { //대화 끝
                    ExitDialogue();
                }
            }         
        }
    }
}
