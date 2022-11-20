using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityExt
{
    [CustomEditor(typeof(ModelShuffler), editorForChildClasses: true)]
    [CanEditMultipleObjects]
    public class ModelShufflerEditor : Editor
    {
        ModelShuffler script;
        void OnEnable()
        {
            script = target as ModelShuffler;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (GUILayout.Button("Next Prop"))
            {
                script.NextProp();
            }
            if (GUILayout.Button("Refresh"))
            {
                script.EditorUpd();
            }
            GUILayout.Space(20);
            DrawDefaultInspector();
            serializedObject.ApplyModifiedProperties();
        }
    }
}