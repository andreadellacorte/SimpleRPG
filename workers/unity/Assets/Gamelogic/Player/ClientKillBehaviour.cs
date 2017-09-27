using Assets.Gamelogic.Core;
using Improbable.Player;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientKillBehaviour : MonoBehaviour
    {
        /*
         * Client will only have write access for their own designated PlayerShip entity's ShipControls component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players' ships.
         */
        [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;
				[Require] private Score.Reader ScoreReader;

        private Text totalPointsGUI;

        private void Awake() {
						totalPointsGUI =
              GameObject.Find("Canvas/Score").GetComponent<Text>();

            updateGUI(0);
        }

        private void OnEnable() {
            // Register callback for when components change
            ScoreReader.ScoreUpdated.Add(OnScoreUpdated);
        }

        private void OnDisable() {
            // Deregister callback for when components change
            ScoreReader.ScoreUpdated.Remove(OnScoreUpdated);
        }

        // Callback for whenever one or more property of the Score component is updated
        private void OnScoreUpdated(int numberOfPoints) {
            updateGUI(numberOfPoints);

            IncreaseCameraOffset();
            IncreaseLightOffset();
        }

        void updateGUI(int score) {
      	     totalPointsGUI.text = "Score: " + score.ToString();
        }

        void IncreaseCameraOffset() {
            gameObject.GetComponent<ClientCameraBehaviour>().IncreaseCameraOffset();
        }

        void IncreaseLightOffset() {
            gameObject.GetComponent<ClientLightBehaviour>().IncreaseLightOffset();
        }
    }
}
