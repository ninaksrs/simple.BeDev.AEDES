using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class SendReceiveMessageExample : SimulationManager
{
 GAMAMessage message = null;
    public int score;
    public Vector3 pos;
    protected override void ManageOtherMessages(string content)
 {
     message = GAMAMessage.CreateFromJSON(content);
 }

//action activated at the end of the update phase (every frame)
 protected override void OtherUpdate()
 {
        score = GameManager.instance.score;
     if (IsGameState(GameState.GAME))
     {
            string mes = transform.position.ToString() ;
            //call the action "receive_message" from the unity_linker agent with two arguments: the id of the player and a message
            Dictionary<string, string> args = new Dictionary<string, string> {
         {"id",ConnectionManager.Instance.GetConnectionId() },
             { "score", score.ToString()},
         {"mes",  mes }};

            Debug.Log("sent to GAMA: " + mes);
         ConnectionManager.Instance.SendExecutableAsk("receive_message", args);
     }
     if (message != null)
        {
            Debug.Log("received from GAMA: cycle " + message.cycle);
            message = null;
        }


 }


}


[System.Serializable]
public class GAMAMessage
{

   
    public int cycle;

    public static GAMAMessage CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GAMAMessage>(jsonString);
    }

}
