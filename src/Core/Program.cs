using System.IO;
using Godot;
using SFIUtils.Logging;
using Logger = SFIUtils.Logging.Logger;

namespace FreePlay.Core;

public partial class Program : Node
{
	// -- Static and Shorthand --
	/// <summary>
	/// The primary singleton.
	/// </summary>
	public static Program Instance { get; private set; }
	/// <summary>
	/// Static accessor for main log.
	/// </summary>
	public static Logger MainLog => Instance.Logger;
	/// <summary>
	/// Tells us if we're debugging so we can avoid bogging down performance in release mode.
	/// </summary>
	public static bool DebugMode => Instance.debugMode;

	// -- Props --
	public Logger Logger { get; private set; }
	
	// -- Fields --
	FileManager fileManager;
	NetworkManager networkManager;
	AudioStreamPlayer player;
	bool debugMode;
	
	// -- Exports --
	[ExportSubgroup("Debug")] object crlf;

	public Program()
	{
		if (!SetSingleton()) return;


		if (File.Exists(".editorconfig")){debugMode = true;}
		
		// Mode Independent Initialization
		Logger = new Logger();
		
		fileManager = new FileManager();
		networkManager = new NetworkManager();
		player = new AudioStreamPlayer();
		
		AddChild(player);

		if (debugMode) InitDebug();
		else InitRelease();
		
		Logger.Log("Init done.");
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	#region Init
	
	bool SetSingleton()
	{
		if (Instance is null) { Instance = this; return true; }

		Free(); return false;
	}

	void InitDebug()
	{
		Logger.MinimumFileLogLevel = LogLevel.Debug;
		Logger.SendToConsole = true;
		Logger.UseDateStamps = false;

		Ready += DebugPostInit;

		void DebugPostInit()
		{
			player.Stream = fileManager.DebugSongInfo().clip;
			player.Play();
		}
	}

	void InitRelease()
	{
		Logger.MinimumFileLogLevel = LogLevel.Warning;
		Logger.SendToConsole = false;
	}

	#endregion
}