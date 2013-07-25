using UnityEngine;
using System.Collections;

public class ColorSelector : MonoBehaviour {
	/* Unused Script:
	 * Originally both the pose and the color would be randomized
	 * Such that you would need to have the same color and pose
	 * Making this a game for mouse and keyboard. However, I couldn't
	 * Figure out how to call textures through script, so selecting a color became moot
	 * */
	
	public Transform bracelet;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	void OnMouseUpAsButton(){
	
		bracelet.renderer.material.color = renderer.material.color;
		
	}
}
