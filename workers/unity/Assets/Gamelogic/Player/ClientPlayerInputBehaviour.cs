using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using Improbable.Player;
using Improbable.Notes;
using Improbable.Projectiles;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientPlayerInputBehaviour : MonoBehaviour {

        [Require] private PlayerInput.Writer PlayerInputWriter;
        [Require] private NoticeCreator.Writer NoticeCreatorWriter;
        [Require] private ArrowCreator.Writer ArrowCreatorWriter;

        private GameObject inputFieldGUI;

        private void OnEnable() {
            inputFieldGUI =
              GameObject.Find("Canvas").transform.Find("InputField").gameObject;
        }

    		void Update () {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            var feedbackButton = Input.GetKeyDown(KeyCode.F);
            var arrowButton = Input.GetKeyDown(KeyCode.Q);

            var update = new PlayerInput.Update();
            update.SetJoystick(new Joystick(xAxis, yAxis));

            update.SetJump(Input.GetKey(KeyCode.Space));

            update.SetFight(Input.GetKey(KeyCode.LeftShift));

            PlayerInputWriter.Send(update);

            if(feedbackButton) {
                //inputFieldGUI.SetActive(true);
                var createPosition = transform.position;
                createPosition.y = 1f;

                NoticeCreatorWriter.Send(new NoticeCreator.Update()
                    .AddCreate(new CreateNoticeData("Hello", createPosition.ToSpatialCoordinates())));
            }

            if(arrowButton) {
                //inputFieldGUI.SetActive(true);
                var createPosition = (transform.position + ((new Vector3(xAxis, 0.5f, yAxis).normalized)));

                Vector3 relativePos = createPosition - transform.position;
                Vector3 createRotation = Quaternion.LookRotation(relativePos).eulerAngles;

                ArrowCreatorWriter.Send(new ArrowCreator.Update()
                    .AddCreate(new CreateArrowData(createPosition.ToSpatialCoordinates(),
                                                   createRotation.ToSpatialCoordinates())));
            }
    		}
    }
}
