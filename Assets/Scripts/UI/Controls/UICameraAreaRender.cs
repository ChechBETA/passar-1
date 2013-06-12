using UnityEngine;
using System.Collections;

public class UICameraAreaRender : MonoBehaviour {
	
	private void OnDrawGizmos () 
	{
        Gizmos.color = new Color(0,0.6F,0.5F,1F);
        Gizmos.DrawWireCube(transform.position, new Vector3(768F,1024F,1));
		
		Gizmos.color = new Color(0,0.6F,0.5F,0.5F);
        Gizmos.DrawWireCube(transform.position, new Vector3(560F,850F,1));
    }
}
