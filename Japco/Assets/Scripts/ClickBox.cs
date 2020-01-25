using UnityEngine;

public class ClickBox : MonoBehaviour {

	public bool Clicked { get; private set; }

	private void OnMouseDown() {
		Clicked = true;
	}

}