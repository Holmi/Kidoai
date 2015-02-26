using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadText : MonoBehaviour {

	//画面に表示させる文章を格納する変数
	Text text;
	//テキストの各１行づつ格納する配列
	public string[] txtArray;
	//テキストの行番号の変数
	int n = 0;

	// Use this for initialization
	void Start () {
		int p1, p2;
		int heroineNum=0;
		p1 = StatusCal(heroineNum,PlayerStatusModel.player1);
		p2 = StatusCal(heroineNum,PlayerStatusModel.player2);
		//p2 = 10000000;
		if(p1 >= p2) Load (1);
		else Load(2);
	}

	// Update is called once per frame
	void Update () {
		text.text = txtArray[n];
		//マウス押したら次の文
		if(Input.GetMouseButtonDown(0)) n++;
	}

	//最終的な合計のスコアを計算する関数
	int StatusCal(int heroineNum,PlayerStatusModel.PlayerStatus player){
		if(heroineNum == 0) return player.Love*5 + player.MaxStamina*3 + player.Looks + player.Money;
		else if(heroineNum == 1) return player.Love*5 + player.MaxStamina + player.Looks*3 + player.Money;
		else return player.Love*5 + player.MaxStamina + player.Looks + player.Money*3;
	}

	//テキスト等のロードや文章を区切る関数
	void Load(int winner){
		string splitMark = ";";
		//テキストを格納
		TextAsset txt = Resources.Load("EndPlayer" + winner) as TextAsset;
		//";"で区切って配列に格納
		txtArray = txt.text.Split (splitMark[0]);
		text = GetComponent<Text> ();
	}

}
