using UnityEngine;
using System.Collections;

public class brake_light : MonoBehaviour
{

	public Light[] lights;
	public KeyCode keyboard;
	public KeyCode keyboard2;


	void Update ()
	{
		foreach (Light light in lights) 
		{
			if (Input.GetKeyDown (keyboard)) 
				light.enabled = !light.enabled;
			{
				if (Input.GetKeyDown (keyboard2)) 
					light.enabled = !light.enabled;
				{
					if (Input.GetKeyUp (keyboard))
						light.enabled = !light.enabled;
					{
						if (Input.GetKeyUp (keyboard2))
							light.enabled = !light.enabled;
					
					}
				}
			}
		}

	}
}

