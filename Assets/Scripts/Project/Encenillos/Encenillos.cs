using UnityEngine;
using System.Collections;

public class Encenillos : MonoBehaviour {
	
	public ProjectManager project;
	public GameObject localizacion;
	public GameObject sedesocial;
	public GameObject sorrento;
	public GameObject praga;
	public GameObject video;
	
	
	private void Start () 
	{
		int currentProject = AppManager.Instance.CurrentProject.id;
		project.preloader.gameObject.SetActive(false);
		
		switch(currentProject)
		{
			case 7:
				localizacion.SetActive(true);
				project.logo.Hide(false);
			break;
			
			case 8:
				sedesocial.SetActive(true);
				project.logo.Hide(true);
			break;
			
			case 9:
				sorrento.SetActive(true);
				project.logo.Hide(true);
			break;
			
			case 10:
				praga.SetActive(true);
				project.logo.Hide(true);
			break;
			
			default:
				video.SetActive(true);
			break;
		}
	}
	
}
