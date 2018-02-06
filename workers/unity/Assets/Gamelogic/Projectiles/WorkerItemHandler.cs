using Improbable;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Projectiles
{
		[WorkerType(WorkerPlatform.UnityWorker)]
		public class WorkerItemHandler : MonoBehaviour {
				public EntityId playerId;
		}
}
