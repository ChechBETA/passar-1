using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class CR_Rotate : MonoBehaviour {

	public Camera rotateCam;
	public BoxCollider boxCollider;
	public Transform newContainer;
	
	private float speed = 0.1f;
	private bool canRotate = false;
	private bool canStartRotate = false;
	private Transform myTransform;
	private Quaternion myInitialRotation;
	
	
	public bool CanRotate
	{
		get { return canRotate; } 
		private set { canRotate = value; } 
	}

	
	private void Start() 
	{
		myTransform = GetComponent<Transform>();
		myInitialRotation = myTransform.localRotation;
		//boxCollider.enabled = false;
		rotateCam.enabled = false;
		
	}
	
	private void Update () 
	{
		if(Application.isEditor)
		{
			if(Input.GetKeyDown(KeyCode.A))
			{
				if(!canStartRotate)
				{
					canStartRotate = true;
					ActiveRotateCam();
				}
			}
		}
		
		
#if UNITY_IPHONE
		//startRotate = (Input.touchCount >=4);
		
		/*if( !startRotate && !rotateCam.enabled)
			return;
		*/
		if(Input.touchCount >=4)
			ActiveRotateCam();
		
		if(Input.touchCount == 1 && canStartRotate)
		{
			Touch touch = Input.GetTouch(0);
			
			switch(Input.GetTouch(0).phase)
			{
				case TouchPhase.Began:	
					if(VerifyTouch(touch))
						CanRotate = true;
					Debug.Log(CanRotate);
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
#endif
			

	}
	
	private bool VerifyTouch(Touch touch)
	{
		Camera  cam = QCARManager.Instance.ARCamera;
		Ray ray = cam.ScreenPointToRay(touch.position);
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
		Debug.Log("starts Rotate");
		myTransform.Rotate(new Vector3(touch.deltaPosition.y, -touch.deltaPosition.x,0)*speed, Space.World);
	}
	
	private void ActiveRotateCam()
	{
		canStartRotate = !canStartRotate;
		if(!canRotate)
			myTransform.localRotation = myInitialRotation;
		
		return;
	}
	
	
}