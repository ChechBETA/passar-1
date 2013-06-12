using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIProjectsList : UIScrollList , IUIDisplayObject 
{
	public SimpleSprite dotItem;
	public UIScrollList dots;
	public UIPanel panel;
	public UIPassarButton buttonLeft;
	public UIPassarButton buttonRight;
	public UIProject itemProjectPrefab;
	public event Action <ProjectDescriptor> onProductSelect;
	
	private int currentItemNum = 0;
	private float scrollSpeed = 0.5F;
	private Color initColor = new Color(1F,1F,1F,0.25F);
	private Color selectColor = Color.white;
	
	public void Init()
	{
		buttonLeft.onPressDelegate += OnBack;
		buttonRight.onPressDelegate += OnNext;
		AddItemSnappedDelegate(OnSnapped);
	}
	
	private void OnDestroy()
	{
		buttonLeft.onPressDelegate -= OnBack;
		buttonRight.onPressDelegate -= OnNext;
	}
	
	public void Show ()
	{
		panel.BringIn();
	}
	
	public void Hide ()
	{
		panel.Dismiss();
	}
	
	private void OnBack()
	{
		if(currentItemNum <= 0)
			return;
		
		if(currentItemNum != ((UIProject)this.SnappedItem).Index)
			currentItemNum = ((UIProject)this.SnappedItem).Index;
		
		currentItemNum--;
		OnControlsChange();
		ScrollToItem(currentItemNum,scrollSpeed);
	}
	
	private void OnNext()
	{
		if(currentItemNum == this.Count - 1 )
			return;
		
		currentItemNum++;
		OnControlsChange();
		ScrollToItem(currentItemNum,scrollSpeed);
	}
	
	private void OnSnapped(IUIListObject item)
	{
		if(currentItemNum != ((UIProject)item).Index)
			currentItemNum = ((UIProject)item).Index;
		OnControlsChange();
	}
	
	public void CreateProjectList()
	{
		this.ClearList(true);
		
		float dotWidth = 0F;
		foreach(ProjectDescriptor project in AppManager.Instance.projects)
		{
			UIProject projectObject = Instantiate(itemProjectPrefab) as UIProject;
			projectObject.onPressDelegate = OnSelectProjectListProducts;
			projectObject.TextureURL = project.portraitURL;
			projectObject.projectId = project.id;
			this.AddItem(projectObject);
			
			SimpleSprite dot = Instantiate(dotItem) as SimpleSprite;
			dotWidth = dot.width;
			dots.AddItem(dot.gameObject);
		}
		
		float widthDotsContainer = (dotWidth + dots.itemSpacing) * dots.Count;
		dots.transform.Translate(Vector3.right * -(widthDotsContainer / 2) );
		
		if(GlobalParams.Instance.HasInternetConnection)
			StartCoroutine(DownloadImages());
		else
			StartCoroutine(GetImagesFromCache());
	}
	
	private IEnumerator GetImagesFromCache()
	{
		int i = 0;
		
		foreach(ProjectDescriptor project in AppManager.Instance.projects)
		{
			if(project.imageResources == null)
			{
				int version = project.imagesBundle.version;
				string url = project.imagesBundle.url;
				
				yield return StartCoroutine( AssetBundleManager.DownloadAssetBundle(url,version));
				AssetBundle asset = AssetBundleManager.GetAssetBundle(url,version);
				
				GameObject imagesBundles = Instantiate(asset.mainAsset) as GameObject;
				imagesBundles.transform.parent = AppManager.Instance.gameObject.transform;
				project.imageResources = imagesBundles.GetComponent<ServerImages>();
			}
			
			UIProject item = (UIProject)GetItem(i);
			item.SetPortrait(project.imageResources.portrait.texture);

			i++;
		}
	}
	
	public void OnControlsChange()
	{
		buttonLeft.Hide((Count <= 1));
		buttonRight.Hide((Count <= 1));

		if(Count > 0)
		{	
			buttonLeft.Hide(currentItemNum == 0);
			buttonRight.Hide(currentItemNum == (Count-1));
			OnChangeDots();
		}
	}
	
	private void OnChangeDots()
	{
		for(int i = 0; i < dots.Count ; i++)
		{
			SimpleSprite dot = dots.GetItem(i).gameObject.GetComponent<SimpleSprite>();
			dot.SetColor(initColor);
		}
		
		SimpleSprite currentDot = dots.GetItem(currentItemNum).gameObject.GetComponent<SimpleSprite>();
		currentDot.SetColor(selectColor);
	}
	
	private IEnumerator DownloadImages()
	{
		for(int i = 0; i < Count; i++)
		{
			UIProject item = (UIProject)GetItem(i);
	
			if(item == null)
				continue;
			
			string url = GlobalParams.Instance.DomainPath + item.TextureURL;
			
			WWW imageAsset = new WWW(url);
			yield return imageAsset;
			
			if(imageAsset.error == null && item != null)
			{
				item.SetPortrait(imageAsset.texture);
			}
			
		}
	}
	
	private void OnSelectProjectListProducts()
	{
		Hide();
		if(onProductSelect != null)
		{
			ProjectDescriptor currentProject = AppManager.Instance.GetCurrentProject();
			onProductSelect( currentProject );
		}
	}
}
