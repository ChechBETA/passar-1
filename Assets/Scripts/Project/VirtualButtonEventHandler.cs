using UnityEngine;
using System.Collections;


                                        
public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	public ScaleControlSlider sliderControl;
	public GameObject [] projects;
	
	private void Start()
	{
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);
        }
	}	
	/*Assets/Scripts/Project/VirtualButtonsController.cs(6,14): error CS0738: `VirtualButtonsController
		' does not implement interface member `IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour)' 
			and the best implementing candidate `VirtualButtonsController.OnButtonPressed(VirtualButtonBehaviour)
			' return type `void' does not match interface member return type `void'*/
	public void OnButtonPressed (VirtualButtonBehaviour vb)
	{
		Debug.Log("OnButtonPressed");
		
		sliderControl.slider.Value = sliderControl.DefaultScale;
		DisabledProjects();
		
		switch(vb.VirtualButtonName)
		{
			case "projecto1":
				projects[0].SetActive( true );
			break;
			
			case "projecto2":
				projects[1].SetActive( true );
			break;
			
			case "projecto3":
				projects[2].SetActive(true);
			break;
			
			case "projecto4":
				projects[3].SetActive(true);
			break;
			
			case "projecto5":
				projects[4].SetActive( true );
			break;
		}
	}
	
	public void OnButtonReleased (VirtualButtonBehaviour vb)
	{
		Debug.Log("OnButtonPressed");
	}
	
	private void DisabledProjects()
	{
		foreach(GameObject project in projects)
		{
			project.SetActive(false);
		}
	}
}
