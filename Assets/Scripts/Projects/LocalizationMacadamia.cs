using UnityEngine;
using System.Collections;

public class LocalizationMacadamia : MonoBehaviour 
{
	public Animation animationComponent;
	public Animation [] indicator;
	
	private bool isAnimate = false;
	private Vector3 originalSize;
	private Transform coneTransform;
	private float growScale = 0.55F;
	private float speedScale = -0.02F;
	private Vector3 minSize;
	private bool hasBeenFinished;
	private float timeToEndedScaleAnimation;
	private float durationScaleAnimation = 3.0F;
	
	private void Start()
	{	
		coneTransform = animationComponent.transform;
		originalSize = coneTransform.localScale;
		minSize = originalSize * growScale;
		
	}
	
	private void Update()
	{
		if(!isAnimate)
			return;
		
		if(coneTransform.localScale.x <= minSize.x)
		{
			speedScale = -0.01F;
		}
		else if(coneTransform.localScale.x > originalSize.x)
		{
			speedScale = 0.01F;
		}
		
		coneTransform.localScale -= (Vector3.one * speedScale);
		
		if(timeToEndedScaleAnimation <= Time.realtimeSinceStartup)
		{
			coneTransform.localScale = originalSize;
			AnimationStarts();
		}
	}
	
	private void AnimationStarts()
	{
		isAnimate = false;
		animationComponent.Play();
	}
	
	private void OnShowPopUp()
	{
		if(!hasBeenFinished)
			return;
		
		isAnimate = false;
		coneTransform.localScale = originalSize;
	}
	
	private void Reset()
	{
		animationComponent.Play();
	}
	
	public void AnimateScaleCone()
	{
		timeToEndedScaleAnimation = Time.realtimeSinceStartup + durationScaleAnimation;
		isAnimate = true;
		hasBeenFinished = true;
	}
	
	public void AnimationIndicator(int index)
	{
		indicator[index].Play();
	}
}
