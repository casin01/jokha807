using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [NodeTint("#FFFFFF")]
    public class ChangeValue : DialogueBaseNode
    {
        public int[] value1 = new int[5];
        public int[] value2;
        public dbplayer player;
        public GameObject FloatingText2;
        Color textcolor;
        int i = 0;

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
        {  //object pooling
           //사운드 재생
            Canvas p = FindObjectOfType<Canvas>();
            string text = ChangeText(ind);
            var clone = Instantiate(FloatingText2, Vector3.back, Quaternion.identity, p.transform);
            if(val>0)   clone.GetComponent<TextMesh>().text = text + " +" + val.ToString();
            else clone.GetComponent<TextMesh>().text = text + " " + val.ToString();
            clone.GetComponent<TextMesh>().color = textcolor;
        }

        public override void Trigger()
        {
            int[] stat = player.Stat;
            float[] senior = player.Senior_p;

            if (value1.Length == 5)
            {
                for (i = 0; i < 5; i++)
                {
                    if (value1[i] != 0)
                    {
                        stat[i] += value1[i];
                        if (stat[i] < 0) stat[i] = 0;
                        if (stat[i] > 100) stat[i] = 100;

                        if (FloatingText2 != null)
                        {
                            Floating_Text(i, value1[i]);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (value2.Length == 4)
            {
                for (i = 0; i < 4; i++)
                {
                    if (value2[i] != 0)
                    {
                        senior[i] += value2[i];
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            NodePort port;
            port = GetOutputPort("output");
            if (!port.IsConnected) { Debug.Log("!port.isconnected"); }
            if (port == null) return;

            for (int i = 0; i < port.ConnectionCount; i++)
            {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }
        }
    }
}
