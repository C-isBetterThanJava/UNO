using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Text name;
    public Text username;
    public Text password;
    public Text passwordConfirm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void Awake(){
    //     readFile();
    //     writeToFile();
    //     readFile();
    // }

    public string getUsername()
    {
        return username.text.ToString();
    }

    public string getPassword()
    {
        return password.text.ToString();
    }

    public string getPasswordConfirmation(){
        return passwordConfirm.text.ToString();
    }

    public string getName()
    {
        return name.text.ToString();
    }

    public string getFilePath(){
        string filePath = "Assets/userData.csv";
        return filePath;
    }

    public void readFile(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = reader.ReadLine();
            if(data_string == null){
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(',');
            for(int i = 0; i < data_values.Length; i++){
                Debug.Log(data_values[i].ToString());
            }
        }
        reader.Close();
    }
    
    public bool checkAccount(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = reader.ReadLine();
            if(data_string == null){
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(',');
            for(int i = 0; i < data_values.Length; i++){ //check the first 2 columns only
                if(data_values[i] == getName() || data_values[i] == getUsername()){
                    return false;
                }
            }
        }
        reader.Close();
        return true;
    }

    public void writeToFile(string line){
        string filePath = getFilePath();
        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(line);
        writer.Close();
    }

    public void createAccount(){
        if(getPasswordConfirmation() != getPassword()){
            Debug.Log("Passwords do not match");
        }
        else{
            if(checkAccount()){
                string data = getName().ToString() + "," + getUsername().ToString() + "," + getPassword().ToString();
                writeToFile(data);
                SceneManager.LoadScene("Menu");
            }
            else{
                Debug.Log("Name or username already taken");
            }
        }
        
    }

    public bool validateLogin(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = reader.ReadLine();
            if(data_string == null){
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(',');
            for(int i = 0; i < data_values.Length; i++){ //check the first 2 columns only
                if(data_values[1] == getUsername()){
                    if(getPassword() == data_values[2]){
                        return true;
                    }
                }
            }
        }
        reader.Close();
        return false;
    }

    public void login(){
        if(validateLogin()){
            SceneManager.LoadScene("Menu");
        }
        else{
            Debug.Log("Invalid Login");
        }
        
    }

    
}
