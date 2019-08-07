using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace Dialogue {
	[NodeTint("#FFFFAA")]
	public class Event : DialogueBaseNode {

		public SerializableEvent[] trigger; // Could use UnityEvent here, but UnityEvent has a bug that prevents it from serializing correctly on custom EditorWindows. So i implemented my own.

        [SerializeField] [TextArea] string memo;

        public override void Trigger() {
			for (int i = 0; i < trigger.Length; i++) {
				trigger[i].Invoke();
			}

            NodePort port = GetOutputPort("output");
            if (!port.IsConnected) { Debug.Log("!port.isconnected event"); }
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