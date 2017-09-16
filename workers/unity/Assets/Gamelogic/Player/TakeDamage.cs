using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class TakeDamage : MonoBehaviour {

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

                //TODO
                //Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
            }
        }
    }
}
