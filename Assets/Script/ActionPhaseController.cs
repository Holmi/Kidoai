using UnityEngine;
using System.Collections;

public class ActionPhaseController : MonoBehaviour
{

    // とりあえず定義しとく
    public int money;
    public int stamina;
    public int max_stamina; // 体力の上限値
    public int looks;
    public int taketime;    // かかる日数

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
        // 前シーンから受けつぐが今は仮値入れとく
        //myAction = eAction.shapeup;
        
        money = 1000;
        stamina = 10;
        max_stamina = 15;
        looks = 10;
        taketime = 2;// 体力づくりの場合

        //UpdateParameters(myAction);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateParameters(mySecondAction);
        }
    }


    void UpdateParameters(eSecondAction action)
    {
        
        taketime -= 1;
        switch (action)
        {
            case eSecondAction.work:
                money += 750;
                stamina -= 2;
                break;
            case eSecondAction.shapeup:
                stamina -= 2;
                if (taketime == 0) max_stamina += 5; // 最終日に上限値上げる         
                break;
            case eSecondAction.looks:
                looks += 10;
                break;
            case eSecondAction.rest:
                stamina += 5;
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