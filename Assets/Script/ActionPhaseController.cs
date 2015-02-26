using UnityEngine;
using System.Collections;

public class ActionPhaseController : MonoBehaviour
{

    // とりあえず定義しとく
    //public int money;
    //public int stamina;
    //public int max_stamina; // 体力の上限値
    //public int looks;
    //public int taketime;    // かかる日数

    public eFirstAction myFirstAction;  // 自分磨き、アピール、妨害のどれか
    public eSecondAction mySecondAction; // バイト、会話、噂など

    public enum eFirstAction { improve, appeal, disturb };
    public enum eSecondAction
    {
        work = 1, shapeup, looks, rest,
        talk, present, date,
        gossip, challenge, curse
    };



    // Use this for initialization
    void Start()
    {
        

        //UpdateParameters(myAction);


    }

    // Update is called once per frame
    void Update()
    {
        // いつステータス更新するかはあとで検討
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateParameters(PlayerStatusModel.player1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateParameters(PlayerStatusModel.player2);
        }

    }

    void UpdateParameters(PlayerStatusModel.PlayerStatus player)
    {     
        player.TakeTime -= 1;
        switch (player.SelectedAction)
        {
            case eSecondAction.work:
                player.Money += 750;
                player.Stamina -= 2;
                break;
            case eSecondAction.shapeup:
                player.Stamina -= 2;
                if (player.TakeTime == 0) player.MaxStamina += 5; // 最終日に上限値上げる         
                break;
            case eSecondAction.looks:
                player.Looks += 10;
                break;
            case eSecondAction.rest:
                player.Stamina += 5;
                break;
            case eSecondAction.talk:
                TalkEvent();
                break;
        }
    }



    void TalkEvent()
    {
        // 選択肢のオブジェクトをスライドさせて持ってくる
        // 処理はそのオブジェクト用のスクリプトに任せる?

        // 会話ボックスも持ってくる
        // 描画に関しては会話ボックスのスクリプトに任せる?

    }





}