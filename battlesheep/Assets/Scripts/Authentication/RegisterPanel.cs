using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : Panel 
{
	[SerializeField]
	private InputField _usernameField;

	[SerializeField]
	private InputField _passwordField;

	[SerializeField]
	private Button _button;

	[SerializeField]
	private Button _loginButton;

	void Start()
	{
		SocketManager.Main.Listener.OnRegister += OnRegisterResponse;
	}

	void OnRegisterResponse(SocketMessage message)
	{
		var success = message.GetBool(1);

		if (success)
		{
			MessageBoxManager.Show("Cadastro realizado com sucesso!", "Faça seu login agora mesmo.");
		}
		else
		{
			MessageBoxManager.Show("Falha no cadastro", "Usuário existente ou dados inválidos. Por favor, tente novamente.");

			SetInputEnabled(true);
			_button.GetComponentInChildren<Text>().text = "LOGIN";
		}
	}

	public void Register()
	{
		SetInputEnabled(false);
		_button.GetComponentInChildren<Text>().text = "...";

		SocketManager.Main.Sender.Register(_usernameField.text, _passwordField.text);
	}

	void SetInputEnabled(bool enabled)
	{
		_usernameField.enabled = enabled;
		_passwordField.enabled = enabled;
		_button.enabled = enabled;
		_loginButton.enabled = enabled;
	}
}
