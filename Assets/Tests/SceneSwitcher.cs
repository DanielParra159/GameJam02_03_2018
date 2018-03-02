using CodeStage.AdvancedFPSCounter;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{

	private void Awake()
	{
		//AFPSCounter.Instance.fpsCounter.Anchor = LabelAnchor.UpperRight;
	}

    private void OnGUI()
    {
        if (GUILayout.Button("Next scene"))
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }

		if (GUILayout.Button("Next scene async"))
		{
			Application.LoadLevelAsync(Application.loadedLevel + 1);
		}

		if (GUILayout.Button("Next scene additive"))
		{
			Application.LoadLevelAdditive(Application.loadedLevel + 1);
		}

		if (GUILayout.Button("Next scene additive async"))
		{
			Application.LoadLevelAdditiveAsync(Application.loadedLevel + 1);
		}

        if (GUILayout.Button("Prev scene"))
        {
            Application.LoadLevel(Application.loadedLevel - 1);
        }

		if (GUILayout.Button("Prev scene async"))
		{
			Application.LoadLevelAsync(Application.loadedLevel - 1);
		}

		if (GUILayout.Button("Prev scene additive"))
		{
			Application.LoadLevelAdditive(Application.loadedLevel - 1);
		}

		if (GUILayout.Button("Prev scene additive async"))
		{
			Application.LoadLevelAdditiveAsync(Application.loadedLevel - 1);
		}

		if (GUILayout.Button("Reload scene"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if (GUILayout.Button("Add AFPSCounter"))
		{
			AFPSCounter.AddToScene(false);
		}

		if (GUILayout.Button("Add AFPSCounter keepAlive"))
		{
			AFPSCounter.AddToScene(true);
		}
	}
}
