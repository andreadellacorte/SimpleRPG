using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
			gameObject
				.transform.Find("Sword/Blade")
				.GetComponent<BladeBehaviour>().playerId = gameObject.EntityId();
	}
}
