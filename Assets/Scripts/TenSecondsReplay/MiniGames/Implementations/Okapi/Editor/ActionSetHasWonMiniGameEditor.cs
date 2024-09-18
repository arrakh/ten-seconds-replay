using OkapiKit;
using OkapiKit.Editor;
using UnityEditor;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi.Editor
{
    [CustomEditor(typeof(ActionSetHasWonMiniGame))]
    public class ActionSetHasWonMiniGameEditor : ActionEditor
    {
        SerializedProperty propHasWon;
        SerializedProperty propOkapiMinigame;

        protected override void OnEnable()
        {
            base.OnEnable();

            propHasWon = serializedObject.FindProperty("hasWon");
            propOkapiMinigame = serializedObject.FindProperty("minigame");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (WriteTitle())
            {
                StdEditor(false);

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(propHasWon, new GUIContent("Has Won", "Whether the Minigame is won or not"));
                EditorGUILayout.PropertyField(propOkapiMinigame, new GUIContent("Minigame", "Okapi Minigame"));
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