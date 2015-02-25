using UnityEngine;
using System.Collections;

public class SelectPhaseMangerScript : MonoBehaviour {

	// 各プレイヤーが選択肢を決定し終えたかのフラグ
	private bool[] readyToNextPhaseFlg = { false, false };

	// 各プレイヤーのステータス表示オブジェクト
	public GameObject p1_statusObj;
	public GameObject p2_statusObj;

	// Use this for initialization
	void Start () {
		PlayerStatusModel.nowDate++;
		SetStatusToUI();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			Application.LoadLevel(Application.loadedLevel);
	}
	
	/// <summary>
	/// プレイヤーIDを受け取り、対応するフラグをtrueにします。
	/// すべてのプレイヤーが次の選択肢を決定し終えた場合、次のシーンへ遷移します。
	/// </summary>
	/// <param name="playerId">プレイヤーID</param>
	public void ChangeReadyToNextFlg(int playerId) {
		readyToNextPhaseFlg[playerId] = true;
		foreach (bool item in readyToNextPhaseFlg) {
			if (!item)
				return;
		}
		Application.LoadLevel("Action Phase");
	}

	/// <summary>
	/// 画面にプレイヤーのパラメータを表示します。
	/// </summary>
	void SetStatusToUI() {
		TextMesh[] textMesh = p1_statusObj.GetComponentsInChildren<TextMesh>();
		foreach(TextMesh item in textMesh) {
			switch (item.name) {
				case "Date":
					item.text = PlayerStatusModel.nowDate.ToString();
					break;
				case "Money":
					item.text = PlayerStatusModel.player1.Money.ToString();
					break;
				case "Looks":
					item.text = CalcLooksRank(PlayerStatusModel.player1.Looks);
					break;
				case "Stamina":
					item.text = CalcStaminaRank(PlayerStatusModel.player1.Stamina, PlayerStatusModel.player1.MaxStamina);
					break;
				default:
					break;
			}
		} 

		textMesh = p2_statusObj.GetComponentsInChildren<TextMesh>();
		foreach(TextMesh item in textMesh) {
			switch (item.name) {
				case "Date":
					item.text = PlayerStatusModel.nowDate.ToString();
					break;
				case "Money":
					item.text = PlayerStatusModel.player2.Money.ToString();
					break;
				case "Looks":
					item.text = CalcLooksRank(PlayerStatusModel.player2.Looks);
					break;
				case "Stamina":
					item.text = CalcStaminaRank(PlayerStatusModel.player2.Stamina, PlayerStatusModel.player2.MaxStamina);
					break;
				default:
					break;
			}
		}
	}

	/// <summary>
	/// ルックス値からランクを計算します。
	/// </summary>
	/// <param name="looks">ルックス値</param>
	/// <returns></returns>
	string CalcLooksRank(int looks) {
		if (looks >= 100)
			return "S";
		else if (looks >= 85)
			return "A";
		else if (looks >= 70)
			return "B";
		else if (looks >= 50)
			return "C";
		else if (looks >= 35)
			return "D";
		else if (looks >= 20)
			return "E";
		
		return "F";
	}

	/// <summary>
	/// 体力の最大値と現在の値からスタミナのランクを計算します。
	/// </summary>
	/// <param name="stamina">現在のスタミナ値</param>
	/// <param name="maxStamina">最大スタミナ値</param>
	/// <returns></returns>
	string CalcStaminaRank(int stamina, int maxStamina) {
		float staminaRate = stamina / maxStamina;
		if (staminaRate >= 0.8f)
			return "A";
		else if (staminaRate >= 0.7f)
			return "B";
		else if (staminaRate >= 0.5f)
			return "C";
		else if (staminaRate >= 0.35f)
			return "D";
		else if (staminaRate >= 0.2f)
			return "E";
		return "F";
	}
}
