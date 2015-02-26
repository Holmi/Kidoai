using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadText : MonoBehaviour {

	Text text;
	//テキストの各１行づつ格納する配列
	public string[] txtArray;
	int n = 0;

	// Use this for initialization
	void Start () {
		string splitMark = ";";
		//テキストを格納
		TextAsset txt = Resources.Load("test") as TextAsset;
		//\nで区切って配列に格納
		txtArray = txt.text.Split (splitMark[0]);
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = txtArray[n];
		//マウス押したら次の文へ
		if(Input.GetMouseButtonDown(0)) n++;
	}
}
