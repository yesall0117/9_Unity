using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    public InputField Id;
    public InputField Password;
    public Text Notify;

   
    // Start is called before the first frame update
    void Start()
    {
        Notify.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveUserData()
    {
        if (!CheckInput(Id.text, Password.text))
        {
            return;
        }
        if (!PlayerPrefs.HasKey(Id.text))
        {
            PlayerPrefs.SetString(Id.text, Password.text);
            Notify.text = "아이디 생성이 완료되었습니다. ";
        }
        else 
        {
            Notify.text = "이미 존재하는 아이디 입니다. ";
        }
        
    }

    public void CheckUserData()
    {
        if (!CheckInput(Id.text, Password.text))
        {
            return;
        }
        string pass=PlayerPrefs.GetString(Id.text);
        if (Password.text == pass)
        {
            SceneManager.LoadScene(1);
        }
        else 
        {
            Notify.text = "입력하신 아이디와 패스워드가 일치하지 않습니다. ";
        }
    }
    bool CheckInput(string id, string pwd)
    {
        if (id == "" || pwd == "")
        {
            Notify.text = "아이디 또는 패스워드를 입력해주세요.";
            return false;
        }
        else 
        {

            return true;
        }
    }
}
