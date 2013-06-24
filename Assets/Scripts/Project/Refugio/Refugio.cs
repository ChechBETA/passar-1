using UnityEngine;
using System.Collections;

public class Refugio : MonoBehaviour {
	
	public ProjectManager project;
	public GameObject localizacion;
	public GameObject externo;
	
	private void Start () 
	{
		int currentProject = AppManager.Instance.CurrentProject.id;
		project.preloader.gameObject.SetActive(false);
		
		switch(currentProject)
		{
			case 16:
				project.logo.Hide(false);
				localizacion.SetActive(true);
			break;
			
			case 17:
				project.logo.Hide(true);
				externo.SetActive(true);
			break;
		}
	}
	
	
}
