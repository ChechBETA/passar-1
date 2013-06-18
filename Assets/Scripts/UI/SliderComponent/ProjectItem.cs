using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AssetBundleFile
{
	public string assetBundleURL;
	public int assetBundleVersion;
	public Vector3 scaleAsset;
}

[System.Serializable]
public class ProjectItemDescriptor
{
	public string name;
	public int id;
	public string subtitle;
	public string sortDescription;
	public string description;
	public string thumbnailURL;
	public Level level;
	public List<AssetBundleFile> assetBundles;
}

public class ProjectItem : UIListItemContainer {
	
	public SpriteText title;
	public SpriteText subtitle;
	public SpriteText description;
	public SimpleSprite background;
	public SimpleSprite thumbnail;
	public UIPassarButton button;
	public GameObject preloader;
	public ProjectItemDescriptor project;
	
	public Action<ProjectItemDescriptor> onPressDelegate;
	
	public override void Start ()
	{
		base.Start ();
		button.onPressDelegate = OnSelectCurrentProject;
	}
	
	public string Title
	{
		get{ return title.Text; }
		set{ title.Text = value; }
	}
	
	public string SubTitle
	{
		get{ return subtitle.Text; }
		set{ subtitle.Text = value; }
	}
	
	public string Description
	{
		get{ return description.Text; }
		set{ description.Text = value; }
	}
	
	public void SetThumbnail(Texture2D image)
	{
		Rect uvImageConfig;
		float xDimensions = thumbnail.pixelDimensions.x;
		float yDimensions = thumbnail.pixelDimensions.y;
		
		if(GlobalParams.Instance.IsApplicationDinamic)
			uvImageConfig = new Rect(0F,0F,xDimensions,yDimensions); 
		else
			uvImageConfig = new Rect(0F,0F,256F,256F); 
		
		thumbnail.SetTexture(image);
		thumbnail.UpdateUVs();
		thumbnail.SetUVsFromPixelCoords(uvImageConfig);
		preloader.SetActive(false);
	}
	
	private void OnSelectCurrentProject()
	{
		if(onPressDelegate != null)
			onPressDelegate(project);
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0,0.6F,0.8F,1F);
        Gizmos.DrawWireCube(transform.localPosition, new Vector3(355,110,1));
	}
}