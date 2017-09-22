using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Improbable;
using Improbable.Collections;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerDamageHandler : MonoBehaviour {

        [Require] private Health.Writer HealthWriter;

        private void OnTriggerEnter(Collider other) {
            /*
             * Unity's OnTriggerEnter runs even if the MonoBehaviour is disabled, so non-authoritative UnityWorkers
             * must be protected against null writers
             */
            if (HealthWriter == null)
                return;

            // Ignore collision if this player is already dead
            if (HealthWriter.Data.health <= 0)
                return;

            if (other != null && other.gameObject.CompareTag("Sword")) {

                // Reduce health of this entity when hit
                int newHealth = HealthWriter.Data.health - 250;
                HealthWriter.Send(new Health.Update().SetHealth(newHealth));

                if (newHealth <= 0) {
                    AwardPointsForKill(other.GetComponent<WorkerBladeHandler>().playerId);
                }

                //TODO
                //Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
            }
        }

        private void AwardPointsForKill(EntityId playerId) {
            uint pointsToAward = 1;
            // Use Commands API to issue an AwardPoints request to the entity who fired the cannonball
            SpatialOS.Commands.SendCommand(HealthWriter, Score.Commands.AwardPoints.Descriptor, new AwardPoints(pointsToAward), playerId)
                .OnSuccess(OnAwardPointsSuccess)
                .OnFailure(OnAwardPointsFailure);
        }

        private void OnAwardPointsSuccess(AwardResponse response)
        {
            Debug.Log("AwardPoints command succeeded. Points awarded: " + response.amount);
        }

        private void OnAwardPointsFailure(ICommandErrorDetails response)
        {
            Debug.LogError("Failed to send AwardPoints command with error: " + response.ErrorMessage);
        }
    }
}
