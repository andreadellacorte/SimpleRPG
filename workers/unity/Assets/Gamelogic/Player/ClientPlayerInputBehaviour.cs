using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Improbable.Player;
using Improbable.Notes;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientPlayerInputBehaviour : MonoBehaviour {

        [Require] private PlayerInput.Writer PlayerInputWriter;

        private GameObject inputFieldGUI;

        private void OnEnable() {
            inputFieldGUI =
              GameObject.Find("Canvas").transform.Find("InputField").gameObject;
        }

    		void Update () {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            var feedbackButton = Input.GetKeyDown(KeyCode.Plus);

            var update = new PlayerInput.Update();
            update.SetJoystick(new Joystick(xAxis, yAxis));

            update.SetJump(Input.GetKey(KeyCode.Space));

            update.SetFight(Input.GetKey(KeyCode.LeftShift));

            PlayerInputWriter.Send(update);

            if(feedbackButton) {
                inputFieldGUI.SetActive(true);
            }
    		}
    }
}
