using Assets.Gamelogic.EntityTemplates;
using Improbable;
using Improbable.Notes;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerNoticeCreatorReceiver : MonoBehaviour
    {
        [Require] private Position.Writer PositionWriter;
        [Require] private NoticeCreator.Reader NoticeCreatorReader;

        private void OnEnable()
        {
            NoticeCreatorReader.CreateTriggered.Add(Create);
        }

        private void OnDisable()
        {
            NoticeCreatorReader.CreateTriggered.Remove(Create);
        }

        private void Create(CreateNoticeData args)
        {
            var noticeTemplate = EntityTemplateFactory.CreateNoticeTemplate(args.text, args.initialPosition);
            SpatialOS.Commands.CreateEntity(PositionWriter, noticeTemplate);
        }
    }
}
