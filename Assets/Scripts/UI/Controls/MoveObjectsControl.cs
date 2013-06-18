using UnityEngine;
using System.Collections;

public enum Axis
{
	X = 0,
	Y = 1,
	Z = 2,
	All = 3
}
public class MoveObjectsControl : MonoBehaviour {
	
	private bool hasBeenPressed = false;
	private bool isAnimationStart = false; 
	private Vector3 initialPosition = Vector3.zero;
	private Transform myTransform;
	private bool invert = false;
	
	public float speedMove = 1.0f;
	public Vector3 finalPos;
	public Axis axisMove = Axis.Z;
	
	private void Start () 
	{
		myTransform = transform;
	}
	
	private void OnMouseDown()
	{
		if( hasBeenPressed )
			return;
		
		hasBeenPressed = true;
	}
	
	private void OnMouseUp()
	{
		if( hasBeenPressed )
		{
			hasBeenPressed = false;
			isAnimationStart = true;
		}
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
		
		if(isAnimationStart)
		{
			if(!invert)
			{
				
				Move( false );
			}
			else
			{
				Move( true );
			}
		}
	}
	
	private void Move(bool invert)
	{
		switch(axisMove)
		{
			case Axis.X:
				MoveInXDirection(invert);
			break;
			
			case Axis.Z:
				MoveInZDirection(invert);
			break;
		}
	}
	
	private void MoveInZDirection( bool goInvert )
	{
		float speedMoveDirection = goInvert ? -this.speedMove : this.speedMove;
		
		myTransform.Translate(myTransform.localPosition.x,myTransform.localPosition.y,Time.deltaTime * speedMoveDirection);
		if( myTransform.localPosition.z > finalPos.z && !goInvert )
		{
			isAnimationStart = false;
			invert = !goInvert;
			myTransform.Translate(finalPos.x,finalPos.y , finalPos.z );
			myTransform.localPosition = new Vector3 (finalPos.x,finalPos.y , finalPos.z );
		}
		else if( myTransform.localPosition.z < initialPosition.z && goInvert )
		{
			isAnimationStart = false;
			invert = !goInvert;
			myTransform.Translate( 0.0F, 0.0F, 0.0F );
			myTransform.localPosition = new Vector3(0.0F,0.0F,0.0F);
		}
	}
	
	private void MoveInXDirection( bool goInvert )
	{
		float speedMoveDirection = goInvert ? -this.speedMove : this.speedMove;
		
		myTransform.Translate(Time.deltaTime * speedMoveDirection , initialPosition.y , initialPosition.z );
		if( myTransform.localPosition.x > finalPos.x && !goInvert )
		{
			isAnimationStart = false;
			invert = !goInvert;
			myTransform.Translate(finalPos.x,finalPos.y , finalPos.z );
			myTransform.localPosition = new Vector3 (finalPos.x,finalPos.y , finalPos.z );
		}
		else if( myTransform.localPosition.x < initialPosition.x && goInvert )
		{
			isAnimationStart = false;
			invert = !goInvert;
			myTransform.Translate( 0.0F, 0.0F, 0.0F );
			myTransform.localPosition = new Vector3(0.0F,0.0F,0.0F);
		}
	}
}

