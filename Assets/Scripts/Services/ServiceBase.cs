using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServiceBase
{
	private string getProductsInfo = "GetProjectsInfoByUserID";

	public event Action onServiceComplete;
	public event Action<string> onServiceError;
	public List<ProjectDescriptor> projects;
	private MonoBehaviour caller;
	
	public ServiceBase(MonoBehaviour caller)
	{
		this.caller = caller;
	}
	
	public void GetProjectsInfoByUserID( int userid)
	{
		ArrayList data = new ArrayList();
		data.Add(userid);

		this.caller.StartCoroutine(ExecuteService(this.getProductsInfo , data));
	}
			
	private IEnumerator ExecuteService( string functionNameToExec , ArrayList data)
	{
		string urlService = GlobalParams.Instance.DomainPath + GlobalParams.Instance.connectionParameters.ServicePath;
		string functionName = "f=" + functionNameToExec;
		string parameters = "p=" + MiniJSON.jsonEncode(data);
		string urlData = urlService + "?" + functionName + "&" + parameters;
		
		Debug.Log(urlData);
		WWW service = new WWW(urlData);
		yield return service;
		
		
		if(service.error == null)
		{
			LocalStorage.AppProjectsInfo = service.text;
			Debug.Log(LocalStorage.AppProjectsInfo);	
			
			ParseInfoToProjectDescriptor(service.text);
			if(onServiceComplete != null)
				onServiceComplete();
		}
		else
		{
			if(onServiceError != null)
				onServiceError(service.error);
		}
	}
	
	public void ParseInfoToProjectDescriptor( string result )
	{
		ArrayList projectsResult = MiniJSON.jsonDecode(result) as ArrayList;
		this.projects = new List<ProjectDescriptor>();
		
		foreach(Hashtable project in projectsResult)
		{
			ProjectDescriptor projectDescriptor = new ProjectDescriptor();
			projectDescriptor.id = int.Parse(project["id"].ToString());
			projectDescriptor.name = project["name"].ToString();
			projectDescriptor.portraitURL = project["portrait_url"].ToString();
			projectDescriptor.smallLogoURL =  project["small_logo_url"].ToString();
			projectDescriptor.items = GetProjectsItems( project["projects"] as ArrayList );
			projectDescriptor.imagesBundle = GetImagesCacheAsset(project["images"] as Hashtable);
			this.projects.Add(projectDescriptor);
		}
	}
	
	private AssetImages GetImagesCacheAsset(Hashtable info)
	{
		string url = info["url"].ToString();
		int version = int.Parse(info["version"].ToString());
		this.caller.StartCoroutine(DownloadImagesAssets(url,version));
		
		AssetImages images = new AssetImages();
		images.url = url;
		images.version = version;
		
		return images;
	}
	
	private IEnumerator DownloadImagesAssets(string url,int version)
	{
		yield return AssetBundleManager.DownloadAssetBundle(url,version);
	}
	
	private List<ProjectItemDescriptor> GetProjectsItems(ArrayList projetcsItems)
	{
		List<ProjectItemDescriptor> items = new List<ProjectItemDescriptor>();
		
		foreach(Hashtable item in projetcsItems)
		{
			ProjectItemDescriptor itemDescriptor = new ProjectItemDescriptor();
			itemDescriptor.name = item["name"].ToString();
			itemDescriptor.id = int.Parse(item["id"].ToString());
			itemDescriptor.subtitle = item["subtitle"].ToString();
			itemDescriptor.sortDescription = item["sortDescription"].ToString();
			itemDescriptor.description = item["description"].ToString();
			itemDescriptor.thumbnailURL = item["thumbnailURL"].ToString();
			itemDescriptor.assetBundles =  GetAssetBundlesFiles( item["assetBundles"] as ArrayList );
			items.Add(itemDescriptor);
		}
		
		return items;
	}
	
	private List<AssetBundleFile> GetAssetBundlesFiles(ArrayList assetBundlesFiles)
	{
		List<AssetBundleFile> assetBundles = new List<AssetBundleFile>();
		
		foreach(Hashtable assetBundleFile in assetBundlesFiles)
		{
			AssetBundleFile assetContainer = new AssetBundleFile();
			string assetBundleVersion = assetBundleFile["assetBundleVersion"].ToString();
			string assetBundleScale = assetBundleFile["scaleAsset"].ToString();
			
			assetContainer.assetBundleURL = assetBundleFile["assetBundle"].ToString();
			assetContainer.assetBundleVersion = int.Parse( assetBundleVersion );
			assetContainer.scaleAsset = ParseScale( assetBundleScale );
			assetBundles.Add( assetContainer );
		}
		
		return assetBundles;
	}
	
	private Vector3 ParseScale(string scaleFactor)
	{
		string [] values = scaleFactor.Split(',');
		if(values.Length > 0)
		{
			float xScale = float.Parse(values[0]);
			float yScale = float.Parse(values[1]);
			float zScale = float.Parse(values[2]);
			
			return new Vector3(xScale,yScale,zScale);				
		}
		return Vector3.one;		
	}
}
