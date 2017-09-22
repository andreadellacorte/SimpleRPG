using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System;
using System.Collections;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerDyingHandler : MonoBehaviour
    {
        // Inject access to the entity's Health component
        [Require] private Health.Writer HealthWriter;
        [Require] private Position.Writer PositionWriter;

        private WorkerInputHandler inputHandler;

        public bool isDead = false;

        private void OnEnable() {
            isDead = false;
            InitializeDyingAnimation();

            // Register callback for when components change
            HealthWriter.HealthUpdated.Add(OnHealthUpdated);

            inputHandler = GetComponent<WorkerInputHandler>();
            if (inputHandler == null) {
                Debug.LogError("PlayerInputSender not found.");
            }
        }

        private void OnDisable() {
            // Deregister callback for when components change
            HealthWriter.HealthUpdated.Remove(OnHealthUpdated);
        }

        private void Update() {
            if (PositionWriter.Data.coords.ToUnityVector().y < - 50) {
                Die();
            }
        }

        private void InitializeDyingAnimation() {
            /*
             * DyingAnimation is triggered when the ship is first killed. But a worker which checks out
             * the entity after this time (for example, a client connecting to the game later)
             * must not visualize the ship as still alive.
             *
             * Therefore, on checkout, any sunk ships jump to the end of the sinking animation.
             */
            if (HealthWriter.Data.health <= 0) {
                isDead = true;
            }
        }

        // Callback for whenever the Health component is updated
        private void OnHealthUpdated(int newHealth)
        {
            if (!isDead && newHealth <= 0) {
                Die();
            }
        }

        private void Die() {
            isDead = true;

            // Lock controls
            if (inputHandler != null) {
                inputHandler.HasControl(false);
            }

            // TODO This can cause troubles if the worker loses authority over
            // the item before respawning
            // Respawn and regain controls after x secs
            StartCoroutine(DelayedAction(Respawn, 4f));
        }

        private IEnumerator DelayedAction(Action action, float delay) {
            yield return new WaitForSeconds(delay);
            action();
        }

        private void Respawn() {
            // Initialise player
            isDead = false;

            HealthWriter.Send(new Health.Update().SetHealth(SimulationSettings.PlayerSpawnHealth));

            // Respawn character
            inputHandler.Respawn();

            // Regain controls
            inputHandler.HasControl(true);
        }
    }
}
