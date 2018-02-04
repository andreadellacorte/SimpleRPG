using Improbable;
using Improbable.Entity.Component;
using Improbable.Notes;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Notes
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class WorkerNoticeCreatingBehaviour : MonoBehaviour
    {
        [Require]
        private Notice.Writer NoticeWriter;

        private void OnEnable()
        {
            NoticeWriter.CommandReceiver.OnCreateNotice.RegisterResponse(OnCreateNotice);
        }

        private void OnDisable()
        {
            NoticeWriter.CommandReceiver.OnCreateNotice.DeregisterResponse();
        }

        private CreateNoticeResponse OnCreateNotice(CreateNoticeRequest request, ICommandCallerInfo callerinfo)
        {
            //CreateNotice(request.Data.text, request.Data.x, request.Data.y);
            return new CreateNoticeResponse();
        }

        private void CreateNotice(string text, Vector3 coordinates)
        {
            //var noticeEntityTemplate = EntityTemplateFactory.CreateNoticeTemplate("Hello");
            //SpatialOS.Commands.CreateEntity(NoticeWriter, entityId, noticeEntityTemplate)
						//		.OnSuccess(result => CreateNoticeResponse())
						//		.OnFailure(failure => OnFailedNoticeCreation(failure, text, coordinates));
        }

        private void OnFailedNoticeCreation(ICommandErrorDetails response, string text, Vector3 coordinates)
        {
            Debug.LogError("Failed to Create Notice: " + response.ErrorMessage + ". Retrying...");
            CreateNotice(text, coordinates);
        }
    }
}
