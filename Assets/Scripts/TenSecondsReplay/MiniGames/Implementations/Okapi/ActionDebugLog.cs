using OkapiKit;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi
{
    public class ActionDebugLog : Action
    {
        [SerializeField] private string message;
        
        public override string GetRawDescription(string ident, GameObject refObject)
        {
            return $"Prints \"{message}\" in the console";
        }

        public override void Execute()
        {
            Debug.Log("PING");
        }

        protected override void CheckErrors()
        {
            base.CheckErrors();
            
            if (string.IsNullOrEmpty(message))
                _logs.Add(new LogEntry(LogEntry.Type.Error, "Message is empty!"));
        }
    }
}