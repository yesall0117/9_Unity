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
            Notify.text = "���̵� ������ �Ϸ�Ǿ����ϴ�. ";
        }
        else 
        {
            Notify.text = "�̹� �����ϴ� ���̵� �Դϴ�. ";
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
            Notify.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�. ";
        }
    }
    bool CheckInput(string id, string pwd)
    {
        if (id == "" || pwd == "")
        {
            Notify.text = "���̵� �Ǵ� �н����带 �Է����ּ���.";
            return false;
        }
        else 
        {

            return true;
        }
    }
}
