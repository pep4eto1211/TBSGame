using UnityEngine;
using System.Collections;

public class FieldButtonScript : MonoBehaviour
{

	public int x;
	public int y;
	
	public void doButtonAction()
	{
        GameplayScript.fieldButtonPress(x, y);	
	}

	// Use this for initialization
	void Start () {
	}

    private bool isPressed = false;

	// Update is called once per frame
	void Update () {
        
    }
}
