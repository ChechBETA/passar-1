using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppManager : MonoBehaviour 
{
	public static AppManager Instance
	{
		get;
		private set;
	}

	public int CurrentProjectID
	{
		get;
		set;
	}
	
	public ProjectItemDescriptor CurrentProject
	{
		get;
		set;	
	}
	
	public List<ProjectDescriptor> projects;
	private ServiceBase projectsService;
	private StaticDataManager staticDataManager;
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
	
	private void Start()
	{
		if( GlobalParams.Instance.IsApplicationDinamic )
			GetDinamicData();
		else
			GetStaticData();
	}
	
	private void OnDestroy()
	{
		if( !GlobalParams.Instance.IsApplicationDinamic )
			return;
		
		projectsService.onServiceComplete -= OnGetDataComplete;
	}
	
	private void GetStaticData()
	{
		staticDataManager = FindObjectOfType(typeof(StaticDataManager)) as StaticDataManager;
		projects = staticDataManager.projects;
		
		foreach(ProjectDescriptor project in projects)
		{
			GameObject imagesResources = Instantiate(project.imageResources.gameObject) as GameObject;
			imagesResources.transform.parent = transform;
		}
	}
	
	private void GetDinamicData()
	{
		projectsService = new ServiceBase(this);
		
		if(GlobalParams.Instance.HasInternetConnection)
		{
			projectsService.onServiceComplete += OnGetDataComplete;
			projectsService.GetProjectsInfoByUserID( GlobalParams.Instance.connectionParameters.UserID );
		}
		else if(!string.IsNullOrEmpty( LocalStorage.AppProjectsInfo ) )
		{
			projectsService.ParseInfoToProjectDescriptor( LocalStorage.AppProjectsInfo );
			projects = projectsService.projects;
		}
		else 
			Debug.Log("You don't have connection internet");
	}
	
	private void OnGetDataComplete()
	{
		projects = projectsService.projects;
	}

	public ProjectDescriptor GetCurrentProject()
	{
		ProjectDescriptor currentProject = null;
		
		foreach(ProjectDescriptor project in projects)
		{
			if(project.id == CurrentProjectID)
				return project;
		}
		
		return currentProject;
	}
}
