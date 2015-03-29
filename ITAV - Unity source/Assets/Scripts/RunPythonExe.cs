using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class RunPythonExe : MonoBehaviour
{
	public Text userName;
	public InputField oAuthToken;
	public string scene;
	private string correctedFileLocation;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OpenExe()
	{
		try
		{
			// Get the file location of the program
			correctedFileLocation = Application.dataPath;

			// If we're debugging, find the assets folder.
			if (correctedFileLocation.Contains("/Assets"))
				correctedFileLocation = correctedFileLocation.Remove(correctedFileLocation.IndexOf("/Assets"));

			// If we're built, find the ItTakesAVillage_Data folder.
			else if (correctedFileLocation.Contains("/ItTakesAVillage_Data"))
				correctedFileLocation = correctedFileLocation.Remove(correctedFileLocation.IndexOf("/ItTakesAVillage_Data"));

			// For some reason, unity doesn't like forward slashes in the file location.
			correctedFileLocation = correctedFileLocation.Replace("/", "\\");
			correctedFileLocation += "\\TPD dist\\";

			// Open the exe with the parameters inputted in the UI.
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo.FileName = correctedFileLocation + "main.exe";
			process.StartInfo.Arguments = userName.text + " " + oAuthToken.text;
			process.Start();

		}
		catch
		{
			print("Could not load python file.");
		}
	}

	public void RunGame()
	{
		Application.LoadLevel(scene);
	}
}
