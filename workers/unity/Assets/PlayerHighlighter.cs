using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class PlayerHighlighter : MonoBehaviour {

    [Require] private PlayerInput.Writer PlayerInputWriter;

    [SerializeField] private GameObject playerBody;
    [SerializeField] private Material material;

    private void OnEnable()
    {
        playerBody.GetComponent<Renderer>().material = material;
    }
}
