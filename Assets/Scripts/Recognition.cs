using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Recognition : MonoBehaviour {

    public AudioClip doorOpenSound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>(); //contains words to be recognized and associated actions

    float duration = 3.0f;

    GameObject door1;
    float door1StartTime;
    Vector3 door1StartPos;
    Vector3 door1EndPos;
    bool door1Activated = false;

    GameObject door2;
    float door2StartTime;
    Vector3 door2StartPos;
    Vector3 door2EndPos;
    bool door2Activated = false;

    GameObject door3;
    float door3StartTime;
    Vector3 door3StartPos;
    Vector3 door3EndPos;
    bool door3Activated = false;

    GameObject door4;
    float door4StartTime;
    Vector3 door4StartPos;
    Vector3 door4EndPos;
    bool door4Activated = false;

    GameObject door5;
    float door5StartTime;
    Vector3 door5StartPos;
    Vector3 door5EndPos;
    bool door5Activated = false;

    GameObject passwordDoor;
    float passwordDoorStartTime;
    Vector3 passwordDoorStartPos;
    Vector3 passwordDoorEndPos;
    bool passwordDoorActivated = false;

    GameObject passwordText;
    int passwordTextCounter;
    bool displayPassword;

    void Start()
    {
        //Initialize
        keywords.Add("go there", () =>
        {
            MoveDoor1();
        });

        keywords.Add("I am a monkey", () =>
        {
            MoveDoor2();
        });

        keywords.Add("Nothing happened in the fifteen hundreds", () =>
        {
            MoveDoor3();
        });

        keywords.Add("Macs are better than Windows", () =>
        {
            MoveDoor4();
        });

        keywords.Add("Gaussian Blur", () =>
        {
            MoveDoor5();
        });

        keywords.Add("I hate passwords", () =>
        {
            MovePasswordDoor();
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized; //when we recognize a phrase - call this
        keywordRecognizer.Start();
        Debug.Log("Initialized");

        door1 = GameObject.FindWithTag("Door1");
        door1StartPos = door1.transform.position;
        door1EndPos = new Vector3(door1.transform.position.x, door1.transform.position.y, -13);

        door2 = GameObject.FindWithTag("Door2");
        door2StartPos = door2.transform.position;
        door2EndPos = new Vector3(door2.transform.position.x, door2.transform.position.y, -13);

        door3 = GameObject.FindWithTag("Door3");
        door3StartPos = door3.transform.position;
        door3EndPos = new Vector3(door3.transform.position.x, door3.transform.position.y, -13);

        door4 = GameObject.FindWithTag("Door4");
        door4StartPos = door4.transform.position;
        door4EndPos = new Vector3(door4.transform.position.x, door4.transform.position.y, -13);

        door5 = GameObject.FindWithTag("Door5");
        door5StartPos = door5.transform.position;
        door5EndPos = new Vector3(door5.transform.position.x, door5.transform.position.y, -13);

        passwordDoor = GameObject.FindWithTag("PasswordDoor");
        passwordDoorStartPos = passwordDoor.transform.position;
        passwordDoorEndPos = new Vector3(passwordDoor.transform.position.x, passwordDoor.transform.position.y, -13);

        passwordText = GameObject.FindWithTag("PasswordText");
        Invoke("SetPasswordInvisible", 0.5f);
        

        passwordTextCounter = 1000;
    }

    void Update()
    {
        if (door1Activated)
        {
            door1.transform.position = Vector3.Lerp(door1StartPos, door1EndPos, (Time.time - door1StartTime) / duration);
        }

        if (door2Activated)
        {
            door2.transform.position = Vector3.Lerp(door2StartPos, door2EndPos, (Time.time - door2StartTime) / duration);
        }

        if (door3Activated)
        {
            door3.transform.position = Vector3.Lerp(door3StartPos, door3EndPos, (Time.time - door3StartTime) / duration);
        }

        if (door4Activated)
        {
            door4.transform.position = Vector3.Lerp(door4StartPos, door4EndPos, (Time.time - door4StartTime) / duration);
        }

        if (door5Activated)
        {
            door5.transform.position = Vector3.Lerp(door5StartPos, door5EndPos, (Time.time - door5StartTime) / duration);
        }

        if (passwordDoorActivated)
        {
            passwordDoor.transform.position = Vector3.Lerp(passwordDoorStartPos, passwordDoorEndPos, (Time.time - passwordDoorStartTime) / duration);
        }

        
    }

    void KeywordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if(keywords.TryGetValue(args.text, out keywordAction)) //try and get phrase spoken
        {
            keywordAction.Invoke();
        }
    }

    void MoveDoor1()
    {
        Debug.Log("GO THERE has been spoken.");
        door1StartTime = Time.time;
        door1.GetComponent<AudioSource>().Play();
        SetPasswordVisible();
        Invoke("SetPasswordInvisible", 0.5f);
        door1Activated = true;
    }

    void MoveDoor2()
    {
        Debug.Log("I AM A MONKEY has been spoken.");
        door2StartTime = Time.time;
        door2Activated = true;
        SetPasswordVisible();
        Invoke("SetPasswordInvisible", 0.3f);
    }

    void MoveDoor3()
    {
        Debug.Log("Fifteen hundreds has been spoken.");
        door3StartTime = Time.time;
        door3Activated = true;
        SetPasswordVisible();
        Invoke("SetPasswordInvisible", 0.1f);
    }

    void MoveDoor4()
    {
        Debug.Log("Macs has been spoken.");
        door4StartTime = Time.time;
        door4Activated = true;
    }

    void MoveDoor5()
    {
        Debug.Log("Gaussian has been spoken.");
        door5StartTime = Time.time;
        door5Activated = true;
    }

    void MovePasswordDoor()
    {
        Debug.Log("Password has been spoken.");
        passwordDoorStartTime = Time.time;
        passwordDoorActivated = true;
    }

    void SetPasswordTextVisibility(bool isVisible)
    {
        passwordText.SetActive(isVisible);
    }

    void SetPasswordVisible()
    {
        SetPasswordTextVisibility(true);
    }

    void SetPasswordInvisible()
    {
        SetPasswordTextVisibility(false);
    }

    bool IsPasswordTextVisible()
    {
        return passwordText.activeSelf;
    }
}
