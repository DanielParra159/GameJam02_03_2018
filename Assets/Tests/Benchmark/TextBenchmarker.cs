using UnityEngine;
using UnityEngine.UI;

public class TextBenchmarker : MonoBehaviour
{
	public Canvas canvas;
	public GameObject target;

	public float startDelay = 2;
	public int instances = 1000;

	private void Start ()
	{
		Invoke("Run", startDelay);
	}

	private void Run()
	{
		RectTransform canvasTransform = canvas.transform as RectTransform;

		for (int i = 0; i < instances; i++)
		{
			GameObject newObj = (GameObject)Instantiate(target, target.transform.position, target.transform.rotation);
			newObj.transform.SetParent(target.transform.parent);

			RectTransform rt = (newObj.transform as RectTransform);
			//Text text = newObj.GetComponent<Text>();

			rt.position = new Vector2(Random.Range(0f, canvasTransform.rect.width), Random.Range(0f, canvasTransform.rect.height));
		}
	}
}
