using UnityEngine;
using CodeStage.AdvancedFPSCounter;

public class Dummy : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		AFPSCounter.Instance.OperationMode = OperationMode.Disabled;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
