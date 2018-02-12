using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientHighlightBehaviour : MonoBehaviour {

        [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

        [SerializeField] private GameObject head;
        [SerializeField] private Material material;

        private void OnEnable() {
            GameObject.Find("Player Light").GetComponent<Light>().enabled = true;
            gameObject.transform.Find("Crown").GetComponent<MeshRenderer>().enabled = true;
            head.GetComponent<SkinnedMeshRenderer>().materials[1].color = Color.red;
        }
    }
}
