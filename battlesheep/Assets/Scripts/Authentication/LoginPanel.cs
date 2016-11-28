using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : Panel 
{
	[SerializeField]
	private InputField _usernameField;

	[SerializeField]
	private InputField _passwordField;

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Button _registerButton;

	void Start()
	{
		SocketManager.Main.Listener.OnLogin += OnLoginResponse;
	}

	void OnLoginResponse(SocketMessage message)
	{
		var success = message.GetBool(1);

		if (success)
		{
			var messageBox = MessageBoxManager.Show("Login realizado com sucesso!", "O jogo será iniciado em breve.");
			messageBox.OnButtonClick += delegate() 
			{
				SceneManager.LoadScene("Board Preparation");
			};
		}
		else
		{
			MessageBoxManager.Show("Falha no login", "Confira seus dados e tente novamente.");

			SetInputEnabled(true);
			_button.GetComponentInChildren<Text>().text = "LOGIN";
		}
	}

	public void Login()
	{
		SetInputEnabled(false);
		_button.GetComponentInChildren<Text>().text = "...";

		SocketManager.Main.Sender.Login(_usernameField.text, _passwordField.text);
	}

	void SetInputEnabled(bool enabled)
	{
		_usernameField.enabled = enabled;
		_passwordField.enabled = enabled;
		_button.enabled = enabled;
		_registerButton.enabled = enabled;
	}
}
