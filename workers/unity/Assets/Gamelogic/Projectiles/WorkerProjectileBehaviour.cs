using Improbable;
using Improbable.Projectiles;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Projectiles
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerProjectileBehaviour : MonoBehaviour
    {
        [Require] private Owner.Reader Owner;

        private void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            Vector3 direction = (transform.forward).normalized;

            rb.AddForce(direction * 15, ForceMode.Impulse);

            GetComponent<WorkerItemHandler>().playerId = Owner.Data.owner;
        }
    }
}
