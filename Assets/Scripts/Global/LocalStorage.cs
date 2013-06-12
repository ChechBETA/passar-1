using UnityEngine;
using System.Collections;

public class LocalStorage : MonoBehaviour 
{
	private const string APP_PROJECTS_INFO = "appProjectsInfo";
	
	public static string AppProjectsInfo
	{
		get
		{
			return PlayerPrefs.GetString(APP_PROJECTS_INFO,string.Empty);
		}
		set
		{
			if(value != string.Empty)
				PlayerPrefs.SetString(APP_PROJECTS_INFO,value);
		}
	}	
}
