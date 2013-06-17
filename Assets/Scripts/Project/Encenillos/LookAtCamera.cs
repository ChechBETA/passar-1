using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	
	public Transform camera;
	private Transform myTransform;
	
	private void Start()
	{
		myTransform = transform;	
	}
	
	private void Update () 
	{
		myTransform.LookAt(camera);
	}
}
