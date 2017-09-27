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
    public class WorkerInputHandler : MonoBehaviour {

        [Require] private Position.Writer PositionWriter;
        [Require] private Rotation.Writer RotationWriter;
        [Require] private Size.Writer SizeWriter;

        [Require] private PlayerInput.Reader PlayerInputReader;

        private Vector3 defaultPos;
        private Quaternion defaultRot;

        private bool hasControl = true;
        private bool respawn = false;
        private float distanceToGround;

        private Rigidbody rb;
        private Transform swordTransform;

    		void OnEnable () {
            defaultPos = gameObject.transform.position;
            defaultRot = gameObject.transform.rotation;

            rb = GetComponent<Rigidbody>();

            swordTransform = gameObject.transform.Find("Sword").transform;
            distanceToGround = gameObject.GetComponent<SphereCollider>().radius;
    		}

    		void FixedUpdate () {

            distanceToGround =
              gameObject.GetComponent<SphereCollider>().radius
                * SizeWriter.Data.sizeMultiplier;

            if (respawn) {
                Reset();
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

                rb.AddForceAtPosition(
                  direction * SimulationSettings.PlayerAcceleration,
                  swordTransform.position);
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
            int groundLayerMask = (1 << 8);
       			return Physics.Raycast(rb.position, Vector3.down, 2 * distanceToGround, groundLayerMask);
     		}

        private void Reset() {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameObject.transform.position =
              defaultPos * SizeWriter.Data.sizeMultiplier;

            gameObject.transform.rotation = defaultRot;

            respawn = false;
        }

        public void HasControl(bool control) {
            hasControl = control;
        }

        public void Respawn() {
            respawn = true;
        }
    }
}
