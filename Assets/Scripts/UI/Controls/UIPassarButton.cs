using System;
using UnityEngine;
using System.Collections;

public class UIPassarButton : UIButton 
{
	public Action onPressDelegate;
	protected Vector3 originalPosition;
	protected Transform myTransform;
	protected Vector3 originalSize;
	protected float growScale = 0.85F;
	private bool wasPressed;
	private float startTimePressed;
	private float finishTimePressed;
	
	private SimpleSprite [] images;
	
	public float TimeLapsePressed
	{
		get
		{ 
			float timeLapse = finishTimePressed - startTimePressed;
			return timeLapse < 0F ? 0F : timeLapse; 
		}
	}
	
	protected override void Awake ()
	{
		base.Awake ();
		images = ((MonoBehaviour)this).GetComponentsInChildren<SimpleSprite>();
	}
	
	public override void Start ()
	{
		base.Start ();
		myTransform = transform;
		originalSize = myTransform.localScale;
		originalPosition = myTransform.localPosition;
		
		AddInputDelegate(OnPressButton);
	}
	
	protected override void OnDisable ()
	{
		base.OnDisable ();
		wasPressed = false;
	}
	
	public override void Hide (bool tf)
	{
		base.Hide (tf);
		foreach(SimpleSprite image in images)
			image.Hide(tf);
	}
	
	private void OnPressButton(ref POINTER_INFO ptr)
	{
		if(ptr.evt == POINTER_INFO.INPUT_EVENT.TAP)
		{
			wasPressed = true;
			ActivatePressedState(false,true);
		}
		else if(ptr.evt == POINTER_INFO.INPUT_EVENT.PRESS)
		{
			wasPressed = true;
			ActivatePressedState(true, false);
		}
		else if(ptr.evt == POINTER_INFO.INPUT_EVENT.RELEASE)
		{
			
			ActivatePressedState(false,true);
		}
		else if(ptr.evt == POINTER_INFO.INPUT_EVENT.RELEASE_OFF)
		{
			ActivatePressedState(false,false);
		}
	}
	
	private void ActivatePressedState(bool bPressed, bool executeDelegate)
	{
		if(bPressed)
		{
			startTimePressed = Time.realtimeSinceStartup;
			myTransform.localScale = originalSize * growScale;
		}
		else
		{
			myTransform.localScale = originalSize;
			finishTimePressed = Time.realtimeSinceStartup;
			if(executeDelegate && wasPressed)
			{
				wasPressed = false;
				ExecuteButtonDelegate();
			}
		}
	}
	
	private void ExecuteButtonDelegate()
	{
		if(onPressDelegate != null)
			onPressDelegate();
	}
}