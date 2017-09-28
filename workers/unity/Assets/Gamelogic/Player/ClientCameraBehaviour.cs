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

        public void UpdateCameraOffset(float newSizeMultiplier) {
            camOffset = defaultCamOffset * newSizeMultiplier;
        }
    }
}
