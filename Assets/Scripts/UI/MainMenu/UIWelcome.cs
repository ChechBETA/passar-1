using System;
using UnityEngine;
using System.Collections;

public class UIWelcome : DisplayObject
{	
	public UIPanel panel;
	public UIPanel welcomeText1;
	public UIPanel welcomeText2;
	public UIPassarButton button;
	
	public enum State {Initial = 0,Final = 1};
	private State panelState;
	public State PanelState
	{
		get
		{ 
			return panelState;
		}
		set
		{
			panelState = value;
		}
	}
	
	private void ShowTexts()
	{
		if(PanelState == State.Initial)
		{
			welcomeText1.BringIn();
			welcomeText2.Dismiss();
		}
		else
		{
			welcomeText1.Dismiss();
			welcomeText2.BringIn();
		}
	}
	
	private void Start()
	{
		OnChangeState(State.Initial);
		button.onPressDelegate = OnPressDelegate;
	}
	
	private void OnChangeState(State state)
	{
		PanelState = state;
		ShowTexts();
	}
	
	private void OnPressDelegate()
	{
		if(PanelState == State.Initial)
			OnChangeState(State.Final);
		else
			Hide();
	}
	
	public override void Show ()
	{
		panel.BringIn();
		panel.AddTempTransitionDelegate((_panel, transition) => 
		{
			if(onShowComplete != null)
				onShowComplete();
		});
	}
	
	public override void Hide ()
	{
		panel.Dismiss();
		panel.AddTempTransitionDelegate((_panel, transition) => 
		{
			if(onHideComplete != null)
				onHideComplete();
		});
	}
	
}
