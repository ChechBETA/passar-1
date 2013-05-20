using UnityEngine;
using System.Collections;

public enum Levels 
{
	MainMenu = 0 , 
	Project = 1
};

public class LevelLoader : MonoBehaviour
{
	public Levels lastLoadedLevel = Levels.MainMenu; 
	
	public void StartLoadingInLoadScreen ()
	{
		StartCoroutine (LoadLevelAsync ());
	}

	private IEnumerator LoadLevelAsync ()
	{
		System.GC.Collect ();

		System.GC.WaitForPendingFinalizers ();

		yield return Resources.UnloadUnusedAssets();

		yield return Application.LoadLevelAsync ( (int) lastLoadedLevel);
	}
	
}
