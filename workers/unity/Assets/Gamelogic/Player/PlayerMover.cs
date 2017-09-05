using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class PlayerMover : MonoBehaviour {

    [Require] private Position.Writer PositionWriter;
    [Require] private Rotation.Writer RotationWriter;
    [Require] private PlayerInput.Reader PlayerInputReader;

    private Rigidbody rigidbody;
		private bool jump = false;

		void OnEnable () {
        rigidbody = GetComponent<Rigidbody>();
				PlayerInputReader.JumpTriggered.Add(OnJump);
		}

		void OnDisable() {
				PlayerInputReader.JumpTriggered.Remove(OnJump);
		}

		void FixedUpdate () {
	      var joystick = PlayerInputReader.Data.joystick;
	      var direction = new Vector3(joystick.xAxis, 0, joystick.yAxis);

				if (direction.sqrMagnitude > 1) {
						direction.Normalize();
				}

				if (jump && IsGrounded()) {
						direction += new Vector3(0, 5, 0);
				}

	      rigidbody.AddForce(direction * SimulationSettings.PlayerAcceleration);

	      var newCoords = rigidbody.position.ToCoordinates();
	      PositionWriter.Send(new Position.Update().SetCoords(newCoords));

				var newRotation = rigidbody.rotation.ToNativeQuaternion();
				RotationWriter.Send(new Rotation.Update().SetRotation(newRotation));

				jump = false;
		}

		private bool IsGrounded() {
   			return Physics.Raycast(rigidbody.position, Vector3.down, 1);
 		}

		private void OnJump(Jump _){
				jump = true;
		}
}
