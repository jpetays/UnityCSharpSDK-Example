using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TelemetryClient.TestApp.Scripts
{
    public class AppManager : MonoBehaviour
    {
        private const string githubRepoURL = "https://github.com/jpetays/UnityCSharpSDK-Example";

        [Header("Preferences")]
        private bool initializeTelemetryOnAwake = false;

        [Header("UI")]
        [SerializeField]
        private Text uiText;
        [SerializeField]
        private Button initTelemetryButton;
        [SerializeField]
        private Button sendSimpleButton;
        [SerializeField]
        private Button sendAdvancedButton;
        [SerializeField]
        private Button startNewSessionButton;
        [SerializeField]
        private Button stopTelemetryButton;

        private int numberOfSignalsSentThisSession = 0;
        private bool TelemetryInitialized => TelemetryManager.IsInitialized;
        private MyTelemetrySettings myTelemetrySettings;

        private void Awake()
        {
            myTelemetrySettings = Resources.Load<MyTelemetrySettings>(nameof(MyTelemetrySettings));
            if (myTelemetrySettings == null)
            {
                myTelemetrySettings = ScriptableObject.CreateInstance<MyTelemetrySettings>();
            }
            if (initializeTelemetryOnAwake)
                InitializeTelemetryIfNeeded();
        }

        private void Start()
        {
            UpdateUI();
        }

        /// <summary>
        /// Setup the Telemetry Manager so we can begin sending Signals.
        /// You may want to put this inside your project's [RuntimeInitializeOnLoadMethod].
        /// </summary>
        public void InitializeTelemetryIfNeeded()
        {
            if (TelemetryInitialized)
                return;

            if (myTelemetrySettings.showDebugLogs)
            {
                Debug.Log($"AppId {myTelemetrySettings.TelemetryAppId}");
                Debug.Log($"UserId {myTelemetrySettings.GenericUserId}");
            }
            Assert.IsTrue(!string.IsNullOrWhiteSpace(myTelemetrySettings.TelemetryAppId), "TelemetryAppId is required");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(myTelemetrySettings.GenericUserId), "GenericUserId is required");
            if (myTelemetrySettings.IsTestMode)
            {
                Debug.LogWarning("TelemetryDeck SDK Test Mode is enabled");
            }
            var configuration = new TelemetryManagerConfiguration(myTelemetrySettings.TelemetryAppId);
            // anonymize the telemetry sent entirely by setting a generic user ID
            configuration.defaultUser = myTelemetrySettings.GenericUserId;
            configuration.IsTestMode = myTelemetrySettings.IsTestMode;
            configuration.showDebugLogs = myTelemetrySettings.showDebugLogs;
            // initialize the TelemetryClient (otherwise we can't send any Signals)
            // you can delay this call to whenever you choose to start sending Signals
            // TelemetryClient will automatically attempt to send a "new session" signal.
            TelemetryManager.Initialize(configuration);
            UpdateUI();
        }

        /// <summary>
        /// Updates the UI to reflect the Telemetry state.
        /// </summary>
        private void UpdateUI()
        {
            if (TelemetryInitialized)
            {
                string newText = "Telemetry initialized\n";
                newText += string.Format("Sent {0:d} signals this session.", numberOfSignalsSentThisSession);
                uiText.text = newText;
            }
            else
            {
                uiText.text = "Telemetry unavailable";
            }

            initTelemetryButton.interactable = !TelemetryInitialized;
            sendSimpleButton.interactable = TelemetryInitialized;
            sendAdvancedButton.interactable = TelemetryInitialized;
            startNewSessionButton.interactable = TelemetryInitialized;
            stopTelemetryButton.interactable = TelemetryInitialized;
        }

        /// <summary>
        /// Sends a Telemetry Signal with no additional parameters.
        /// The signal is sent without a unique user ID to make the statistics anonymous.
        /// </summary>
        public void SendSimpleSignal()
        {
            if (!TelemetryInitialized)
                return;

            TelemetryManager.SendSignal(myTelemetrySettings.SimpleSignalName, clientUser: myTelemetrySettings.GenericUserId);
            numberOfSignalsSentThisSession++;
            UpdateUI();
        }

        /// <summary>
        /// Sends a Telemetry Signal with an additional payload.
        /// The signal is sent without a unique user ID to make the statistics anonymous.
        /// </summary>
        public void SendAdvancedSignal()
        {
            if (!TelemetryInitialized)
                return;

            var numberString = string.Format("{0:d}", numberOfSignalsSentThisSession);
            var additionalPayload = new Dictionary<string, string>();
            additionalPayload.Add(myTelemetrySettings.SignalsCountName, numberString);
            TelemetryManager.SendSignal(myTelemetrySettings.AdvancedSignalName, clientUser: myTelemetrySettings.GenericUserId, additionalPayload);
            numberOfSignalsSentThisSession++;
            UpdateUI();
        }

        public void StartNewSession()
        {
            if (!TelemetryInitialized)
                return;

            TelemetryManager.Instance.GenerateNewSession();
            numberOfSignalsSentThisSession = 0;
            UpdateUI();
        }

        public void StopTelemetry()
        {
            if (!TelemetryInitialized)
                return;

            TelemetryManager.Terminate();
            UpdateUI();
        }

        public void AboutApp()
        {
            Application.OpenURL(githubRepoURL);
        }

        public void QuitApp()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
