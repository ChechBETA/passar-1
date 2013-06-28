using UnityEngine;
using System.Collections;

public class ScaleControlSlider : MonoBehaviour 
{	
	public UISlider slider;
	public UIPassarButton buttonZoomIn;
	public UIPassarButton buttonZoomOut;
	public Transform objetcToScale;
	
	public float scaleFactor = 1.0F;
	public float speedScale = 0.1F;
	
	private const float MAX_SCALE_FACTOR = 1F;
	private const float MIN_SCALE_FACTOR = 0F;
	private Vector3 currentObjectScale;
	private float defaultScale = 0.25F;
	
	public float DefaultScale
	{
		get
		{
			return defaultScale;
		}
	}
	
	
	private void Awake () 
	{
		slider.defaultValue = DefaultScale;
		slider.SetValueChangedDelegate(UpdateScale);
		
		buttonZoomIn.AddInputDelegate(UpdateZoomIn);
		buttonZoomOut.AddInputDelegate(UpdateZoomOut);
		
		currentObjectScale = objetcToScale.localScale;
	}
	
	private void UpdateScale (IUIObject sliderControl) 
	{
		float scaleFactorUpdate = slider.Value + scaleFactor; 
		objetcToScale.localScale = currentObjectScale * scaleFactorUpdate;
	}
	
	private void UpdateZoomIn(ref POINTER_INFO ptr)
	{
		if(ptr.evt == POINTER_INFO.INPUT_EVENT.TAP && slider.Value < MAX_SCALE_FACTOR)
		{
			slider.Value+= speedScale;
			if(slider.Value > MAX_SCALE_FACTOR)
				slider.Value = MAX_SCALE_FACTOR;
		}
	}
	
	private void UpdateZoomOut(ref POINTER_INFO ptr)
	{
		if(ptr.evt == POINTER_INFO.INPUT_EVENT.TAP && slider.Value > MIN_SCALE_FACTOR)
		{
			slider.Value-= speedScale;
			if(slider.Value < MIN_SCALE_FACTOR)
				slider.Value = MIN_SCALE_FACTOR;
		}
	}
}
