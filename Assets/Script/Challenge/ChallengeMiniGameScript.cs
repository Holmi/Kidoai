using UnityEngine;
using System.Collections;

public class ChallengeMiniGameScript : MonoBehaviour {

	private int p1Count = 0;
	private int p2Count = 0;

	public GameObject bar;
	public Canvas canvas;

	public float limitTime = 10f;

	// Use this for initialization
	void Start() {
		if (bar == null)
			bar = GameObject.Find("Bar");
	}

	// Update is called once per frame
	void Update() {
		canvas.GetComponent<Canvas>().guiText.text = limitTime.ToString();
		if (limitTime <= 0f || p1Count >= 100 || p2Count >= 100) {

		} else {
			limitTime -= Time.deltaTime;
			if (Input.GetButtonDown("Player1 Dicision") || Input.GetButtonDown("Player1 Left")
				|| Input.GetButtonDown("Player1 Right") || Input.GetButtonDown("Player1 Cancel")) {
				p1Count++;
				bar.renderer.material.mainTextureOffset += new Vector2(0.01f, 0);
			}
			if (Input.GetButtonDown("Player2 Dicision") || Input.GetButtonDown("Player2 Left")
				|| Input.GetButtonDown("Player2 Right") || Input.GetButtonDown("Player2 Cancel")) {
				p2Count++;
				bar.renderer.material.mainTextureOffset -= new Vector2(0.01f, 0);
			}
		}
	}
}
