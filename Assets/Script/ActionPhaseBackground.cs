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
        // 今はシーンから持ってこれないのでworkに初期化
        //actionName = "looks";

        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // 行動名によって背景を変える
        ChangeBackground(actionName);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void ChangeBackground(string actionName)
    {
        // SpriteRenderのspriteを設定済みの他のspriteに変更
        switch (actionName)
        {
            case "work":
                MainSpriteRenderer.sprite = WorkSprite;
                break;
            case "rest":
                MainSpriteRenderer.sprite = RestSprite;
                break;
            case "looks":
                MainSpriteRenderer.sprite = LooksSprite;
                break;
            case "shape":
                MainSpriteRenderer.sprite = ShapeSprite;
                break;
            default:
                break;
        }    
    }
}
