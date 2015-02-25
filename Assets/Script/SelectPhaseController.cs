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
	private int maxSelectPhase2;

	// 描画されていない選択肢のオブジェクトを保管するポジション
	private Vector3 poolPosition;

	//選択カーソルのゲームオブジェクト
	public GameObject select;

	// シーンの管理オブジェクト
	public GameObject manager;

	// 選択終了時に表示するテキストオブジェクト
	public GameObject waitText;

	// Use this for initialization
	void Start () {
		poolPosition = new Vector3(-10, 0, 0);
		if (manager == null)
			manager = GameObject.Find("Select Phase Manager");
	}
	
	// Update is called once per frame
	void Update () {
		// 右ボタンが押された場合、カーソルを右に動かす。
		// 選択肢の端を超えた場合、反対側へカーソルを移動させる
		if (Input.GetButtonDown(playerId.ToString() + " Right")) {
			currentSelected++;
			if (currentPhase == Phase.Phase1 && currentSelected > maxSelectPhase1 - 1)
				currentSelected = 0;
			else if (currentPhase == Phase.Phase2 && currentSelected > maxSelectPhase2 - 1)
				currentSelected = 0;
			MoveCursorObjectToSelectedObject();
		} else if (Input.GetButtonDown(playerId.ToString() + " Left")) {
			select.transform.position -= new Vector3(0.6f,0,0);
			currentSelected--;
			if (currentPhase == Phase.Phase1 && currentSelected < 0)
				currentSelected = maxSelectPhase1 - 1;
			else if (currentPhase == Phase.Phase2 && currentSelected < 0)
				currentSelected = maxSelectPhase2 - 1;
			MoveCursorObjectToSelectedObject();
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
				// 子オブジェクト中からローカルポジションが親オブジェクトの原点のものを取得し、選択肢の数を取得する
				Transform o = this.GetComponentsInChildren<Transform>()
					.Where(obj => obj.transform.localPosition == Vector3.zero && obj.transform != this.transform)
					.First();
				maxSelectPhase2 = o.transform.GetComponentsInChildren<Transform>()
					.Where(obj => obj.transform != o.transform && obj.name.Contains("Select"))
					.ToArray()
					.Length;
				
				// カーソルを選択中の選択肢の上に配置する
				MoveCursorObjectToSelectedObject();
			} else if (currentPhase == Phase.Phase2) {
				manager.GetComponent<SelectPhaseMangerScript>().ChangeReadyToNextFlg((int) playerId);
				GameObject obj = Instantiate(waitText) as GameObject;
				obj.transform.parent = this.transform;
				obj.transform.localPosition = new Vector3(0, 0, 0.8f);
				Destroy(this);
			}
		}

		if (Input.GetButtonDown(playerId.ToString() + " Cancel")) {
			if (currentPhase == Phase.Phase2) {
				currentPhase = Phase.Phase1;
				currentSelected = 0;
				string currentPhaseObjName = GetComponentsInChildren<Transform>()
					.Where(obj => obj.transform.localPosition == Vector3.zero)
					.First()
					.name;
				SetLocalPositionToPoolPositionByName(currentPhaseObjName);
				SetLocalPositionToZeroByName("Phase1 select");
				MoveCursorObjectToSelectedObject();
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
	/// カーソルオブジェクトを選択中のオブジェクトのポジションに移動させます。
	/// </summary>
	void MoveCursorObjectToSelectedObject() {
		Transform obj = transform.GetComponentsInChildren<Transform>()
			.Where(o => o.transform.localPosition == Vector3.zero).First();
		select.transform.parent = obj.GetChild(currentSelected).transform;
		select.transform.localPosition = new Vector3(0, 0.5f, 0);
	}
}
