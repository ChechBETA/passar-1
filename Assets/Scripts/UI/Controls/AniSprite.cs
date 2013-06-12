using UnityEngine;
using System.Collections;

public class AniSprite : MonoBehaviour 
{
	private int index;
	private float time = 0;
	private float speed = 0.2f;

	private void AnimationSprite (int columnSize, int rowSize, int colFrameStart, int rowFrameStart,int  totalFrames,int framesPerSecond)
	{
		index  = (int)time * framesPerSecond;
		index = index % totalFrames;
		
		Vector2 size = new Vector2 ( 1.0f / columnSize, 1.0f / rowSize);											// scale for column and row size
		
		float columnX = index % columnSize;																		// u gets current x coordinate from column size
		float columnY = index / columnSize;																		// v gets current y coordinate by dividing by column size
		
		Vector2 offset = new Vector2 ((columnX + colFrameStart) * size.x,(1.0f - size.y) - (columnY + rowFrameStart) * size.y); // offset equals column and row
		
		renderer.material.mainTextureOffset = offset;													// texture offset for diffuse map
		renderer.material.mainTextureScale  = size;														// texture scale  for diffuse map 
	}
	
	public void Update () 
	{
		AnimationSprite(12,1,1,0,12,1);
		time += speed;
	}
}
