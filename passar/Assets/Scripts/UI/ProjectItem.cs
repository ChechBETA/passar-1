using UnityEngine;
using System.Collections;

public class ProjectItem : UIListItemContainer {
	
	public SpriteText title;
	public SpriteText subtitle;
	public SpriteText description;
	public SimpleSprite background;
	public SimpleSprite thumbnail;
	
	public Texture2D Thumbnail
	{
		get
		{
			return (Texture2D)thumbnail.renderer.material.mainTexture;
		}
		set
		{
			thumbnail.SetTexture(value);
			thumbnail.UpdateUVs();
		}
	}
	
	public void Hide (bool tf)
	{
		title.Hide(tf);
		subtitle.Hide(tf);
		description.Hide(tf);
		background.Hide(tf);
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0,0.6F,0.8F,1F);
        Gizmos.DrawWireCube(transform.localPosition, new Vector3(480,135,1));
	}
}
