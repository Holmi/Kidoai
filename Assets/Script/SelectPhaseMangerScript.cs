using UnityEngine;
using System.Collections;
using System.Threading;

public class SelectPhaseMangerScript : MonoBehaviour {

	// 各プレイヤーが選択肢を決定し終えたかのフラグ
	private bool[] readyToNextPhaseFlg = { false, false };

	// 各プレイヤーのステータス表示オブジェクト
	public GameObject p1_statusObj;
	public GameObject p2_statusObj;

	// 次のシーンへ遷移する際の効果音
	public AudioClip changeSceneSE;

	// カードを引いた時の効果音
	public AudioClip drawCardSE;

	// 最終日の日付
	[SerializeField]
	private int lastDate = 30;

	private delegate void Function();
	void PlayEvent(AudioClip ac, Function func) {
		audio.PlayOneShot(ac);
		Thread.Sleep(ac.samples / ac.frequency * 1000);
		func();
	}

	// Use this for initialization
	void Start () {
		PlayerStatusModel.nowDate++;
		if (PlayerStatusModel.nowDate >= lastDate)
			PlayEvent(changeSceneSE, () => Application.LoadLevel("End Scene"));
		else if (PlayerStatusModel.nowDate == 10 || PlayerStatusModel.nowDate == 20)
			PlayEvent(changeSceneSE, () => { Application.LoadLevel("Mid"); });
		SetStatusToUI();
		PlayDrawSound();
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
	public void ChangeReadyToNextFlg(int playerId, ActionPhaseController.eSecondAction action) {
		readyToNextPhaseFlg[playerId] = true;
		SetAction(playerId, action);
		Debug.Log(action.ToString());
		foreach (bool item in readyToNextPhaseFlg) {
			if (!item)
				return;
		}
		PlayEvent(changeSceneSE, () => Application.LoadLevel("Action Phase"));
	}

	/// <summary>
	/// 画面にプレイヤーのパラメータを表示します。
	/// </summary>
	public void SetStatusToUI() {
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

	/// <summary>
	/// プレイヤーの行動を次シーンへの受け渡しモデルに設定します。
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="action"></param>
	void SetAction(int playerId, ActionPhaseController.eSecondAction action) {
		switch (playerId) {
			case 0:
				PlayerStatusModel.player1.SelectedAction = action;
				PlayerStatusModel.player1.TakeTime = CalcTakeTime(action);
				break;
			case 1:
				PlayerStatusModel.player2.SelectedAction = action;
				PlayerStatusModel.player2.TakeTime = CalcTakeTime(action);
				break;
			default:
				break;
		}
	}


	/// <summary>
	/// 行動からかかる時間を取得します。
	/// </summary>
	/// <param name="action">行動</param>
	/// <returns></returns>
	int CalcTakeTime(ActionPhaseController.eSecondAction action) {
		switch (action) {
			case ActionPhaseController.eSecondAction.work:
				return 3;
			case ActionPhaseController.eSecondAction.shapeup:
				return 2;
			case ActionPhaseController.eSecondAction.looks:
				return 1;
			case ActionPhaseController.eSecondAction.rest:
				return 1;
			case ActionPhaseController.eSecondAction.talk:
				return 1;
			case ActionPhaseController.eSecondAction.present:
				return 1;
			case ActionPhaseController.eSecondAction.date:
				return 2;
			case ActionPhaseController.eSecondAction.gossip:
				return 1;
			case ActionPhaseController.eSecondAction.challenge:
				return 1;
			case ActionPhaseController.eSecondAction.curse:
				return 1;
			default:
				return 0;
		}
	}

	// カードを引いた効果音を鳴らします。
	void PlayDrawSound() {
		audio.PlayOneShot(drawCardSE);
	}
}
