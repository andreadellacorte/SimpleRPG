using Assets.Gamelogic.Core;
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

        private Rigidbody rb;

        private void OnEnable() {
           rb = gameObject.GetComponent<Rigidbody>();
        }

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
                int newHealth = HealthWriter.Data.health
                  - SimulationSettings.PlayerSwordDamage;

                HealthWriter.Send(new Health.Update().SetHealth(newHealth));

                int pointsToAward = SimulationSettings.PlayerHitPointAward;
                bool isKill = false;

                if (newHealth <= 0) {
                    isKill = true;
                    pointsToAward += SimulationSettings.PlayerKillPointAward;
                }

                AwardPointsToPlayer(pointsToAward,
                                    isKill,
                                    other
                                      .GetComponent<WorkerBladeHandler>()
                                      .playerId);
            }

            if (other != null
                  && (other.gameObject.CompareTag("Sword")
                      || other.gameObject.CompareTag("Shield"))) {

                Debug.LogError(rb);

                Vector3 direction =
                  gameObject.transform.position
                    - other.gameObject.transform.position;

                direction.y = Mathf.Abs(direction.y);

                Vector3.Normalize(direction);

                rb.AddForce(direction * 15, ForceMode.Impulse);
            }
        }

        private void AwardPointsToPlayer(int pointsToAward, bool isKill, EntityId playerId) {
            SpatialOS.Commands.SendCommand(
              HealthWriter,
              Score.Commands.AwardPoints.Descriptor,
              new AwardPoints(pointsToAward, isKill), playerId)
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
