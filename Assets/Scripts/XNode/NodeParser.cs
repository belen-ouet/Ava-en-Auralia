using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using UnityEngine.SceneManagement;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser;
    public Text speaker;
    public Text dialogue;
    public Image speakerImageL;
    public Image speakerImageR;
    public Image bubbleImage;
    public Image notesImage;
    public Text notesText;
    public bool buttonPress = false;
   

    private void Start()
    {
        foreach (BaseNode b in graph.nodes)
        {
           if (b.GetString() == "Start")
           {
            //Make this node the startting point
            graph.current = b;
            break;
           }
        }
        _parser = StartCoroutine(ParseNode());
    }

    public void PressButton()
    {
        if (buttonPress == false)
        {
            buttonPress = true;
        }
        
        AudioManager.instance.PlaySFX("click");
    }

    IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');
        if (dataParts[0] == "Start")
        {
            NextNode("exit");
        }        
        if (dataParts[0] == "DialogueNode")
        {
            //Run dialogue processing
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];
            speakerImageL.sprite = b.GetSpriteL();
            speakerImageR.sprite = b.GetSpriteR();
            bubbleImage.sprite = b.GetSpriteBubble();
            notesImage.sprite = b.GetSpriteNotebook();
            notesText.text = dataParts[3];
            
            yield return new WaitUntil(() => buttonPress == true);
            buttonPress = false;
            NextNode("exit");         
        }
        if (dataParts[0] == "Stop")
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void NextNode (string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach (NodePort p in graph.current.Ports)
        {
            //Check if this port is the one we're looking for
            if (p.fieldName == fieldName)
            {
                graph.current = p.Connection.node as BaseNode;
                break;
            }
        }
        _parser = StartCoroutine(ParseNode());
    }
}