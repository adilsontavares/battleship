using UnityEngine;

public class SocketListener
{
    public delegate void ResponseCallback(SocketMessage message);

    public ResponseCallback OnLogin;
    public ResponseCallback OnRegister;
    public ResponseCallback OnStartGame;

    public void Handle(SocketMessage message)
    {
        switch (message.Command)
        {
            case SocketMessage.Code.LoginResponse:
                LoginResponse(message);
                break;

            case SocketMessage.Code.RegisterResponse:
                RegisterResponse(message);
                break;

            case SocketMessage.Code.StartGame:
                StartGame(message);
                break;

            default:
                OnInvalidMessage(message);
                break;
        }
    }

    void OnInvalidMessage(SocketMessage message)
    {
        byte code = (byte)message.Command;
        Debug.LogWarning("Received invalid message with code " + code);
    }

    void StartGame(SocketMessage message)
    {
        if (OnStartGame != null)
            OnStartGame(message);
    }

    void RegisterResponse(SocketMessage message)
    {
        if (OnRegister != null)
            OnRegister(message);
    }

    void LoginResponse(SocketMessage message)
    {
        if (OnLogin != null)
            OnLogin(message);
    }
}