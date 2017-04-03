using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Recognition : MonoBehaviour {

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>(); //contains words to be recognized and associated actions

    GameObject door1;
    float door1StartTime;
    Vector3 door1StartPos;
    Vector3 door1EndPos;
    bool door1Activated = false;
    float duration = 3.0f;



    void Start()
    {
        //Initialize
        keywords.Add("go there", () =>
        {
            MoveDoor1();
        });

        keywords.Add("I am a monkey", () =>
        {
            MoveDoor1();
        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhraseRecognized; //when we recognize a phrase - call this
        keywordRecognizer.Start();
        Debug.Log("Initialized");
        door1 = GameObject.FindWithTag("Door1");
        door1StartPos = door1.transform.position;
        door1EndPos = new Vector3(door1.transform.position.x, door1.transform.position.y, -13);
        
    }

    void Update()
    {
        if (door1Activated)
        {
            door1.transform.position = Vector3.Lerp(door1StartPos, door1EndPos, (Time.time - door1StartTime) / duration);
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
        door1Activated = true;
    }

    void MoveDoor2()
    {
        Debug.Log("GO THERE has been spoken.");
        door1StartTime = Time.time;
        door1Activated = true;
    }
}
