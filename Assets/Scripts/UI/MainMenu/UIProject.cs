using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetImages
{
	public string url;
	public int version;
}

[System.Serializable]
public class ProjectDescriptor
{
	public string name;
	public int id;
	public string portraitURL;
	public string smallLogoURL;
	public ServerImages imageResources;
	public AssetImages imagesBundle;
	public List<ProjectItemDescriptor> items;
}

public class UIProject : UIListItemContainer
{
	public int projectId;
	public SimpleSprite portrait;
	public UIPassarButton button;
	public GameObject preloader;
	public Action onPressDelegate;
	
	public string TextureURL
	{
		get; 
		set;
	}
	
	public void SetPortrait(Texture2D image)
	{
		Rect configUVS = SetConfigUVS( image );			
		portrait.SetTexture(image);
		portrait.UpdateUVs();
		portrait.SetUVsFromPixelCoords(configUVS);
		preloader.SetActive(false);
	}
	
	private Rect SetConfigUVS( Texture2D image )
	{
		return new Rect(0,0,image.width,image.height);
	}
	
 	public override void Start ()
	{
		base.Start ();
		button.onPressDelegate = OnPressDelegate;
	}
	
	public override void OnDestroy ()
	{
		base.OnDestroy ();
		button.onPressDelegate = OnPressDelegate;
	}
	
	private void OnPressDelegate()
	{
		AppManager.Instance.CurrentProjectID = projectId;
		
		if(onPressDelegate != null)
			onPressDelegate();
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.localPosition,new Vector3(350F,560F,1F));
	}
}