using UnityEngine;
using System.Collections;

public class EZScreenPlacementHelper : MonoBehaviour 
{
	public EZScreenPlacement screenPlacement;
	
	private void Awake()
	{
		screenPlacement.renderCamera = UIManager.instance.rayCamera;
	}
	
}
