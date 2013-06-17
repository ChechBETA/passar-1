using UnityEngine;
using System.Collections;

public class ProjectManager : MonoBehaviour 
{
	public SimpleSprite logo;
	public SimpleSprite logoProject;
	public SpriteText subtitle;
	public ImageTargetBehaviour target;
	public UIPanel preloader;
	private ProjectItemDescriptor project = new ProjectItemDescriptor();
	private ProjectDescriptor appProject;
	private int assetsAlreadyDownload = 0;
	
	public ProjectDescriptor CurrentProject
	{
		get{ return appProject; }
	}
	
	private void Awake()
	{
		project = AppManager.Instance.CurrentProject;
		
		SetInfo();

		DownloadAssetBundles();
	}
	
	private void DownloadAssetBundles()
	{
		foreach(AssetBundleFile assetBundleFile in project.assetBundles)
		{
			StartCoroutine(DownloadAssetBundle(assetBundleFile)); 
		}
	}
	
	private void SetInfo()
	{
		appProject = AppManager.Instance.GetCurrentProject();
		
		if(GlobalParams.Instance.HasInternetConnection)
			StartCoroutine(DownloadThumbnail(appProject.smallLogoURL));
		else
			GetThumbnailFromCache();
		
		if(project.sortDescription.Length <= 0)
			return;
		
		logo.Hide(true);
		subtitle.Text = project.name + " / " + project.sortDescription;
	}
	
	private void GetThumbnailFromCache()
	{
		Texture2D logoProject = appProject.imageResources.smallLogo.texture;
		SetTextureSmallLogo(logoProject);
	}
	
	private IEnumerator DownloadThumbnail(string url)
	{
		Debug.Log(GlobalParams.Instance.DomainPath + url);
		WWW texture = new WWW(GlobalParams.Instance.DomainPath + url);
		yield return texture;
		
		if(logoProject != null && texture.error == null)
		{ 
			SetTextureSmallLogo(texture.texture);
		}
	}
	
	private void SetTextureSmallLogo(Texture2D texture)
	{
		logoProject.SetTexture(texture);
		logoProject.UpdateUVs();
		
		if(GlobalParams.Instance.IsApplicationDinamic)
			logoProject.SetUVsFromPixelCoords(new Rect(2,2,texture.width-5,texture.height-5));
		else
		{
			logoProject.SetLowerLeftPixel(1,256);
			logoProject.SetPixelDimensions(256,256);
			logoProject.SetUVsFromPixelCoords(new Rect(2,2,256,116));
		}
	}
	
	private IEnumerator DownloadAssetBundle(AssetBundleFile assetBundleFile)
	{
		string url = assetBundleFile.assetBundleURL; 
		int version = assetBundleFile.assetBundleVersion;
		
		yield return StartCoroutine(AssetBundleManager.DownloadAssetBundle(url,version));
		AssetBundle assetBundle = AssetBundleManager.GetAssetBundle(url,version);
		
		if(assetBundle != null)
		{
			GameObject model = Instantiate(assetBundle.mainAsset) as GameObject;
			model.transform.parent = target.transform;
			model.transform.localScale = assetBundleFile.scaleAsset;
			
			CheckOnDownloadsComplete();
		}
	}
	
	private void CheckOnDownloadsComplete()
	{
		assetsAlreadyDownload++;
		if(assetsAlreadyDownload == project.assetBundles.Count)
			preloader.Dismiss();
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position,new Vector3(300F,60F,300F));
	}
}