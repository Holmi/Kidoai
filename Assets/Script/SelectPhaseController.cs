using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// 行動選択画面コントローラ
/// </summary>
public class SelectPhaseController : MonoBehaviour {

	// プレイヤーの識別子
	public enum PlayerId { Player1, Player2 };
	public PlayerId playerId = PlayerId.Player1;

	// 現在の選択階層
	private enum Phase { Phase1, Phase2 };
	private Phase currentPhase = Phase.Phase1;

	// 選択画面で現在選択中の選択肢
	private int currentSelected = 0;
	private int maxSelectPhase1 = 3;
	private int[] maxSelectPhase2 = {4, 3, 3};

	// 行動選択肢のテキスト
	public string[][] selectText;

	// 描画されていない選択肢のオブジェクトを保管するポジション
	private Vector3 poolPosition;

	// Use this for initialization
	void Start () {
		poolPosition = new Vector3(-10, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// 右ボタンが押された場合、カーソルを右に動かす。
		if (Input.GetButtonDown(playerId.ToString() + " Right")) {
			currentSelected++;
			if (currentPhase == Phase.Phase1 && currentSelected > maxSelectPhase1 - 1)
				currentSelected = 0;
			else if (currentPhase == Phase.Phase2 && currentSelected > maxSelectPhase2[currentSelected] - 1)
				currentSelected = 0;
			SetTextColorByIndex(currentSelected);
		} else if (Input.GetButtonDown(playerId.ToString() + " Left")) {
			currentSelected--;
			if (currentPhase == Phase.Phase1 && currentSelected < 0)
				currentSelected = maxSelectPhase1 - 1;
			else if (currentPhase == Phase.Phase2 && currentSelected < 0)
				currentSelected = maxSelectPhase2[currentSelected] - 1;
			SetTextColorByIndex(currentSelected);
		}

		// 決定ボタンが押された場合、次のフェーズへ進める。
		if (Input.GetButtonDown(playerId.ToString() + " Dicision")) {
			if (currentPhase == Phase.Phase1) {
				currentPhase++;
				SetLocalPositionToPoolPositionByName("Phase1 select");
				switch (currentSelected) {
					case 0:
						SetLocalPositionToZeroByName("Phase2 Act select");
						break;
					case 1:
						SetLocalPositionToZeroByName("Phase2 Appeal select");
						break;
					case 2:
						SetLocalPositionToZeroByName("Phase2 Jummer select");
						break;
					default:
						break;
				}
			} else if (currentPhase == Phase.Phase2) {
				Application.LoadLevel("Act");
			}
		}
	}

	/// <summary>
	/// 名前からオブジェクトを取得し、ローカルポジションを親オブジェクトの原点に設定します。
	/// </summary>
	/// <param name="objectName">子オブジェクト名</param>
	void SetLocalPositionToZeroByName(string objectName) {
		Transform obj = transform.GetComponentsInChildren<Transform>()
			.Where(o => o.name.Equals(objectName)).First();
		obj.transform.localPosition = Vector3.zero;
	}

	/// <summary>
	/// 名前からオブジェクトを取得し、ローカルポジションをオブジェクト保持場所に設定します。
	/// </summary>
	/// <param name="objectName">子オブジェクト名</param>
	void SetLocalPositionToPoolPositionByName(string objectName) {
		Transform obj = transform.GetComponentsInChildren<Transform>()
			.Where(o => o.name.Equals(objectName)).First();
		obj.transform.localPosition = poolPosition;
	}

	/// <summary>
	/// 現在選択されている選択肢のテキストの色を変更します。
	/// </summary>
	/// <param name="index"></param>
	void SetTextColorByIndex(int index) {
		Transform obj = transform.GetComponentsInChildren<Transform>()
			.Where(o => o.transform.position == Vector3.zero).First();
		
		foreach (Transform item in obj) {
			item.GetComponent<TextMesh>().color = Color.black;
		}

		obj.GetChild(index).GetComponent<TextMesh>().color = Color.red;
	}
}
