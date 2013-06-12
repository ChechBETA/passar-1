using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class AssetBundleManager
{   
	private static Dictionary<string, AssetBundleRef> dictAssetBundleRefs;

	static AssetBundleManager ()
	{
		dictAssetBundleRefs = new Dictionary<string, AssetBundleRef> ();
	}
   
	private class AssetBundleRef
	{
		public AssetBundle assetBundle = null;
		public int version;
		public string url;

		public AssetBundleRef (string strUrlIn, int intVersionIn)
		{
			url = strUrlIn;
			version = intVersionIn;
		}
	}
   
	public static AssetBundle GetAssetBundle (string url, int version)
	{
		string assetPath = GlobalParams.Instance.AssetBundlePath;
		string keyName = assetPath + url + version.ToString ();
       
		AssetBundleRef abRef;
		if (dictAssetBundleRefs.TryGetValue (keyName, out abRef))
			return abRef.assetBundle;
		else
			return null;
	}

	public static IEnumerator DownloadAssetBundle (string url, int version)
	{
		string assetPath = GlobalParams.Instance.AssetBundlePath;
		string keyName = assetPath + url + version.ToString ();
		
		if (dictAssetBundleRefs.ContainsKey (keyName))
		{
			yield return null;
		}
		else 
		{
			Debug.Log(assetPath + url);
			WWW assetBundle = WWW.LoadFromCacheOrDownload (assetPath + url, version);
			
			yield return assetBundle;
			if (assetBundle.error != null)
			{
				Debug.Log(assetBundle.error);
				throw new Exception ("WWW download:" + assetBundle.error);
			}
			else
			{
				Debug.Log("DOWNLOADED ASSETBUNDLE COMPLETE ====> " + assetBundle.assetBundle.mainAsset.name);
				AssetBundleRef abRef = new AssetBundleRef (assetPath + url, version);
				abRef.assetBundle = assetBundle.assetBundle;
				dictAssetBundleRefs.Add (keyName, abRef);
			}
		}
	}
	
	public static void Unload (string url, int version)
	{
		string keyName = url + version.ToString ();
		bool unloadAllObjects = true;
		
		AssetBundleRef abRef;
		if (dictAssetBundleRefs.TryGetValue (keyName, out abRef)) 
		{
			abRef.assetBundle.Unload (unloadAllObjects);
			abRef.assetBundle = null;
			dictAssetBundleRefs.Remove (keyName);
		}
	}
}