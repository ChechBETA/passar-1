using UnityEngine;
using System.Collections;

public enum Level 
{
	StartLevel = 0,
	LevelLoader = 1,
	MainMenu = 2 ,
	Project = 3,
	Macadamia = 4,
	Encenillos = 5,
	Sumapaz = 6
}

public enum SubLevel
{
	
}

public class LevelLoader : MonoBehaviour
{
	public Level PendingScene
	{
		get;
		private set;
	}
	
	public bool HasStartsInMenu
	{
		get;
		set;
	}
	
	public static LevelLoader Instance
	{
		get;
		private set;
	}
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
	
	private void OnDestroy()
	{
		Instance = null;
	}
	
	public void LoadPendingScene()
	{
		StartCoroutine (LoadLevelAsync());
	}
	
	private IEnumerator LoadLevelAsync()
	{	
		System.GC.Collect ();

		System.GC.WaitForPendingFinalizers ();

		yield return Resources.UnloadUnusedAssets();
		
		yield return Application.LoadLevelAsync ( (int) PendingScene);
	}
	
	public void LoadScene(Level level)
	{
		PendingScene = level;
		Application.LoadLevel ((int)Level.LevelLoader);
	}
	
}
