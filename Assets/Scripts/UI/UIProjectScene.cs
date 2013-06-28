using UnityEngine;
using System.Collections;

public class UIProjectScene : MonoBehaviour {
	
	public SimpleSprite logoProject;
	public SimpleSprite logoCorporation;
	public SpriteText titleProject;
	public UIPassarButton buttonClose;
	public Camera rotateCamera;
	public GameObject ui;
	public GameObject contentItems;
	
	public static UIProjectScene Instance
	{
		get;
		private set;
	}
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}

	public void LoadRotateScene()
	{
		DontDestroyOnLoad(contentItems);
		DontDestroyOnLoad(rotateCamera.gameObject);
		DontDestroyOnLoad(ui);
		
		LevelLoader.Instance.LoadScene(Level.RotateObjects);
	}
	
	private void OnDestroy()
	{
		Instance = null;
	}
	
	protected void Start () 
	{
		buttonClose.onPressDelegate = OnCloseProject;
	}
	
	private void OnCloseProject()
	{
		//DestroyImmediate(rotateCamera.gameObject);
		//DestroyImmediate(ui);
		//DestroyImmediate(contentItems);
		
		LevelLoader.Instance.HasStartsInMenu = true;
		LevelLoader.Instance.LoadScene(Level.MainMenu);
	}
	
}
