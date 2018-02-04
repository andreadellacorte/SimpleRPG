using Assets.Gamelogic.Core;
using Assets.Gamelogic.EntityTemplates;
using Improbable;
using Improbable.Worker;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	public class SnapshotMenu : MonoBehaviour
	{
		[MenuItem("Improbable/Snapshots/Generate Default Snapshot")]
		private static void GenerateDefaultSnapshot()
		{
				var snapshotEntities = new Dictionary<EntityId, Entity>();
				var currentEntityId = 1;

				snapshotEntities.Add(new EntityId(currentEntityId++), EntityTemplateFactory.CreatePlayerCreatorTemplate());
				snapshotEntities.Add(new EntityId(currentEntityId++), EntityTemplateFactory.CreateNoticeCreatorTemplate());

				string helpNotice = "Arrow Keys: Movement\nSpace: Jump\nHold Shift: Swing Sword";

				snapshotEntities.Add(new EntityId(currentEntityId++),
					EntityTemplateFactory.CreateNoticeTemplate(helpNotice,
						new Improbable.Coordinates(0, 1f, 0).ToUnityVector()));

				SaveSnapshot(snapshotEntities);
		}

		private static void SaveSnapshot(IDictionary<EntityId, Entity> snapshotEntities)
		{
				File.Delete(SimulationSettings.DefaultSnapshotPath);
				SnapshotOutputStream stream = new SnapshotOutputStream(SimulationSettings.DefaultSnapshotPath);

				foreach (EntityId key in snapshotEntities.Keys)
				{
				    Entity entity = snapshotEntities[key];

						var maybeError = stream.WriteEntity(key, entity);

						if (maybeError.HasValue)
						{
								Debug.LogErrorFormat("Failed to generate initial world snapshot: {0}", maybeError.Value);
								return;
						}
						else
						{
								Debug.LogFormat("Successfully generated initial world snapshot at {0}", SimulationSettings.DefaultSnapshotPath);
						}
				}

		}
	}
}
