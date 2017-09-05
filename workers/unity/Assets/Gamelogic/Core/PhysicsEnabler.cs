using Improbable;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicsEnabler : MonoBehaviour {

    [Require] private Position.Writer PositionWriter;

    private void OnEnable()
    {
        SetPhysicsEnabled(true);
    }

    private void OnDisable()
    {
        SetPhysicsEnabled(false);
    }

    private void SetPhysicsEnabled(bool physicsEnabled)
    {
        GetComponent<Rigidbody>().isKinematic = !physicsEnabled;
    }
}
