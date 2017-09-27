using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientHealthBehaviour : MonoBehaviour
    {
        // Inject access to the entity's Health component
        [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;
        [Require] private Health.Reader HealthReader;

        private Text healthGUI;

        private void OnEnable() {
            healthGUI =
              GameObject.Find("Canvas/Health").GetComponent<Text>();

            updateGUI(HealthReader.Data.health);

            // Register callback for when components change
            HealthReader.HealthUpdated.Add(OnHealthUpdated);
        }

        private void OnDisable() {
            // Deregister callback for when components change
            HealthReader.HealthUpdated.Remove(OnHealthUpdated);
        }

        // Callback for whenever the Health component is updated
        private void OnHealthUpdated(int newHealth) {
            updateGUI(newHealth);
        }

        private void updateGUI(int newHealth) {
            healthGUI.text = "Health: " + newHealth.ToString();
        }
    }
}
