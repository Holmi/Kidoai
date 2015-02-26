using UnityEngine;
using System.Collections;

public class PlayerStatusModel {

	public static PlayerStatus player1 = new PlayerStatus();
	public static PlayerStatus player2 = new PlayerStatus();

	public static int nowDate = 0;

	public class PlayerStatus {
		public PlayerStatus() {
			money = 0;
			maxStamina = 10;
			stamina = maxStamina;
			looks = 0;
			takeTime = 0;
		}

		// プレイヤー1の所持金額値
		private int money;
		public int Money {
			get { return money; }
			set { money = (value >= 0) ? value : money; }
		}

		// 体力値
		private int stamina;
		public int Stamina {
			get { return stamina; }
			set { stamina = value; }
		}

		// ルックス値
		private int looks;
		public int Looks { 
			get { return looks; }
			set { looks = value; }
		}

		// 最大体力値
		private int maxStamina;
		public int MaxStamina {
			get { return maxStamina; }
			set { maxStamina = (value >= maxStamina) ? value : maxStamina; }
		}

		// かかる時間
		public int takeTime;
	}
}
