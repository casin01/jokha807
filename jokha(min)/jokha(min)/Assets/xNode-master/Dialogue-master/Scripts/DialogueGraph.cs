using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/Graph", order = 0)]
    public class DialogueGraph : NodeGraph {
        public dbplayer player;
        public GameObject floating;

        [HideInInspector]
        public Chat current;

        public void Restart() { //초기화 및 선언
            //Find the first DialogueNode without any inputs. This is the starting node.
            current = nodes.Find(x => x is Chat && x.Inputs.All(y => !y.IsConnected)) as Chat;
            ChangeValue.cvPlayer = player;
            ChangeValue.floatingtext = floating;
            ChangeValue.flag = false;      //아니면 조건문으로 수정해도 됨

        }

        public Chat AnswerQuestion(int i) {
            current.AnswerQuestion(i);
            return current;
        }
    }
}