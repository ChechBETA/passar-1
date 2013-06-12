using UnityEngine;
using System.Collections;

public class PersistenObject : MonoBehaviour 
{
	private static bool isAlreadyPresent = false;
	
	private void Awake()
	{
		if( isAlreadyPresent )
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			isAlreadyPresent = true;
			DontDestroyOnLoad(gameObject);
		}
		
	}
}
