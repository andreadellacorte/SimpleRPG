using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientCameraBehaviour : MonoBehaviour {

        /*
         * Clients will only have write-access for their own designated Player entity's ClientAuthorityCheck component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players'.
         */
        [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

        private Transform cam;

        private Vector3 defaultCamOffset;

        [SerializeField]
        private Vector3 camOffset;

      	// Use this for initialization
      	void Start () {
            cam = GameObject.FindObjectOfType<Camera>().transform;
            defaultCamOffset = camOffset;
        }

        void LateUpdate () {
            cam.position = gameObject.transform.position + camOffset;
        }

        public void ResetCameraOffset() {
            camOffset = defaultCamOffset;
        }

        public void IncreaseCameraOffset() {
            camOffset *= SimulationSettings.PlayerKillSizeAward;
        }
    }
}
