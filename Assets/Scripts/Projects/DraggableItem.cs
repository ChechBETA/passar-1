using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshCollider))]
public class DraggableItem : MonoBehaviour 
{
	public float horizontalLimit = 25F;
	public float dragSpeed = 1F;
	
	private Transform myTransform;
	private Vector3 initialPosition;
	
	private void Start()
	{
		myTransform = transform;
		initialPosition = myTransform.localPosition;
	}
	
	private void Update()
	{
		if(Application.isEditor)
		{
			EditorDetectMove();
		}
		else if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			MobileDetectMove();
		}

	}
	
	private void MobileDetectMove()
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 deltaPos = Input.GetTouch(0).deltaPosition;
			DragObject(deltaPos);
		}
	}
	
	private void EditorDetectMove()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Vector2 deltaPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			DragObject(deltaPos);
		}
	}
	
	private void DragObject( Vector2 deltaPosition )
	{
		int posX = (int)((deltaPosition.x * dragSpeed) + myTransform.position.x);
		int minXPos = (int)(initialPosition.x - horizontalLimit);
		int maxXPos = (int)(initialPosition.x + horizontalLimit);
		
		//int posY = (int)((deltaPosition.y * dragSpeed) + myTransform.position.y);
		//int minYPos = (int)(initialPosition.y - verticalLimit);
		//int maxYPos = (int)(initialPosition.y + verticalLimit);
		
		float newXPos = Mathf.Clamp(posX,minXPos,maxXPos);
		//float newYPos = Mathf.Clamp(posY,minYPos,maxYPos);
		
		//myTransform.position = new Vector3(newXPos , newYPos, myTransform.position.z );
		myTransform.position = new Vector3(newXPos , myTransform.position.y , myTransform.position.z );
	}
	
}