using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelController : MonoBehaviour, IScoreListener {

	public Text label;

	void Start() {
		Contexts.sharedInstance.game.CreateEntity().AddScoreListener(this);
	}

	public void OnScore(GameEntity entity, int value) {
		label.text = "Score " + value;
	}
}
