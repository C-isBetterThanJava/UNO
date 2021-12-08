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
        return username.text.ToString(); //convert text to string then return
    }

    public string getPassword()
    {
        return password.text.ToString(); //convert text to string then return
    }

    public string getPasswordConfirmation(){
        return passwordConfirm.text.ToString(); //convert text to string then return
    } 

    public string getName()
    {
        return name.text.ToString(); //convert text to string then return
    }

    public string getFilePath(){
        string filePath = "Assets/userData.csv"; //filePath
        return filePath;
    }

    public void readFile(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){ //go until next of file
            string data_string = reader.ReadLine(); //read line
            if(data_string == null){ //check line 
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(','); //split since were working with a csv
            for(int i = 0; i < data_values.Length; i++){
                Debug.Log(data_values[i].ToString()); //read each column
            }
        }
        reader.Close();
    }
    
    public bool checkAccount(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){ //go until next of file
            string data_string = reader.ReadLine();  //read line
            if(data_string == null){ //check line 
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(','); //split since were working with a csv
            for(int i = 0; i < data_values.Length; i++){ //check all the columns
                if(data_values[i] == getName() || data_values[i] == getUsername()){ //check to see if username or name has already but submitted
                    return false; //cannot reuse name or username 
                }
            }
        }
        reader.Close();
        return true; //has not been used yet
    }

    public void writeToFile(string line){
        string filePath = getFilePath();
        StreamWriter writer = new StreamWriter(filePath, true); //true so we can append 
        writer.WriteLine(line); //writeline 
        writer.Close();
    }

    public void createAccount(){
        if(getPasswordConfirmation() != getPassword()){ //make sure password and confirmpassword match 
            Debug.Log("Passwords do not match");
        }
        else{
            if(checkAccount()){
                string data = getName().ToString() + "," + getUsername().ToString() + "," + getPassword().ToString(); //format data to write
                writeToFile(data);
                SceneManager.LoadScene("Menu"); //load the next scene 
            }
            else{
                Debug.Log("Name or username already taken"); //cant use the username
            }
        }
        
    }

    public bool validateLogin(){
        string filePath = getFilePath();
        StreamReader reader = new StreamReader(filePath);
        bool endOfFile = false;
        while(!endOfFile){ //go until next of file
            string data_string = reader.ReadLine();  //read line
            if(data_string == null){  //check line 
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(',');  //split since were working with a csv
            for(int i = 0; i < data_values.Length; i++){ //check all the columns
                if(data_values[1] == getUsername()){ //find the username 
                    if(getPassword() == data_values[2]){ //check the password 
                        return true; //all good
                    }
                }
            }
        }
        reader.Close();
        return false; //cant find username or password wrong
    }

    public void login(){
        if(validateLogin()){
            SceneManager.LoadScene("Menu"); //if validate == true, go to menu
        }
        else{
            Debug.Log("Invalid Login"); //login info was invalid
        }
        
    }

    
}
