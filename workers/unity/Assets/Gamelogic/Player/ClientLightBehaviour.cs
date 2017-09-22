using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientLightBehaviour : MonoBehaviour {

        /*
         * Clients will only have write-access for their own designated Player entity's ClientAuthorityCheck component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players'.
         */
        [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

        private GameObject playerLight;

        [SerializeField]
        private Vector3 lightOffset;

        private Vector3 defaultLightOffset;
        private float defaultLightRange;

      	// Use this for initialization
      	void Start () {
            playerLight = GameObject.Find("Player Light");
            defaultLightOffset = lightOffset;
            defaultLightRange = playerLight.GetComponent<Light>().range;
        }

        void LateUpdate () {
            playerLight.transform.position = gameObject.transform.position + lightOffset;
        }

        public void ResetLightOffset() {
            lightOffset = defaultLightOffset;
            playerLight.GetComponent<Light>().range = defaultLightRange;
        }

        public void IncreaseLightOffset() {
            lightOffset *= SimulationSettings.PlayerKillSizeAward;
            playerLight.GetComponent<Light>().range *= SimulationSettings.PlayerKillSizeAward;
        }
    }
}
