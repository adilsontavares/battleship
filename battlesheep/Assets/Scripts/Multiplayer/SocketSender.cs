public class SocketSender
{
    public delegate void LoginCallback();
    public delegate void RegisterCallback();
	public delegate void SendInitialBoardCallback();

    public LoginCallback OnLogin;
    public RegisterCallback OnRegister;
	public SendInitialBoardCallback OnSendInitialBoard;

	public void Login(string username, string password)
	{
		var message = new SocketMessage(SocketMessage.Code.Login);
		message.AddString(username, 64);
		message.AddString(password, 64);

		SocketManager.Main.Send(message);

		if (OnLogin != null)
			OnLogin();
	}

	public void Register(string username, string password)
	{
		var message = new SocketMessage(SocketMessage.Code.Register);
		message.AddString(username, 64);
		message.AddString(password, 64);

		SocketManager.Main.Send(message);

		if (OnRegister != null)
			OnRegister();
	}

	public void SendInitialBoard(Board board)
	{
		var message = new SocketMessage(SocketMessage.Code.SendInitialBoard);
		var items = board.Items;

		for (int i = 0; i < items.Count; ++i)
		{
			var item = items[i];
			message.AddByte((byte)item.Index.I);
			message.AddByte((byte)item.Index.J);
			message.AddByte((byte)(item.Direction == BoardItemDirection.Horizontal ? 1 : 0));
			message.AddByte((byte)item.Size);
		}

		SocketManager.Main.Send(message);

		if (OnSendInitialBoard != null)
			OnSendInitialBoard();
	}
}