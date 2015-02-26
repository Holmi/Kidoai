using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadText : MonoBehaviour {

	Text text;
	public string[] txtArray;
	int n = 0;

	// Use this for initialization
	void Start () {
		string splitMark = "\n";
		//テキストを格納
		TextAsset txt = Resources.Load("test") as TextAsset;
		//\nでテキストを区切る
		txtArray = txt.text.Split (splitMark[0]);
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = txtArray[n];
		if(Input.GetMouseButtonDown(0)) n++;
	}
}
