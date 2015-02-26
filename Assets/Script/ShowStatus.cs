using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowStatus : MonoBehaviour {

	Text status;
	//プレイヤー１か２か判別。インスペクター上で設定
	public int playerNum;

	// Use this for initialization
	void Start () {
		string looks = "",stamina = "";
		int money = 0,love = 0;
		status = GetComponent<Text> ();
		if(playerNum == 1){
			looks = CalcLooksRank(PlayerStatusModel.player1.Looks);
			stamina = CalcStaminaRank(PlayerStatusModel.player1.Stamina,PlayerStatusModel.player1.MaxStamina);
			money = PlayerStatusModel.player1.Money;
			love = PlayerStatusModel.player1.Love;
		}
		if(playerNum == 2){
			looks = CalcLooksRank(PlayerStatusModel.player2.Looks);
			stamina = CalcStaminaRank(PlayerStatusModel.player2.Stamina,PlayerStatusModel.player2.MaxStamina);
			money = PlayerStatusModel.player2.Money;
			love = PlayerStatusModel.player2.Love;
		}
		status.text = "好感度: " + love + "\n所持金: " + money + "\nルックス: " + looks + "\nスタミナ: " + stamina;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")) Application.LoadLevel("Select Phase");
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
