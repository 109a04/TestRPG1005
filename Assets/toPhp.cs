using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI物件需要增加此行
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;


public class toPhp : MonoBehaviour
{
    public InputField nameregister;//註冊名字
    public InputField emailIregister;//註冊帳號
    public InputField passwordregistert;//註冊密碼
    public Text regisText;//註冊提示

    public InputField emaillog;//輸入帳號
    public InputField passwordlog;//輸入密碼
    public Text loginText;//登入提示
    public Text playername;
    public string userID;

    public GameObject paneljump;//視窗
    public GameObject buttonshow;//開始遊戲的按鈕

    [Serializable]//玩家帳密物件
    public class player
    {
        public string name;
        public string email;
        public string password;
        public string userid;   //加好友用id
        public int id;   
    }

    //public player[] playerjson;暫時沒用到
    public player playerreg;
    public player playerlog;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Login()
    {
        string accountlo = emaillog.text;
        string passwordlo = passwordlog.text;

        playerlog.email = accountlo;
        playerlog.password = passwordlo;

        // 將物件轉換為JSON字符串
        string jsonlog = JsonUtility.ToJson(playerlog);

        // 發送POST請求
        StartCoroutine(SendloginData(jsonlog));
    }

    IEnumerator SendloginData(string jsonData)
    {
        // 建立一個表單
        WWWForm form = new WWWForm();
        form.AddField("json", jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/unityJson/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                string[] responseParts = responseText.Split(':');
                if (responseParts.Length > 1)
                {
                    playername.text = responseParts[1];
                    playerNameManager.Instance.playerName = responseParts[1];
                    playerNameManager.Instance.playerID = responseParts[2];
                    loginText.text = responseParts[0];
                }
                else
                {
                    loginText.text = responseText;
                }
                yield return new WaitForSecondsRealtime(4.0f);
                paneljump.SetActive(false);
                buttonshow.SetActive(true);
            }
        }
    }

    public void register()
    {
        string namere = nameregister.text;
        string accountre = emailIregister.text;
        string passwordre = passwordregistert.text;

        string randomID = GenerateRandomID();  // 生成隨機ID

        playerreg.name = namere;
        playerreg.email = accountre;
        playerreg.password = passwordre;
        playerreg.userid = randomID;  //建立隨機id欄位

        //將物件轉換為JSON字符串
        string jsonreg = JsonUtility.ToJson(playerreg);

        //發送POST請求
        StartCoroutine(SendRegistrationData(jsonreg));
    }

    private const int ID_LENGTH = 8;  // 定義random ID的長度，這裡設置為8位

    // 生成隨機ID
    private string GenerateRandomID()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] idChars = new char[ID_LENGTH];
        for (int i = 0; i < ID_LENGTH; i++)
        {
            idChars[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
        }
        return new string(idChars);
    }

    IEnumerator SendRegistrationData(string jsonData)
    {
        //建立一個表單
        WWWForm form = new WWWForm();
        form.AddField("json", jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/unityJson/register.php", form))
        {
            yield return www.SendWebRequest();

            //如果沒有傳資料成功
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else//有傳資料成功
            {
                string responseText = www.downloadHandler.text;
                regisText.text = responseText;

                yield return new WaitForSecondsRealtime(4.0f);
                paneljump.SetActive(false);
                //SceneManager.LoadScene(1);//暫時沒用到
            }
        }
    }

    public void panelopen()
    {
        paneljump.SetActive(true);
    }

    public void sceneStart()
    {
        SceneManager.LoadScene(1);//進到遊戲場景

    }

}
