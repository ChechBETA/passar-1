using UnityEngine;
using System.Collections;

public class Macadamia : MonoBehaviour 
{
	public ProjectManager project;
	public GameObject localization;
	public GameObject extCiruelos;
	public GameObject intCiruelos;
	public GameObject extAvellanos;
	public GameObject intAvellanos;
	
	private void Start () 
	{
		int currentProject = AppManager.Instance.CurrentProject.id;
		project.preloader.gameObject.SetActive(false);
		switch( currentProject )
		{
			case 1:
				localization.SetActive(true);
				project.logo.Hide(false);
			break;
			
			case 2:
				extCiruelos.SetActive(true);
				project.logo.Hide(true);
			break;
			
			case 3:
				intCiruelos.SetActive(true);
				intAvellanos.GetComponent<InteriorAvellana>().button.gameObject.SetActive(false);
				project.logo.Hide(true);
			break;
			
			case 4:
				extAvellanos.SetActive(true);
				project.logo.Hide(true);
			break;
			
			case 5:
				intAvellanos.SetActive(true);
				intCiruelos.GetComponent<InteriorCiruelos>().button.gameObject.SetActive(false);
				project.logo.Hide(true);
			break;
		}
		
	}
	
	
}
