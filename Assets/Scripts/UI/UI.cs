using UnityEngine;
using System.Collections;

public enum MenuState
{
	Welcome = 0,
	ProjectList = 1,
	Contact = 2,
	Help = 3
}

public class UI : MonoBehaviour 
{
	
	public UIPanel header;
	public FooterBar footer;
	public UIWelcome welcomeMessage;
	public UIProjectsList projectList;
	public ProjectItemsMenu projectItems;
	public UIPanel contact;
	public UIPanel help;
	
	public static UI Instance
	{
		get;
		private set;
	}
	
	private MenuState menuState;
	public MenuState MenuState
	{
		get
		{
			return menuState;
		}
		set
		{
			menuState = value;
			HideAllContents();
			ShowContentByState();
		}
	}
	
	private void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
	
	private void Start()
	{
		projectList.Init();
		if(LevelLoader.Instance.HasStartsInMenu)
		{
			InitMenu();
			welcomeMessage.Hide();
		}
		else
		{
			header.Dismiss();
			projectList.Hide();
			welcomeMessage.onHideComplete = InitMenu;
		}
	}
	
	private void HideAllContents()
	{
		projectList.Hide();
		welcomeMessage.Hide();
		contact.Dismiss();
		help.Dismiss();
	}
	
	private void ShowContentByState()
	{
		switch(MenuState)
		{
			default:
				welcomeMessage.Show();
				break;
			case MenuState.ProjectList:
				projectList.Show();
				break;
			case MenuState.Contact:
				contact.BringIn();
				break;
			case MenuState.Help:
				help.BringIn();
				break;
		}
	}
	
	private void InitMenu()
	{
		welcomeMessage.onHideComplete = null;
		
		header.BringIn();
		footer.Show();
		projectList.Show();
		projectList.CreateProjectList();
		projectList.OnControlsChange();
		projectList.onProductSelect += OnProductSelect;
		projectItems.onClose += OnShowProjectList;
	}
	
	private void OnShowProjectList()
	{
		MenuState = MenuState.ProjectList;
	}
	
	private void OnProductSelect(ProjectDescriptor product)
	{
		projectItems.Init(product);
	}
	
	private void OnDestroy()
	{
		projectList.onProductSelect -= OnProductSelect;
		projectItems.onClose -= OnShowProjectList;
		Instance = null;
	}
}