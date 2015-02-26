using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadText : MonoBehaviour {

	Text text;
	private string[] txtArray;


	// Use this for initialization
	void Start () {
		//テキストを格納
		TextAsset txt = Resources.Load("test") as TextAsset;
		//;でテキストを区切る
		txtArray = txt.text.Replace (";\n", ";").Split (";"[0]);
		text = GetComponent<Text> ();
		text.text = txt.text;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
