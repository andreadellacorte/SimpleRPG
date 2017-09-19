using Improbable.Entity.Component;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on UnityWorker (server-side) workers only
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerKillHandler : MonoBehaviour {
        /*
         * An entity with this MonoBehaviour will only be enabled for the single UnityWorker
         * which has write access for its Score component.
         */
        [Require] private Score.Writer ScoreWriter;

        void OnEnable() {
            // Register command callback
            ScoreWriter.CommandReceiver.OnAwardPoints.RegisterResponse(OnAwardPoints);
        }

        private void OnDisable() {
            // Deregister command callbacks
            ScoreWriter.CommandReceiver.OnAwardPoints.DeregisterResponse();
        }

        // Command callback for handling points awarded by other entities when they sink
        private AwardResponse OnAwardPoints(AwardPoints request, ICommandCallerInfo callerInfo) {

            AwardPoints((int)request.amount);
            AwardSize();

            // Acknowledge command receipt
            return new AwardResponse(request.amount);
        }

        private void AwardPoints(int amount) {
            int newScore = ScoreWriter.Data.score + amount;
            ScoreWriter.Send(new Score.Update().SetScore(newScore));
        }

        private void AwardSize() {
            gameObject.transform.localScale *= 1.5F;
        }
    }
}
