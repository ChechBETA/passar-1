using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public GameObject targetPoint;
	
	private void Start()
	{
		GameObject image = Instantiate(targetPoint) as GameObject;
		transform.parent = image.transform;
	}
	
}
