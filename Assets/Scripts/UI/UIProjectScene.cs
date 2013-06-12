using UnityEngine;
using System.Collections;

public class UIProjectScene : MonoBehaviour {
	
	public SimpleSprite logoProject;
	public SimpleSprite logoCorporation;
	public SpriteText titleProject;
	public UIPassarButton buttonClose;
	
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
		LevelLoader.Instance.HasStartsInMenu = true;
		LevelLoader.Instance.LoadScene(Level.MainMenu);
	}
}
