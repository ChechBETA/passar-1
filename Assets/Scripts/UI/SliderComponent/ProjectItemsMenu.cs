using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectItemsMenu : MonoBehaviour , IUIDisplayObject
{
	public UIScrollList list;
	public ProjectItem item;
	public UIPanel panel;
	public UIPassarButton buttonClose;
	public UIPassarButton buttonBack;
	public List<ProjectItemDescriptor> projectItems;
	public event Action onClose;
	private ProjectDescriptor project;

	
	private void Start()
	{
		buttonClose.onPressDelegate = Close;
		buttonBack.onPressDelegate = Close;
	}
	
	public void Init()
	{
		list.ClearList(true);
		this.project = AppManager.Instance.GetCurrentProject();
		this.projectItems = project.items;
		
		foreach(ProjectItemDescriptor projectItem in this.projectItems)
		{
			ProjectItem itemObj = Instantiate(item) as ProjectItem;
			itemObj.Title = projectItem.name;
			itemObj.SubTitle = projectItem.subtitle;
			itemObj.Description = projectItem.description;
			itemObj.onPressDelegate = OnSelectProduct;
			itemObj.project = projectItem;
			list.AddItem(itemObj);
		}
		
		list.transform.localPosition = UpdatePositionList();
		list.UpdateCamera();
		Show();
		
		if(GlobalParams.Instance.HasInternetConnection)
			StartCoroutine(DownloadImages());
		else
			GetImagesFromCache();
	}
	
	private Vector3 UpdatePositionList()
	{
		Vector3 currentPos = list.transform.localPosition;
		switch(list.Count)
		{
			case 4:
				return new Vector3(currentPos.x , -20F , currentPos.z);
			
			default:
				return new Vector3(currentPos.x , 30F , currentPos.z);
		}
	}
	
	private void GetImagesFromCache()
	{
		int i = 0;
		ServerImages assetImages = project.imageResources;
		foreach(ProjectItemDescriptor projectItem in this.project.items)
		{
			ProjectItem item = (ProjectItem)list.GetItem(i);
			Texture2D texture = assetImages.GetImageByURL( projectItem.thumbnailURL );
			item.SetThumbnail( texture );
			i++;
		}
	}
	
	private IEnumerator DownloadImages()
	{
		for(int i = 0; i < list.Count; i++)
		{
			ProjectItem item = (ProjectItem)list.GetItem(i);
		
			if(item == null)
				continue;

			string url = GlobalParams.Instance.DomainPath + projectItems[i].thumbnailURL;
			
			WWW imageAsset = new WWW(url);
			yield return imageAsset;
		
			if(imageAsset.error == null && item != null)
			{
				item.SetThumbnail(imageAsset.texture);
			}
		}
	}
	
	private void OnSelectProduct(ProjectItemDescriptor project)
	{
		AppManager.Instance.CurrentProject = project;
		
#if UNITY_IPHONE
		/*if(project.loadVideo)
		{
			string pathVideo = GlobalParams.Instance.VideosPath + project.videoURL;
			Debug.Log("APP URL PATH ===== " + GlobalParams.Instance.AppPath);
			Debug.Log("THIS IS THE PATH VIDEO ===== " + pathVideo);
			Handheld.PlayFullScreenMovie (project.videoURL , Color.black, FullScreenMovieControlMode.Full);
			return;
		}*/
#endif
		if(GlobalParams.Instance.IsApplicationDinamic)
			LevelLoader.Instance.LoadScene(Level.Project);
		else
			LevelLoader.Instance.LoadScene(project.level);
	}
	
	public void Hide ()
	{
		panel.Dismiss();
	}
	
	public void Show ()
	{
		panel.BringIn();
	}
	
	private void Close()
	{
		Hide(); 
		if(onClose != null)
			onClose();
	}
}
