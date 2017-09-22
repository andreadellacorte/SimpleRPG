
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityClient)]
    public class ClientPlayerInputBehaviour : MonoBehaviour {

        [Require] private PlayerInput.Writer PlayerInputWriter;

    		void Update () {
            var xAxis = Input.GetAxis("Horizontal");
            var yAxis = Input.GetAxis("Vertical");

            var update = new PlayerInput.Update();
            update.SetJoystick(new Joystick(xAxis, yAxis));

            update.SetJump(Input.GetKey(KeyCode.Space));

            update.SetFight(Input.GetKey(KeyCode.LeftShift));

            PlayerInputWriter.Send(update);
    		}
    }
}
