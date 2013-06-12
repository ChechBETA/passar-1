using System;
using UnityEngine;
using System.Collections;

public class UIButtonPassar3D : MonoBehaviour 
{
	private Vector3 originalScale;
	private Transform myTransform;
	private float growScale = 0.85F;
	private bool hasBeenPressed = false;
	
	public SpriteText label;
	public SpriteText label2;
	public Action onPressDelegate;
	
	public string Text
	{
		get
		{
			return label.Text;
		}
		set
		{
			label.Text = value;
			label2.Text = value;
		}
	}
	
	private void Awake()
	{
		myTransform = transform;
		originalScale = myTransform.localScale;
	}
	
	private void OnMouseDown()
	{
		hasBeenPressed = true;
		myTransform.localScale = originalScale * growScale;
	}
	
	private void OnMouseUp()
	{
		myTransform.localScale = originalScale;
		OnExecutePressEvent();
	}
	
	private void OnExecutePressEvent()
	{
		if(!hasBeenPressed)
			return;
		
		hasBeenPressed = false;

		if(onPressDelegate != null)
			onPressDelegate();
	}
	
	private void Update()
	{
#if UNITY_IPHONE
		RaycastHit hit = new RaycastHit();
		for(int i = 0; i < Input.touchCount; i++)
		{
			if(Input.GetTouch(i).phase.Equals(TouchPhase.Began))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				
				if(Physics.Raycast(ray,out hit))
				{
					hit.transform.gameObject.SendMessage("OnMouseDown");
				}
			}
		}
#endif
	}
}
