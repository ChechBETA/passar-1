using UnityEngine;
using System.Collections;

public class BuildingFloor : MonoBehaviour 
{
	public int floorNumber;
	public string floorName;
	private void Start()
	{
		InteriorCiruelos interiorCiruelos = FindObjectOfType(typeof(InteriorCiruelos)) as InteriorCiruelos;
		interiorCiruelos.AddChild(gameObject);
	}
	
	public void Show(bool tf)
	{
		gameObject.SetActive(tf);
	}
}
