using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ServerImage
{
	public string name;
	public Texture2D texture;
}

public class ServerImages : MonoBehaviour 
{
	public ServerImage portrait;
	public ServerImage smallLogo;
	public List<ServerImage> images;
	
	public Texture2D GetImageByURL(string url)
	{
		foreach(ServerImage image in images)
		{
			if(image.name == url)
				return image.texture;
		}
		
		return null;
	}
}
