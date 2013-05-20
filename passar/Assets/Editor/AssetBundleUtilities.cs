using UnityEngine;
using UnityEditor;

public class ExportAssetBundles
{
	const string EXTENSION = "passar";
	
	[MenuItem("passar/Asset Bundles/Convert Selected Prefab To AssetBundle")]
    static void ExportSingleResource ()
	{
		// Set Target Folder
		string path = EditorUtility.SaveFilePanel ("Create new Asset Bundle", "", Selection.activeObject.name, EXTENSION );
		
		// Check if path was define
		if (path.Length != 0 && AssetDatabase.Contains(Selection.activeObject))
		{
			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);
			
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
			
			Selection.objects = selection;
		}
    }

	[MenuItem("passar/Asset Bundles/Convert All Prefabs In Folder To Assetbundles")]
    static void ParserAllPrefabs ()
	{
		// Get Selected Folder Path
		string selectedFolder = AssetDatabase.GetAssetPath(Selection.activeObject);

		// Get prefabs folder path
		string locatePath = Application.dataPath.Replace("Assets","") + selectedFolder;

		// Set Target Folder
		string savePath = EditorUtility.SaveFolderPanel("Save Assetbundles To Folder", "//..", "");
		
		// Cancel actions if there is no target
		if (savePath == null || savePath == "")
		{
			Debug.Log ("No Target Folder Selected");
			return;
		}
		
		GetFilesPaths(locatePath, savePath);
    }
	
	static public string[] GetFilesPaths(string folderPath, string savingPath)
	{
		string [] directories = System.IO.Directory.GetDirectories(folderPath);
		if (directories.Length > 1)
		{
			foreach(string directoryFiles in directories)
			{
				if (directoryFiles.EndsWith("/.svn") || directoryFiles.EndsWith("/prop-base") || directoryFiles.EndsWith("/props") || directoryFiles.EndsWith("/text-base") || directoryFiles.EndsWith("/tmp"))
					continue;
				
				string savePath =  savingPath +"/"+ directoryFiles.Split('/')[directoryFiles.Split('/').Length - 1];

	            // Try to create the directory.
	            System.IO.Directory.CreateDirectory(savePath);

				// Get Child Archives
				GetFilesPaths(directoryFiles, savePath);
			}
		}
		else
		{
			string[] files = System.IO.Directory.GetFiles(folderPath);

			foreach(string filePath in files)
			{
				if (filePath.EndsWith(".prefab") && savingPath.Length != 0)
				{
					string fileSavePath =  savingPath +"/"+ filePath.Split('/')[filePath.Split('/').Length - 1];
					string assetPath = "Assets/" + filePath.Replace(Application.dataPath + "/","");
					
					BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath(assetPath), AssetDatabase.LoadAllAssetsAtPath(assetPath), fileSavePath.Replace(".prefab", EXTENSION),
					                               BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.iPhone);
			
				}
			}

			return files;
		}
		
		return null;
	}
}