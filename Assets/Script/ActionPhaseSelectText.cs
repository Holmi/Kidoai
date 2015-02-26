using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionPhaseSelectText : MonoBehaviour {

    private List<SelectText> selectdatas = new List<SelectText>();
    public ActionPhaseSelectText()
    {
        ;
    }

    public class SelectText
    {
        public string message; // 表示される選択肢
        public int addlove; //これを選択すると加えられる好感度


        public SelectText()
        {
            message = null;
            this.addlove = 0;
        }
        
    }


    // テキストファイルを処理
    public void LoadSelectTextData(TextAsset textfile)
    {
        string select_texts = textfile.text;

        string[] lines = select_texts.Split('\n');

        foreach (var line in lines)
        {
            if (line == "")
            {
                continue;
            }
            Debug.Log(line);
            string[] words = line.Split();

            int n = 0;

            SelectText selecttext_data = new SelectText();
            foreach (var word in words)
            {
                if (word.StartsWith("#")) break;
                if (word == "") continue;

                switch (n)
                {
                    case 0: // 読み込んだ値をmessageに入れる
                        selecttext_data.message = word;
                        break;
                    case 1:
                        selecttext_data.addlove = int.Parse(word);
                        break;
                }

                n++;
            }

            if (n >= 2) this.selectdatas.Add(selecttext_data);

        }

    }


}
