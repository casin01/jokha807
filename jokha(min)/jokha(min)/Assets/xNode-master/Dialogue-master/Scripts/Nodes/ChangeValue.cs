using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [NodeTint("#FFFFFF")]
    public class ChangeValue : DialogueBaseNode
    {
        static dbplayer player;
        public static dbplayer cvPlayer { get => player; set => player = value; }
        
        //       public static FloatingText floating;
        public Val[] vals;
        [System.Serializable]   public class Val
        {
            public int ind;
            public int val;
        }
        Color textcolor;
        int i = 0;
        int index = 0;
        public static bool flag;

        [HideInInspector] public static GameObject floatingtext;

        //플로팅 텍스트 변경
        public string ChangeText(int ind)
        {
            switch (ind)
            {
                case 0:
                    textcolor = Color.red;
                    return "지능";
                case 1:
                    textcolor = Color.black;
                    return "인맥";
                case 2:
                    textcolor = Color.green;
                    return "돈";
                case 3:
                    textcolor = Color.blue;
                    return "스트레스";
                case 4:
                    textcolor = Color.magenta;
                    return "체력";
                default:
                    Debug.Log("default");
                    textcolor = Color.clear;
                    return "";
            }
        }

        //플로팅텍스트
        void Floating_Text(int ind, int val)
        {  
           //사운드 재생
           /*use instantiate  */
            Canvas p = FindObjectOfType<Canvas>();
 //           FloatingText floating = FindObjectOfType<FloatingText>();
            string text = ChangeText(ind);
            var clone = Instantiate(floatingtext, Vector3.back, Quaternion.identity, p.transform); 
            if(val>0)   clone.GetComponent<TextMesh>().text = text + " +" + val;
            else clone.GetComponent<TextMesh>().text = text + " " + val;
            clone.GetComponent<TextMesh>().color = textcolor;
        }

        

        public override void Trigger()
        {
            int[] stat = player.Stat;
            int[] pcon = player.Con;

            for (i=0; i < vals.Length ; i++)
            {
                index = vals[i].ind;

                if (index < 5) //스탯일 경우
                {
                    stat[index] += vals[i].val;
                    if (stat[index] < 0)
                    {
                        stat[index] = 0;
                        if( (index==2) || (index == 4)) //돈, 체력이 0이 되면
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (stat[index] > 100)
                    {
                        stat[index] = 100;
                        if (index == 3)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (floatingtext != null)  {   Floating_Text(index, vals[i].val);  }
    //                floating.f(index, vals[i].val);
                }

                else
                { 
                    pcon[index] += vals[i].val;
                    if (pcon[index] < 0) pcon[index] = 0;
                }
            }

            if (flag)
            {
                switch (index)
                {
                    case 2:
                        player.startending(14);
                        break;
                    case 3:
                        player.startending(13);
                        break;
                    case 4:
                        player.startending(12);
                        break;
                }
            }

            NodePort port;
            port = GetOutputPort("output");
            if (!port.IsConnected) { Debug.Log("!port.isconnected"); }
            if (port == null) return;

            NodePort connection;
            if (port.ConnectionCount > 1)
            {
                int ran = Random.Range(0, port.ConnectionCount);
                connection = port.GetConnection(ran);
            }
            else
            {
                connection = port.GetConnection(0);
            }
            (connection.node as DialogueBaseNode).Trigger();

        }
    }
}
