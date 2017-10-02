using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

	private Text myText;
	float elapsedTime = 0f;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		elapsedTime += Time.deltaTime;
		myText.text = "Time: " + Mathf.Round (elapsedTime);
	}
}
