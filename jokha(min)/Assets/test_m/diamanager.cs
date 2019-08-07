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

//    private int count;

    public Animator animDialogueWindow;

    //    public string typeSound;
    //    public string enterSound;
    //    private AudioManager theAudio;

    public bool talking = false;
    private bool keyActivated = false;
    private playerStat p;
    ScreenManager thescreen;
    _Gamemanager gm;

    void Start()
    {
 //       count = 0;
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
//        count = 0;
    
        //       animSprite.SetBool("Appear", false);
        animDialogueWindow.SetBool("appear", false);
        talking = false;
        if (xdia.current.answers.Count != 0)
        {
            theChoice.triggerChoice(xdia.current);
        }
        else
        {
            gm.changedia();    //대화 끝
        }
    }

    IEnumerator StartDialogueCoroutine()
    {
        if (xdia.current.changeUI)
        {
            for (int i = 0; i < 5; i++)
            {
                p.changeShowtext();         //스탯창 스탯 변경      
            }
        }

        if (xdia.current.character.name == "blank")
        {
            Name.text = "";
        }
        else
        {
            Name.text += xdia.current.character.name;
        }     
        
//        if (count > 0)
//        {
                //sprite 변경
 //       }
 //       else
 //       {
     //       yield return new WaitForSeconds(0.1f);
  //      }
        
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
    
    IEnumerator Fade()
    {
        switch (xdia.current.f)
        {
            case Dialogue.Chat.fade.fadeout:
                ScreenManager.isfinished = false;               
                talking = false;
                animDialogueWindow.SetBool("appear", false);
                Name.color = Color.white;
                text.color = Color.white;
                StartCoroutine(thescreen.Fadeout(false));    
                yield return new WaitUntil(() => ScreenManager.isfinished);
                if (xdia.current.changeimage)
                {
                    ChangeBack();
                }
                animDialogueWindow.SetBool("appear", true);
                talking = true;
     //           break;
    //        case Dialogue.Chat.fade.fadein:
                ScreenManager.isfinished = false;
                talking = false;
                animDialogueWindow.SetBool("appear", false);
                Name.color = Color.black;
                text.color = Color.black;
                StartCoroutine(thescreen.Fadein(false));              
                yield return new WaitUntil(() => ScreenManager.isfinished);
                animDialogueWindow.SetBool("appear", true);
                talking = true;
                break;
            default:
                break;
        }
    }

    void ChangeBack()
    {
        ScreenManager.isfinished2 = false;
        thescreen.SpritechangeCoroutine(xdia.current.spritename);
    }

    public void displayNextSentence()
    {
        if (talking && keyActivated)
        {
            keyActivated = false;
//            count++;
            Name.text = "";
            text.text = "";
            //       theAudio.Play(enterSound);            

            StopAllCoroutines();

            StartCoroutine(Fade());
            if (xdia.current.answers.Count != 0)    //선택지
            {                
                ExitDialogue();
            }

            else
            {

                if (xdia.current.hasOutput())
                {
                    xdia.AnswerQuestion(0);

                    StartCoroutine(StartDialogueCoroutine());
                }
                else
                {
                    ExitDialogue();
                }
            }
           
        }

    }

}
