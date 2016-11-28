using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class SocketManager : MonoBehaviour 
{
	[SerializeField]
	private string _ipAddress = "127.0.0.1";

	[SerializeField]
	private int _port = 9191;

	private TcpClient _client;
	private NetworkStream _stream;
	private bool _connected = false;

	private SocketListener _listener;
	public SocketListener Listener { get { return _listener; } }

	private SocketSender _sender;
	public SocketSender Sender { get { return _sender; } }

	[SerializeField]
	private Panel _connectionDownPanel;

	public static SocketManager Main { get { return FindObjectOfType<SocketManager>(); } }

	public delegate void Callback();

	// Callbacks
	public Callback OnRequestLogin; 
	public Callback OnRequestRegister; 

	void Awake()
	{
		_sender = new SocketSender();
		_listener = new SocketListener();	
	}

	void Start()
	{
		_client = new TcpClient();
		
		if (!Connect())
			OnConnectionDown();
	}

	void OnDisconnect()
	{
		QuitApplication();
	}

	public void QuitApplication()
	{
		Application.Quit();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}

	public bool Connect()
	{
		if (_client.Connected)
			return true;

		try 
		{
			_client.Connect(_ipAddress, _port);
			_stream = _client.GetStream();
			
			return true;
		}
		catch (Exception) 
		{
			return false;
		}
	} 

	void Update()
	{
		if (_connected && !_client.Connected)
		{
			_connected = false;
			OnConnectionDown();
		}

		if (_stream != null && _stream.DataAvailable)
			ReadStream();
	}

	void ReadStream()
	{
		var buffer = new byte[1024];

		var count = _stream.Read(buffer, 0, buffer.Length);
		if (count <= 0)
			return;

		var data = new byte[count];
		for (int i = 0; i < count; ++i)
			data[i] = buffer[i];

		DidReceiveData(data);
	}
	
	void DidReceiveData(byte[] data)
	{
		var message = new SocketMessage(data);

		Debug.Log("Mensagem recebida: " + message.Command + ".");

		_listener.Handle(message);
	}

	void OnConnectionDown()
	{
		_connectionDownPanel.Show();
	}

	public void Send(SocketMessage message)
	{
		var data = message.GetData();
		var e = new SocketAsyncEventArgs();

		Debug.Log("Mensagem " + message.Command + " enviada.");

		e.SetBuffer(data, 0, data.Length);
		_client.Client.SendAsync(e);
	}
}
