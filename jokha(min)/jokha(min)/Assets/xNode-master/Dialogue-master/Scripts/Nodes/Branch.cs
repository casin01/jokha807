using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace Dialogue {
    [NodeTint("#EEEEFF")]
    public class Branch : DialogueBaseNode {

        public Condition[] conditions;

        [Output] public DialogueBaseNode pass;
        [Output] public DialogueBaseNode fail;

        private bool success;

        [SerializeField] [TextArea] string memo;

        public override void Trigger() {
            // Perform condition
            bool success = true;
            for (int i = 0; i < conditions.Length; i++) {
                if (!conditions[i].Invoke()) {
                    success = false;
                    break;
                }
            }

            //Trigger next nodes
            NodePort port;
            if (success) port = GetOutputPort("pass");
            else port = GetOutputPort("fail");
            if (port == null) return;

            NodePort connection;
            if (port.ConnectionCount > 1)
            {
                int ran = UnityEngine.Random.Range(0, port.ConnectionCount);
                connection = port.GetConnection(ran);
            }
            else
            {
                connection = port.GetConnection(0);
            }
            (connection.node as DialogueBaseNode).Trigger();

            /*
            for (int i = 0; i < port.ConnectionCount; i++) {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
               
            }
            */
        }      

    }

    [Serializable]
    public class Condition : SerializableCallback<bool>
    {
    }
}