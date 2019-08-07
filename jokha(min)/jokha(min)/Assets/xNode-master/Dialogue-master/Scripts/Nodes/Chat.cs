using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    [NodeTint("#CCFFCC")]
    public class Chat : DialogueBaseNode {

        public CharacterInfo character;
        [TextArea] public string text;
        [Output(dynamicPortList = true)] public List<Answer> answers = new List<Answer>();
        //modify1
        [System.Serializable] public class Answer {
            public string text;
   //         public AudioClip voiceClip;
        }
        [SerializeField] public bool changeUI;
        [SerializeField] public bool changeimage;
        [SerializeField] public string spritename;
        [SerializeField] public bool roulette;
        [SerializeField] public bool stop;
        [SerializeField] public bool perce;
        [SerializeField] public int [] per;
        //public enum typ { roul, stop, per};
        int t, j;
        int[] range = new int[10];

        public void AnswerQuestion(int index) {
            NodePort port = null;
            if (answers.Count == 0) {
                port = GetOutputPort("output");
            } else {
                if (answers.Count <= index) return;
                port = GetOutputPort("answers " + index);
            }
            if (!port.IsConnected) { Debug.Log("isn't connected"); }
            if (port == null) return;
            /*for (int i = 0; i < port.ConnectionCount; i++) {            
                  NodePort connection = port.GetConnection(i);
                  (connection.node as DialogueBaseNode).Trigger();
              }
            */

            NodePort connection;
            if (port.ConnectionCount > 1) {
                int ran = Random.Range(0, port.ConnectionCount);
                connection = port.GetConnection(ran);
            }
            else
            {
                connection = port.GetConnection(0);
            }
            (connection.node as DialogueBaseNode).Trigger();
        }


        public void Randomnode()
        {
            t = Random.Range(0, 100);
            Debug.Log(t.ToString());
            
            range[0] = per[0]*10;
            for (int i = 1; i < (answers.Count - 1); i++)
            {
                range[i] = range[i - 1] + per[i]*10;
            }
            j = 0;

            while (j < (answers.Count - 1))
            {
                if (t < range[j])
                {
                    AnswerQuestion(j);
                    break;
                }
                else
                {
                    j++;
                }
            }
            if (j == (answers.Count - 1))
            {
                AnswerQuestion(j);
            }

        }

        public bool hasOutput()
        {
            if ( GetOutputPort("output").IsConnected ) { return true; }
            else { return false; }
        }

        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }
    }
}