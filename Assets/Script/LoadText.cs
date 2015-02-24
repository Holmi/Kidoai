using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadText : MonoBehaviour {

	TextAsset text = Resources.Load ("test.txt") as TextAsset;

	// Use this for initialization
	void Start () {
		/*using (StreamReader reader = new StreamReader(Application.dataPath + "/test.txt")) {
			if (reader != null) {
				this.GetComponent<GUIText>().text = reader.ReadLine();
			}
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(txt.text);
	}
}
