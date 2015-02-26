using UnityEngine;
using System.Collections;

public class ActionPhaseBackground : MonoBehaviour {
    
    SpriteRenderer MainSpriteRenderer;
    

    // publicで宣言し、inspectorで設定可能にする
    public Sprite WorkSprite;
    public Sprite RestSprite;
    public Sprite LooksSprite;
    public Sprite ShapeSprite;
    
    public string actionName;

	// Use this for initialization
	void Start () {

        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // 選択された行動によって背景を変える(なんかスマートにできなかったゴメン)
        if (gameObject.tag == "p1_bg") ChangeBackground(PlayerStatusModel.player1);
        else if (gameObject.tag == "p2_bg") ChangeBackground(PlayerStatusModel.player2);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void ChangeBackground(PlayerStatusModel.PlayerStatus player)
    {
              
        // SpriteRenderのspriteを設定済みの他のspriteに変更
        switch (player.SelectedAction)
        {
            case ActionPhaseController.eSecondAction.work:
                MainSpriteRenderer.sprite = WorkSprite;
                break;
            case ActionPhaseController.eSecondAction.rest:
                MainSpriteRenderer.sprite = RestSprite;
                break;
            case ActionPhaseController.eSecondAction.looks:
                MainSpriteRenderer.sprite = LooksSprite;
                break;
            case ActionPhaseController.eSecondAction.shapeup:
                MainSpriteRenderer.sprite = ShapeSprite;
                break;
            default:
                break;
        }    
    }
}
