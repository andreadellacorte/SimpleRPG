using Improbable;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
		[WorkerType(WorkerPlatform.UnityWorker)]
		public class WorkerBladeHandler : MonoBehaviour {
				public EntityId playerId;
		}
}
