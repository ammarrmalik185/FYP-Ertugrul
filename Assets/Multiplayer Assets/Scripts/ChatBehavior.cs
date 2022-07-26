using Mirror;
using System;
using Managers.Scripts;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehavior : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject canvas = null;

    private static event Action<string> OnMessage;

    // Called when the a client is connected to the server
    public override void OnStartAuthority(){
        canvas = GameDirector.getInstance().uiManager.chatWindow;
        inputField = GameDirector.getInstance().uiManager.chatInputField;
        chatText = GameDirector.getInstance().uiManager.chatTextView;

        inputField.onEndEdit.AddListener(msg=> Send());
        
        inputField.Select();
        inputField.ActivateInputField();
        
        canvas.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    // Called when a client has exited the server
    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    // When a new message is added, update the Scroll View's Text to include the new message
    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    // When a client hits the enter button, send the message in the InputField
    [Client]
    public void Send()
    {
        if(!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command (requiresAuthority = false)]
    private void CmdSendMessage(string message)
    {
        // Validate message
        RpcHandleMessage($"{SteamFriends.GetFriendPersonaName(MultiplayerManager.Instance.GetPlayerId(connectionToClient.connectionId))}: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
    
    [ClientRpc]
    private void RpcHandleMessageTeam(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }

}
