using Improbable;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Assets.Gamelogic.Projectiles;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
		[WorkerType(WorkerPlatform.UnityWorker)]
		public class WorkerPlayerSetup : MonoBehaviour {

			// Use this for initialization
			void Start () {
					SetBladePlayerEntity();
			}

			void SetBladePlayerEntity() {
					gameObject
						.transform.Find("Sword/Blade")
						.GetComponent<WorkerItemHandler>().playerId = gameObject.EntityId();
			}
		}
}
