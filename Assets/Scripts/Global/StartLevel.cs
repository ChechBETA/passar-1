using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour {

	private void Start () 
	{
		LevelLoader.Instance.LoadScene(Level.MainMenu);
	}
}
