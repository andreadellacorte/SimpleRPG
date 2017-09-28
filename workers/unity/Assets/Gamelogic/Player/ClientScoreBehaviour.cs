using Assets.Gamelogic.Core;
using Improbable.Player;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientScoreBehaviour : MonoBehaviour {

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
        }

        void updateGUI(int score) {
            totalPointsGUI.text = "Score: " + score.ToString();
        }
    }
}
