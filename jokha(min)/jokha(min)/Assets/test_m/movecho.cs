using System.Collections;
using UnityEngine;

public class movecho : MonoBehaviour {
    private chomanager theChoice;
    private diamanager dm;
    public bool [] flag;
    public int i = 0;
    Dialogue.Chat cur;

    void Start() { 
        theChoice = FindObjectOfType<chomanager>();
        dm = FindObjectOfType<diamanager>();
    }

    public void triggerChoice(Dialogue.Chat c)  {
        cur = c;
        if (!flag[i]) {           
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()  {
        flag[i++] = true;
        theChoice.ShowChoice(cur);
        yield return new WaitUntil(() => !theChoice.choiceIng);

        dm.sendresult(theChoice.GetResult());
        dm.ShowDialogue();
    }
}
