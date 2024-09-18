using OkapiKit;
using OkapiKit.Editor;
using UnityEditor;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi.Editor
{
    [CustomEditor(typeof(ActionDebugLog))]
    public class ActionDebugLogEditor : ActionEditor
    {
        SerializedProperty propMessage;

        protected override void OnEnable()
        {
            base.OnEnable();

            propMessage = serializedObject.FindProperty("message");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (WriteTitle())
            {
                StdEditor(false);

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(propMessage, new GUIContent("Message", "The message to print"));
                EditorGUI.EndChangeCheck();
                
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    (target as Action)?.UpdateExplanation();
                }
            }
        }
    }
}