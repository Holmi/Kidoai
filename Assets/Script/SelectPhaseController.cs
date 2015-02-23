using UnityEngine;
using System.Collections;

public class SelectPhaseController : MonoBehaviour {

	// プレイヤーの識別子
	public int playerId;

	// 選択画面で現在選択中の選択肢
	private enum Select { Phase1, Phase2 };
	private Select currentSelected = Select.Phase1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Player" + playerId.ToString() + " Right")) {
			currentSelected++;
		}
		if (Input.GetButtonDown("Player" + playerId.ToString() + " Dicition")) {
			
		}
	}
}
