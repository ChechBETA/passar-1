using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteriorCiruelos : MonoBehaviour 
{	
	public UIStateToggleBtn button;
	public SimpleSprite bgButton;
	public SpriteText labelButton;
	public GameObject containerFloors;
	public List<GameObject> buildingFloor;
	
	private int maxNumberFloors = 2;
	private const int FIRST_FLOOR = 0;
	private const int SECOND_FLOOR = 1;
	private const int DISABLED = 2;
	
	
	private void Awake()
	{
		bgButton.Hide(true);

		button.AddValueChangedDelegate((obj) => 
		{
			if(button.StateNum == DISABLED)
				return;
			
			OnFindObjectByFloorNumber( button.StateNum + 1 );
		});
		button.transform.parent = null;
	}
	
	private void Start()
	{
		if(GlobalParams.Instance.IsApplicationDinamic)
			button.SetToggleState(DISABLED);
		else
		{
			button.Hide(false);
			bgButton.Hide(false);
			labelButton.Hide(false);
		}
	}
	
	public void AddChild(GameObject child)
	{
		child.transform.parent = containerFloors.transform;
		buildingFloor.Add(child);
		
		if(maxNumberFloors == buildingFloor.Count)
		{
			button.SetToggleState(FIRST_FLOOR);
			bgButton.Hide(false);
		}
	}
	
	private void OnFindObjectByFloorNumber(int floor)
	{
		if(buildingFloor.Count < floor)
			return;
		
		foreach(GameObject floorGameObject in buildingFloor)
		{
			BuildingFloor build = floorGameObject.GetComponent<BuildingFloor>();
			bool isCurrentFloor = (build.floorNumber == floor);
			build.Show( isCurrentFloor );
			
			if(isCurrentFloor)
				labelButton.Text = build.floorName;
		}
	}
}
