using UnityEngine;
using System.Collections;

public class CR_Rotate : MonoBehaviour {

	
	private float speed = 1f;
	private Transform myTransform;
	private bool canRotate = false;
	
	public bool CanRotate
	{
		get { return canRotate; } 
		private set { canRotate = value; } 
	}

	
	private void Start() 
	{
		myTransform = GetComponent<Transform>();
	}
	
	private void Update () 
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch(Input.GetTouch(0).phase)

			{
				case TouchPhase.Began:	
					if(VerifyTouch(touch))
						CanRotate = true;
				break;

				case TouchPhase.Moved:	
					if(CanRotate)
						RotateObject(touch);
				break;
				case TouchPhase.Ended:	
					CanRotate = false;
				break;

			}
 
		}

	}
	
	private bool VerifyTouch(Touch touch)
	{
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
        	RaycastHit hit ;
		
		if(collider == null)
			gameObject.AddComponent(typeof(BoxCollider));
		
       		if (Physics.Raycast (ray, out hit)) 
		{
			if(hit.collider.gameObject == this.gameObject)
				return true;
		}
		return false;
	}
	
	private void RotateObject(Touch touch)
	{
		myTransform.Rotate(new Vector3(touch.deltaPosition.y, -touch.deltaPosition.x,0)*speed, Space.World);
	}
}