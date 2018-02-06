using Assets.Gamelogic.EntityTemplates;
using Improbable;
using Improbable.Projectiles;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerArrowCreatorReceiver : MonoBehaviour
    {
        [Require] private Position.Writer PositionWriter;
        [Require] private ArrowCreator.Reader ArrowCreatorReader;

        private void OnEnable()
        {
            ArrowCreatorReader.CreateTriggered.Add(Create);
        }

        private void OnDisable()
        {
            ArrowCreatorReader.CreateTriggered.Remove(Create);
        }

        private void Create(CreateArrowData args)
        {
            var arrowTemplate = EntityTemplateFactory.CreateArrowTemplate(
                gameObject.EntityId(),
                args.initialPosition,
                args.angle);
            SpatialOS.Commands.CreateEntity(PositionWriter, arrowTemplate);
        }
    }
}
