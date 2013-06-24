using UnityEngine;
using System.Collections;

public class EncenillosAnimation : MonoBehaviour 
{
	public Animation mainAnimation;
	public Animation [] indicator;
	
	public void AnimateSign( int index )
	{
		indicator[index].Play();
	}
	
	public void StartAgain()
	{
		mainAnimation.Play();
	}
}
