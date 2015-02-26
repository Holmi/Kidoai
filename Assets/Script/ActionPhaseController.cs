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

    private bool challengeFlag;

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
        challengeFlag = false;

        if (gameObject.tag == "p1")
        {
            SetPurpose(PlayerStatusModel.player1); // purposeで今後の処理をわけるため

            if (PlayerStatusModel.player1.SelectedPurpose == eFirstAction.improve)
            {
                ImproveAction(PlayerStatusModel.player1);
            }
            else if (PlayerStatusModel.player1.SelectedPurpose == eFirstAction.appeal)
            {
                AppealAction(PlayerStatusModel.player1);
            }
            else if (PlayerStatusModel.player1.SelectedPurpose == eFirstAction.disturb)
            {

                DisturbAction(PlayerStatusModel.player1);
            }
        }
        else if (gameObject.tag == "p2")
        {
            SetPurpose(PlayerStatusModel.player2); // purposeでわけるため

            if (PlayerStatusModel.player1.SelectedPurpose == eFirstAction.improve)
            {
                ImproveAction(PlayerStatusModel.player2);
            }
            else if (PlayerStatusModel.player2.SelectedPurpose == eFirstAction.appeal)
            {
                Debug.Log("Selected Appeal");
                AppealAction(PlayerStatusModel.player2);
            }
            else if (PlayerStatusModel.player2.SelectedPurpose == eFirstAction.disturb)
            {
                DisturbAction(PlayerStatusModel.player2);
            }
        }
    }

    void SetPurpose(PlayerStatusModel.PlayerStatus player)
    {
        switch (player.SelectedAction) 
        {
            case eSecondAction.work:
                player.SelectedPurpose = eFirstAction.improve;
                break;
            case eSecondAction.shapeup:
                player.SelectedPurpose = eFirstAction.improve;
                break;
            case eSecondAction.looks:
                player.SelectedPurpose = eFirstAction.improve;
                break;
            case eSecondAction.rest:
                player.SelectedPurpose = eFirstAction.improve;
                break;
            case eSecondAction.talk:
                player.SelectedPurpose = eFirstAction.appeal;
                break;
            case eSecondAction.present:
                player.SelectedPurpose = eFirstAction.appeal;
                break;
            case eSecondAction.date:
                player.SelectedPurpose = eFirstAction.appeal;
                break;
            case eSecondAction.gossip:
                player.SelectedPurpose = eFirstAction.disturb;
                break;
            case eSecondAction.challenge:
                player.SelectedPurpose = eFirstAction.disturb;
                break;
            case eSecondAction.curse:
                player.SelectedPurpose = eFirstAction.disturb;
                break; 
        }

    }

    // Update is called once per frame
    void Update()
    {


        // いつステータス更新するかはあとで検討
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UpdateParameters(PlayerStatusModel.player1);
            if (challengeFlag) Application.LoadLevel("Challenge Mini Game"); 
            else Application.LoadLevel("Select Phase");
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UpdateParameters(PlayerStatusModel.player2);
            if (challengeFlag) Application.LoadLevel("Challenge Mini Game");
            else Application.LoadLevel("Select Phase");
        }



    }

    void AppealAction(PlayerStatusModel.PlayerStatus player)
    {
        Debug.Log(" Enter method AppealAction:" + gameObject.tag.ToString());
        if (gameObject.tag == "p1")
        {
            GameObject selectBoxes = GameObject.FindWithTag("p1_selectBoxes");
            selectBoxes.transform.Translate(5, 0, 0);
        }
        else if (gameObject.tag == "p2")
        {
            GameObject selectBoxes = GameObject.FindWithTag("p2_selectBoxes");
            selectBoxes.transform.Translate(5, 0, 0);
        }


    }

    void ImproveAction(PlayerStatusModel.PlayerStatus player)
    {
        Debug.Log(" Enter method ImproveAction:"+gameObject.tag.ToString());
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

    void DisturbAction(PlayerStatusModel.PlayerStatus player)
    {
        if (player.SelectedAction == eSecondAction.challenge)
        {
            
           // ミニゲームに遷移するためにフラグ立て
           

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