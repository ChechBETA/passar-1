using UnityEngine;
using System.Collections;

public class FooterBar : MonoBehaviour, IUIDisplayObject
{
	
	public UIPanel panel;
	public UIPassarButton home;
	public UIPassarButton markers;
	public UIPassarButton contact;
	public UIPassarButton help;
	
	private void Start () 
	{
		home.onPressDelegate = GoHome;
		markers.onPressDelegate = GoMarkers;
		contact.onPressDelegate = GoContact;
		help.onPressDelegate = GoHelp;
	}
	
	public void Show()
	{
		panel.BringIn();
	}
	
	public void Hide ()
	{
		panel.Dismiss();
	}
	
	private void GoHome()
	{
		LevelLoader.Instance.LoadScene(Level.MainMenu);
	}
	
	private void GoMarkers()
	{
		Application.OpenURL("http://www.passar.co/pedrogomez/marcadores.html");
	}
	
	private void GoContact()
	{
		UI.Instance.MenuState = MenuState.Contact;
	}
	
	private void GoHelp()
	{
		UI.Instance.MenuState = MenuState.Help;
	}
	
}
