using UnityEngine;
using System.Collections;

public class ProjectItemsMenu : MonoBehaviour
{
	public UIScrollList list;
	public ProjectItem item;
	
	private void Start()
	{
		list.renderCamera = UIManager.instance.rayCamera;
		for (int i = 0; i < 10; i++)
		{
			ProjectItem itemGO = Instantiate(item) as ProjectItem;
			list.AddItem(itemGO);
		}
	}

}
