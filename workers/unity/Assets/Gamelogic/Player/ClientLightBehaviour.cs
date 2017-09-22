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

        private Transform light;

        [SerializeField]
        private Vector3 lightOffset;

      	// Use this for initialization
      	void Start () {
            light = GameObject.Find("Player Light").transform;
        }

        void LateUpdate () {
            light.transform.position = gameObject.transform.position + lightOffset;
        }
    }
}
