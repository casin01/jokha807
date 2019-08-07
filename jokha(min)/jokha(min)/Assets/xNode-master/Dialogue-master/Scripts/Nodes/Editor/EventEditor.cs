using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(Dialogue.Event))]
    public class EventEditor : NodeEditor {

        public override void OnBodyGUI() {
            serializedObject.Update();

            Dialogue.Event node = target as Dialogue.Event;
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(target.GetInputPort("input"), GUILayout.Width(100));
            NodeEditorGUILayout.PortField(target.GetOutputPort("output"));
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("trigger"));
            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 336;
        }
    }
}