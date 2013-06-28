using UnityEngine;
using System.Collections;

public class Sumapaz : MonoBehaviour {
	
	public ProjectManager project;
	public GameObject localizacion;
	public GameObject casabambu;
	public GameObject casal;
	public GameObject video;
	
	// Use this for initialization
	private void Start () 
	{
		int currentProject = AppManager.Instance.CurrentProject.id;
		project.preloader.gameObject.SetActive(false);
	
		switch(currentProject)
		{
			case 12:
				project.logo.Hide(false);
				localizacion.SetActive(true);
				break;
			
			case 13:
				project.logo.Hide(true);
				casabambu.SetActive(true);
				break;
			
			case 14:
				project.logo.Hide(true);
				casal.SetActive(true);
				break;
			default:
				video.SetActive(true);	
				break;
		}
	}

}
