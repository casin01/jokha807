using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStat : MonoBehaviour {
    public static playerStat instance;

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

    [SerializeField] Dialogue.dbplayer dbplayer=null;
    int[] stat;

    public Text[] showtext = new Text[5];   //스탯 창에 뜨는 스탯    

    public GameObject FloatingText2;

    void Start() {
        instance = this;
        stat = dbplayer.Stat;

        for (int i = 0; i < 5; i++) {
            showtext[i].text = "" + stat[i];
        }
    }

    public void startStat() //스텟 초기화
    {
        stat[0] = 0;
        stat[1] = 0;
        stat[2] = 0;
        stat[3] = 0;
        stat[4] = 50;

        changeShowtext();
    }

    public void changeShowtext()    //스탯창 스탯 변경
    {
        for (int i = 0; i < 5; i++)
        {
            showtext[i].text = "" + stat[i];
        }    
     }

    /*  플로팅텍스트
        public string ChangeText(int ind)
        {
            switch (ind)
            {
                case 0:
                    textcolor = Color.red;
                    return "지능";
                case 1:
                    textcolor = Color.cyan;
                    return "인맥";
                case 2:
                    textcolor = Color.green;
                    return "운";
                case 3:
                    textcolor = Color.blue;
                    return "스트레스";
                case 4:
                    textcolor = Color.white;
                    return "돈";
                default:
                    Debug.Log("default");
                    textcolor = Color.clear;
                    return "";
            }
        }

        public void showFloating(int ind, int value)
        {
            stat[ind] += value;
            if (stat[ind] < 0) stat[ind] = 0;
            if (stat[ind] > 100) stat[ind] = 100;

            if (FloatingText2 != null)
            {
                Floating_Text(ind, value);
            }
            showtext[ind].text = "" + stat[ind];
        }

        void Floating_Text(int ind, int val)
        {  //object pooling
            string text = ChangeText(ind);
            var clone = Instantiate(FloatingText2, transform.position, Quaternion.identity, transform);
            clone.GetComponent<TextMesh>().text = text + " +" + val.ToString();
            clone.GetComponent<TextMesh>().color = textcolor;
        }*/
}