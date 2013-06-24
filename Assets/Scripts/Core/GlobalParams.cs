using System.IO;
using UnityEngine;
using System.Collections;

public enum ServerType
{
	Local,
	Live
}

public enum ConnectionType
{
	Local = 0,
	Live = 1,
	None = 2
}
public enum ApplicationType
{
	Dinamical = 0,
	Static = 1
}

public class GlobalParams : MonoBehaviour {
	
	public ApplicationType applicationType = ApplicationType.Dinamical;
	public DebugConnectionParameters connectionParameters = new DebugConnectionParameters();
	
	public static GlobalParams Instance
	{
		get;
		private set;
	}
	
	public string AppPath
	{
		get{ return Application.dataPath; }
	}
	
	public string VideosPath
	{
		get{ return Application.streamingAssetsPath + "/Videos/"; }
	}
	
	public string DomainPath
	{
		get { return connectionParameters.DomainPath; }
	}

	public bool HasInternetConnection
	{
		get{ return connectionParameters.HasConnection; }
	}
	
	public bool IsApplicationDinamic
	{
		get { return (applicationType == ApplicationType.Dinamical); }
	}
	
	public string AssetBundlePath
	{
		get
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer)
				return connectionParameters.DomainPath + "ios/";
			return connectionParameters.DomainPath + "ios/";
		}
	}
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
}

[System.Serializable]
public class DebugConnectionParameters
{
	public ServerType domain = ServerType.Local;
	public ConnectionType connection = ConnectionType.Local;
	
	
	public string ServicePath
	{
		get{ return "services.php";}
	}
	
	public int UserID
	{
		get{ return 1;}
	}
	
	public string UserName
	{
		get
		{
			return "pedrogomezapp";
		}
	}
	
	public string DomainPath
	{
		get
		{
			switch(domain)
			{
				case ServerType.Local:
					return "http://localhost:8888/passar/"+UserName+"/";
				
				default:
					return "http://www.magnodev.com/"+UserName+"/";
			}
		}
	}
	
	public bool HasConnection
	{
		get
		{
			switch(connection)
			{
				case ConnectionType.Live:
					return (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork
						|| Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork);
				case ConnectionType.Local:
					return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
				default:
					return false;
			}
		}
	}
}