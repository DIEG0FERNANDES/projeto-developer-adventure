using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
	[Header( "Referencia para o Dropdown de Resoluções" )]
	public TMP_Dropdown resolutionDropdown;
	private Resolution[] resolutions;

	[Header( "Referencia para o Dropdown de Controles" )]
	public TMP_Dropdown controlesDropdown;

	void Start()
	{
		InitializeResolutionsDropdown();
		InitializeControlsDropdown();
	}

	private void InitializeResolutionsDropdown()
	{
		resolutions = Screen.resolutions;

		if ( resolutionDropdown == null )
		{
			Debug.LogError( "TMP_Dropdown não atribuído no Inspector." );
			return;
		}

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();
		int currentResolutionIndex = 0;

		for ( int i = 0; i < resolutions.Length; i++ )
		{
			string option = resolutions[i].width + "x" + resolutions[i].height;
			options.Add( option );

			if ( resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height )
			{
				currentResolutionIndex = i;
			}
		}

		if ( options.Count == 0 )
		{
			Debug.LogError( "Nenhuma opção de resolução encontrada." );
			return;
		}

		resolutionDropdown.AddOptions( options );
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();

		Debug.Log( "Resoluções carregadas com sucesso." );
	}

	private void InitializeControlsDropdown()
	{
		if ( controlesDropdown == null )
		{
			Debug.LogError( "TMP_Dropdown de controles não atribuído no Inspector." );
			return;
		}

		List<string> controlOptions = new List<string> { "Keyboard", "Gamepad/Joystick" };
		controlesDropdown.ClearOptions();
		controlesDropdown.AddOptions( controlOptions );
		controlesDropdown.value = 0; // Padrão para Keyboard
		controlesDropdown.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex)
	{
		if ( resolutionIndex < 0 || resolutionIndex >= resolutions.Length )
		{
			Debug.LogError( "Índice de resolução inválido." );
			return;
		}

		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution( resolution.width, resolution.height, Screen.fullScreen );
		Debug.Log( $"Resolução definida para: {resolution.width}x{resolution.height}" );
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		Debug.Log( $"Modo de tela cheia definido para: {isFullscreen}" );
	}

	public void SetControlLayout(int controlIndex)
	{
		string selectedControl = controlesDropdown.options[controlIndex].text;
		Debug.Log( $"Layout de controle definido para: {selectedControl}" );
		// Aqui você pode adicionar lógica adicional para ativar o layout de controle apropriado
	}
}
