using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class PlayerMover : MonoBehaviour {

        [Require] private Position.Writer PositionWriter;
        [Require] private Rotation.Writer RotationWriter;
        [Require] private PlayerInput.Reader PlayerInputReader;

        private Vector3 defaultPos;

        private bool hasControl = true;
        private bool respawn = false;

        private Rigidbody rb;
        private Rigidbody sword;

    		void OnEnable () {
            defaultPos = gameObject.transform.position;
            rb = GetComponent<Rigidbody>();
            sword = gameObject.transform.Find("Sword").GetComponent<Rigidbody>();
    		}

    		void FixedUpdate () {
            if (respawn) {
              Reset();
              respawn = false;
            }

            if (!hasControl) {
                return;
            }

    	      var joystick = PlayerInputReader.Data.joystick;
            var fight = PlayerInputReader.Data.fight;
            var jump = PlayerInputReader.Data.jump;

    	      var direction = new Vector3(joystick.xAxis, 0, joystick.yAxis);

            // Anti-cheating
    				if (direction.sqrMagnitude > 1) {
    						direction.Normalize();
    				}

            // Fight mode
            if (fight) {

                // Slow down ball
          			if (IsGrounded()) {
                    rb.velocity = rb.velocity * 0.9f;
          			}

                // Raise sword
                if (jump & IsGrounded()) {
                  direction += new Vector3(0, SimulationSettings.PlayerSwordPower, 0);
                }

                sword.AddForce(direction * SimulationSettings.PlayerAcceleration);
        		} else {

                // Jump
                if (jump && IsGrounded()) {
                    direction += new Vector3(0, SimulationSettings.PlayerJumpPower, 0);
                }

                rb.AddForce(direction * SimulationSettings.PlayerAcceleration);
            }

    	      var newCoords = rb.position.ToCoordinates();
    	      PositionWriter.Send(new Position.Update().SetCoords(newCoords));

    				var newRotation = rb.rotation.ToNativeQuaternion();
    				RotationWriter.Send(new Rotation.Update().SetRotation(newRotation));
    		}

    		private bool IsGrounded() {
       			return Physics.Raycast(rb.position, Vector3.down, 1.0f);
     		}

        private void Reset() {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = defaultPos;
        }

        public void HasControl(bool control) {
            hasControl = control;
        }

        public void Respawn() {
            respawn = true;
        }
    }
}
