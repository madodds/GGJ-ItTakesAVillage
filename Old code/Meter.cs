using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Meter : MonoBehaviour {

	public float max;
	public float min;
	public float value;
	public Text meterText;

	// Use this for initialization
	void Start () {
		value = max / 2;
	}
	
	// Update is called once per frame
	void Update () {
		meterText.text = value.ToString();
	}
}
