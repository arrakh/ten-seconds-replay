using OkapiKit.Editor;
using UnityEditor;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi.Editor
{
    [CustomEditor(typeof(TriggerOnGameInput))]
    public class TriggerOnGameInputEditor : TriggerEditor
    {
        SerializedProperty propOkapiMiniGame;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            propOkapiMiniGame = serializedObject.FindProperty("miniGame");
        }


        protected override Texture2D GetIcon()
        {
            var varTexture = GUIUtils.GetTexture("Reset");

            return varTexture;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (WriteTitle())
            {
                StdEditor(false);

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(propOkapiMiniGame, new GUIContent("Okapi Minigame", "Insert the reference to the OkapiMiniGame component"));
                EditorGUI.EndChangeCheck();

                ActionPanel();
            }
        }
    }
}