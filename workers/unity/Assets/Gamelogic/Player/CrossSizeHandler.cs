using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System;
using System.Collections;

namespace Assets.Gamelogic.Player
{
    public class CrossSizeHandler : MonoBehaviour {
        // Inject access to the entity's Size component
        [Require] private Size.Reader SizeReader;

        private void OnEnable() {
            // Register callback for when components change
            SizeReader.SizeMultiplierUpdated.Add(OnSizeUpdated);
        }

        private void OnDisable() {
            // Deregister callback for when components change
            SizeReader.SizeMultiplierUpdated.Remove(OnSizeUpdated);
        }

        // Callback for whenever the Health component is updated
        private void OnSizeUpdated(float newSizeMultiplier)
        {
            gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F) * newSizeMultiplier;
        }
    }
}
