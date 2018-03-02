using CodeStage.AdvancedFPSCounter;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
	private void OnGUI()
	{
		if (GUILayout.Button("Add"))
		{
			AFPSCounter.AddToScene(true);
		}

		if (GUILayout.Button("Remove"))
		{
			AFPSCounter.SelfDestroy();
		}
	}
}
