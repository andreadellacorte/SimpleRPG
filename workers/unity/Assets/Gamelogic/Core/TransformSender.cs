using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System.Collections;

namespace Assets.Gamelogic.Core
{
		[WorkerType(WorkerPlatform.UnityWorker)]
		public class TransformSender : MonoBehaviour {

		    [Require] private Position.Writer PositionWriter;
		    [Require] private Rotation.Writer RotationWriter;

	    	void Update () {

						var newCoords = transform.position.ToCoordinates();;
						if (PositionNeedsUpdate(newCoords)) {
								PositionWriter.Send(new Position.Update().SetCoords(newCoords));
						}

						var newRotation = transform.rotation;
						if (RotationNeedsUpdate(newRotation)) {
								RotationWriter.Send(new Rotation.Update().SetRotation(transform.rotation.ToNativeQuaternion()));
						}
				}

				private bool PositionNeedsUpdate(Coordinates newCoords) {
		        return !MathUtils.ApproximatelyEqual(newCoords, PositionWriter.Data.coords);
		    }

		    private bool RotationNeedsUpdate(UnityEngine.Quaternion newRotation) {
		        return !MathUtils.ApproximatelyEqual(newRotation, RotationWriter.Data.rotation.ToUnityQuaternion());
		    }
		}
}
