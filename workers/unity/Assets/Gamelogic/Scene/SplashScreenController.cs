using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Configuration;
using Improbable.Unity.Core;
using Improbable.Unity.Core.EntityQueries;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour
{
	[SerializeField] private Button ConnectButton;

	public void AttemptSpatialOsConnection()
	{
			DisableConnectionButton();
			AttemptConnection();
	}

	private void DisableConnectionButton()
	{
			ConnectButton.interactable = false;
	}

	private void AttemptConnection()
	{
			FindObjectOfType<Bootstrap>().ConnectToClient();
			StartCoroutine(TimerUtils.WaitAndPerform(SimulationSettings.ClientConnectionTimeoutSecs, ConnectionTimeout));
	}

	private void ConnectionTimeout()
	{
			if (SpatialOS.IsConnected)
			{
					SpatialOS.Disconnect();
			}

			ConnectButton.interactable = true;
	}
}
