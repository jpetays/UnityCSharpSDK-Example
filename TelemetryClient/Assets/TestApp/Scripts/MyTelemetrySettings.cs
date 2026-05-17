using UnityEngine;

namespace TelemetryClient.TestApp.Scripts
{
    [CreateAssetMenu(fileName = nameof(MyTelemetrySettings), menuName = "Telemetry/" + nameof(MyTelemetrySettings))]
    public class MyTelemetrySettings : ScriptableObject
    {
        /// <summary>
        /// ID for the app; provided by TelemetryDeck.
        /// You can (re-)generate this ID in the TelemetryDeck settings.
        /// </summary>
        [Header("Telemetry Settings")]
        public string TelemetryAppId = "";
        /// <summary>
        /// The generic identifier we send instead of a user-specific ID.
        /// This makes sure our telemetry data is anonymized from the time it's sent.
        /// You can choose if and which user identifier you want to send in your own project.
        /// </summary>
        public string GenericUserId = "TelemetryDeckUser";
        /// <summary>
        /// TelemetryDeck Signals need unique names so we can analyze the data 
        /// and let the TelemetryDeck app make pretty charts and graphics.
        /// </summary>
        [Header("Signal Settings")]
        public string SimpleSignalName = "simpleSignal";
        public string AdvancedSignalName = "advancedSignal";
        /// <summary>
        /// TelemetryDeck Signals need unique names so we can analyze the data 
        /// and let the TelemetryDeck app make pretty charts and graphics.
        /// </summary>
        public string SignalsCountName = "numberOfSignalsThisSession";

        [Header("Test Settings")]
        public bool IsTestMode;
        public bool showDebugLogs;
    }
}
