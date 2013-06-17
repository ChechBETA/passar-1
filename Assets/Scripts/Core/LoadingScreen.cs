using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
	private void Start () 
	{
		LevelLoader.Instance.LoadPendingScene();
	}
}
