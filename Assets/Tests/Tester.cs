using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Test
{
	public class Tester : MonoBehaviour
	{
		private void Start()
		{
			/*float f = 0;
			string someStr = "";
			int num = Random.Range(1, 100);*/

			StringBuilder text = new StringBuilder();

			Stopwatch sw = Stopwatch.StartNew();
			for (int i = 0; i < 100000; i++)
			{

				text.Length = 0;
				text.Append(1.43564334.ToString("F"));
				
				//f = 1000f / num;

				//float truncated = (float)(Math.Truncate((double)f*100.0) / 100.0);

				//float rounded = (float)(Math.Round((double)f, 2));

				//someStr = f.ToString("F");
				//someStr = rounded.ToString();
			}
			sw.Stop();
			Debug.Log(sw.ElapsedMilliseconds);
			Debug.Log(text);
		}
	}
}
