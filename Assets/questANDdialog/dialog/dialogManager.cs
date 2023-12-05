using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogManager : MonoBehaviour
{
    public static dialogManager instance;

    public GameObject dialogueBox;
    public Text dialogueText;//對話內容
    public Text nameText;//對話者名字

    public string[] dialogueLines;//對話行數陣列
    [SerializeField] private int currentLine;//目前顯示的對話行

    private bool isScrolling;//對話字串是否完全輸出
    [SerializeField] private float scrollingSpeed;//字母輸出速度

    public Talkable npcTalking;
    public Questable npcQuest;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (dialogueBox.activeInHierarchy)//對話框是否開啟
        {
            if (Input.GetKeyDown(KeyCode.J))//按J才能顯示下一句
            {
                if (isScrolling == false)//整句顯示完整才能顯示下一句
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);
                    }
                }
            }
        }

    }

    //開始對話
    public void ShowDialogue(string[] _newLines)
    {
        dialogueLines = _newLines;
        dialogueBox.SetActive(true);
        currentLine = 0;
        CheckName();
        StartCoroutine(ScrollingText());
    }

    //檢查對話者名字
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");//去除n-
            currentLine++;//跳過名字這行
        }
    }

    //字母滾動出現
    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";//每行都是從空白開始輸出字母
        //把字串每個字母拆開放到暫時陣列
        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(scrollingSpeed);
        }
        isScrolling = false;
    }
}
