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

            Debug.LogError("Trigger Enter");
            /*
             * Unity's OnTriggerEnter runs even if the MonoBehaviour is disabled, so non-authoritative UnityWorkers
             * must be protected against null writers
             */
            if (HealthWriter == null)
                return;

            // Ignore collision if this player is already dead
            if (HealthWriter.Data.health <= 0)
                return;

            Debug.Log("Other tag: " + other.gameObject.tag);

            if (other != null && other.gameObject.CompareTag("Sword")) {

                Debug.LogError("Sword");
                // Reduce health of this entity when hit
                int newHealth = HealthWriter.Data.health - 250;
                HealthWriter.Send(new Health.Update().SetHealth(newHealth));

                //Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
            }

            Debug.LogWarning("Collision Exit");
        }
    }
}
