/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class EndingSignal // 엔딩 시그널 선언 함수
{
    public static int[] Signal = new int[3];

}
public class EndingVisible : MonoBehaviour
{

    public GameObject[] Ending = new GameObject[3];
   // public int[] signal = new int[3];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


        // Update is called once per frame
        void Update()
    {
        // update 코드 수정 필요 (오버헤드 우려됨)

        for (int j = 0; j < 3; j++)
        {
            Debug.Log("j변경");
            if (EndingSignal.Signal[j] == 1)
            {
                Debug.Log("신호받음");
                Ending[j].SetActive(true);
            }
        }

 
    }
    

   
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingVisible : MonoBehaviour
{
    public int n;
    private int i;
    public GameObject[] Ending = new GameObject[3];

    //private EndingClear IsClear = null;

    // Start is called before the first frame update
    void Start()
    {
        //IsClear = GameObject.Find("EndingCanvas").GetComponent<EndingClear>();
    }

    // Update i을 때 사용하는 명령어로 오브젝s called once per frame
    void Update()
    {
        for (int j = 0;  j < 3; j++)
        {
            if (GameManager.Instance.clear[j] != 0)
            {
                //Debug.Log(GameManager.Instance.clear[n]);
                Ending[j].SetActive(true);
            }
        }
    }
}
