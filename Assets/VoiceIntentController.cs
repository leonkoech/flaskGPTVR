using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using TMPro;
using System.Net;
using System.Linq;
using System;
using System.IO;
using Assets;

public class VoiceIntentController : MonoBehaviour
{

    [SerializeField]
    private AppVoiceExperience appVoiceExperience;
    private TextMeshPro textMeshPro;
    string ourInput;

    // activate when hand goes into the sphere
    private bool appVoiceActive;
    
    private string fetchChatGpt(string input) 
        {
            var url = "http://webcode.me/";

            var request = WebRequest.Create(url);
            request.Method = "GET";

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();

            textMeshPro.text = data;

       
        return data;
        } 


    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        //appVoiceExperience.events.onFullTranscription.AddListener((transcription) =>
        //{
        //  Debug.Log(transcription);
        // Logger.instace.LogInfo(transcription);
        //});
         appVoiceExperience.VoiceEvents.onPartialTranscription.AddListener((transcription) =>
        {
        // Logger.instace.LogInfo(transcription);
          Debug.Log(transcription);
           ourInput = transcription;
        });
        appVoiceExperience.VoiceEvents.OnRequestCreated.AddListener((request) =>
        {
            textMeshPro.text = "input started";
        });
        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(() =>
        {
            //textMeshPro.text =  ourInput;
            // call our api here
            fetchChatGpt(ourInput);
        });
        appVoiceExperience.VoiceEvents.onFullTranscription.AddListener((transcription) =>
       {
       textMeshPro.text = "Full transcription: " + transcription;
           
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        appVoiceExperience.Activate();
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.text = "activates";
        Debug.Log("Activatess");

    }

    // Update is called once per frame
    void Update()
    {
        //appVoiceExperience.Activate();
        appVoiceExperience.Activate();

    }
    public void callChat(string[] info)
    {
        textMeshPro = GetComponent<TextMeshPro>();
        if (info.Length > 0)
        {
            string inputToChat = info[0];
            textMeshPro.text = inputToChat;
            Debug.Log(inputToChat);
        }
    }


}
