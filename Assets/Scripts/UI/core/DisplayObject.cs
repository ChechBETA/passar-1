using System;
using UnityEngine;
using System.Collections;

public abstract class DisplayObject : MonoBehaviour, IUIDisplayObject 
{ 
	public Action onShowComplete;
	public Action onHideComplete;
	
	public abstract void Show ();
	
	public abstract void Hide ();
}
