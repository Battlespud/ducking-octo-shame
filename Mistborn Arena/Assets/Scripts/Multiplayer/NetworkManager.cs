using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	
	
	
	
	
	
	
	private const string typeName = "Mistborn";
	private const string gameName = "Ruin";
	
	public GameObject playerPrefab;
	
	
	private HostData[] hostList;
	
	
	
	
	
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		DontDestroyOnLoad (this);
		//MasterServer.ipAddress = "127.0.0.1″;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
	
	//Methods
	//GUI
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
	
	//HOST
	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		Debug.Log ("Hosting server, game name is: " + gameName);
		Application.LoadLevel ("LevelSelect");
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		//SpawnPlayer ();
	}

	//JOIN
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		Application.LoadLevelAdditive("Arena");
		SpawnPlayer ();
	//	Application.LoadLevel ("minigame");
	}



	void SpawnPlayer(){
		Network.Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.identity, 0);
	}
	
	
	
	
	
	
	
	
	
	
	
}
