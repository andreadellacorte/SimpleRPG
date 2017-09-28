using Assets.Gamelogic.Core;
using Improbable.Entity.Component;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on UnityWorker (server-side) workers only
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerPointsHandler : MonoBehaviour {
        /*
         * An entity with this MonoBehaviour will only be enabled for the single UnityWorker
         * which has write access for its Score component.
         */
        [Require] private Score.Writer ScoreWriter;
        [Require] private Size.Writer SizeWriter;
        [Require] private Health.Writer HealthWriter;

        private Rigidbody rb;

        void OnEnable() {
            rb = GetComponent<Rigidbody>();

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

            if(request.isKill) {
                AwardSize();
                AwardHealth();
            }

            // Acknowledge command receipt
            return new AwardResponse(request.amount);
        }

        private void AwardPoints(int amount) {
            int newScore = ScoreWriter.Data.score + amount;
            ScoreWriter.Send(new Score.Update().SetScore(newScore));
        }

        private void AwardSize() {
            float newSizeMultiplier
              = SizeWriter.Data.sizeMultiplier
                * SimulationSettings.PlayerKillSizeAward;
            SizeWriter.Send(new Size.Update().SetSizeMultiplier(newSizeMultiplier));

            rb.mass *= SimulationSettings.PlayerKillSizeAward;
        }

        private void AwardHealth() {
            int currentHealth = HealthWriter.Data.health;

            HealthWriter.Send(new Health.Update().SetHealth(currentHealth
              + SimulationSettings.PlayerKillHealthAward));
        }
    }
}
