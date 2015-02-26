using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeMiniGameScript : MonoBehaviour {

	private int p1Count = 0;
	private int p2Count = 0;

	public GameObject bar;
	public Text text;

	public float limitTime = 10f;

	public Text winnerText;

	// Use this for initialization
	void Start() {
		if (bar == null)
			bar = GameObject.Find("Bar");
	}

	// Update is called once per frame
	void Update() {
		text.text = Floor(limitTime).ToString();

		if (limitTime <= 0f || p1Count >= 100 || p2Count >= 100) {
			limitTime = 0f;
			if (p1Count > p2Count) {
				winnerText.text = "プレイヤー1の勝ち！！";
				PlayerStatusModel.player1.Love += 10;
				PlayerStatusModel.player1.MaxStamina += PlayerStatusModel.player1.MaxStamina / 10;
			} else if (p1Count < p2Count) {
				winnerText.text = "プレイヤー２の勝ち！！";
				PlayerStatusModel.player2.Love += 10;
				PlayerStatusModel.player2.MaxStamina += PlayerStatusModel.player2.MaxStamina / 10;
			} else
				winnerText.text = "引き分け";
			StartCoroutine("ChangeScene");
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

	float Floor(float f) {
		int returnValue = (int) (f * 100);
		return returnValue / 100f;
	}

	IEnumerator ChangeScene() {
		yield return new WaitForSeconds(2f);
		Application.LoadLevel("Select Phase");
	}
}
