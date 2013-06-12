using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClearCacheEditor : EditorWindow
{	
	[MenuItem ("Window/Clear Cache")]
	
    public static void Init () {
        EditorWindow.GetWindow (typeof (ClearCacheEditor));
    }
	
	private void OnGUI()
	{
		GUILayout.Space(10F);
		if(GUILayout.Button("CLEAR ASSET BUNDLES CACHE"))
		{
			if(EditorUtility.DisplayDialog("Asset Bundle Clear Cache","Are you sure that you wish clear cache?","Yes","No"))
				Caching.CleanCache();
		}
		
		GUILayout.Space(10F);
		if(GUILayout.Button("CLEAR PLAYER PREFS"))
		{
			if(EditorUtility.DisplayDialog("Clear Data","Are you sure that you wish clear app's data?","Yes","No"))
				PlayerPrefs.DeleteAll();
		}
	}
}