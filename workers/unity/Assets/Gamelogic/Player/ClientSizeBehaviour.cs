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
    public class ClientSizeBehaviour : MonoBehaviour
    {
				[Require] private Score.Reader ScoreReader;

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
            gameObject.transform.localScale *= 1.5F;
        }
    }
}
