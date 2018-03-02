using UnityEngine;

public class Duplicater : MonoBehaviour
{
	public GameObject target;
	public int instances = 1000;

	void Start ()
	{
		for (int i = 0; i < instances; i++)
		{
			GameObject newObj = (GameObject)Instantiate(target, target.transform.position, target.transform.rotation);
			newObj.transform.SetParent(target.transform.parent);
		}
	}
}
