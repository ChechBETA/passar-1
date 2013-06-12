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
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
	
	private void Start()
	{
		projectsService = new ServiceBase(this);
		
		if(GlobalParams.Instance.HasInternetConnection)
		{
			projectsService.onServiceComplete += OnGetDataComplete;
			projectsService.onServiceError += OnGetDataFailed;
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
	
	private void OnDestroy()
	{
		projectsService.onServiceComplete -= OnGetDataComplete;
		projectsService.onServiceError -= OnGetDataFailed;
	}
	
	private void OnGetDataComplete()
	{
		projects = projectsService.projects;
	}
	
	private void OnGetDataFailed(string error)
	{
		
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
