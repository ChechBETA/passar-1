using UnityEngine;
using System.Collections;

public class InteriorAvellana : MonoBehaviour {
	
	public UIStateToggleBtn button;
	public SimpleSprite bgButton;
	public SpriteText labelButton;
	public GameObject floor1;
	public GameObject floor2;
	
	private const int FIRST_FLOOR = 0;
	private const int SECOND_FLOOR = 1;
	private const int DISABLED = 2;
	
	private void Awake () 
	{
		button.AddValueChangedDelegate((obj) => 
		{
			if(button.StateNum == DISABLED)
				return;
			
			OnFindObjectByFloorNumber( button.StateNum + 1 );
		});
		button.transform.parent = null;
		labelButton.Text = "Primer piso";
	}
	
	private void Start()
	{
		if( !GlobalParams.Instance.IsApplicationDinamic )
		{
			button.Hide(false);
			bgButton.Hide(false);
			labelButton.Hide(false);
		}
	}
	
	private void OnFindObjectByFloorNumber(int floor)
	{
		floor1.SetActive(floor == 1);
		floor2.SetActive(floor == 2);
		
		labelButton.Text = floor == 1? "Primer piso" : "Segundo Piso";
	}
}
