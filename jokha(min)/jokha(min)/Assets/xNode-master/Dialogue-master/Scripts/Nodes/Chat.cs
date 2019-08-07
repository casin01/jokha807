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
        public enum fade { d, fadeout, fadein };
        [SerializeField] public fade f;

        public void AnswerQuestion(int index) {
            NodePort port = null;
            if (answers.Count == 0) {
                port = GetOutputPort("output");
            } else {
                if (answers.Count <= index) return;
                port = GetOutputPort("answers " + index);
            }
            if (!port.IsConnected) { Debug.Log("isnconnected"); }
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