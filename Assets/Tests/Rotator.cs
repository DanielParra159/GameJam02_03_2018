using UnityEngine;
public class Rotator : MonoBehaviour
{
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(10f * Time.deltaTime * Random.Range(1f,5f), 10f * Time.deltaTime, 10f * Time.deltaTime);
	}
}
