using UnityEngine;
using Unity.LiveCapture.CompanionApp;

public class LiveCaptureServer : MonoBehaviour
{
    private CompanionAppServer _server;
    bool starting = true;

    void Awake()
    {
        LiveCaptureServer[] objs = FindObjectsByType<LiveCaptureServer>(FindObjectsSortMode.None);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        if (!starting)
        {
            _server.SetEnabled(true);
        }
       
    }


    void Start()
    {
        // Direkt eine Instanz des CompanionAppServer erstellen
        _server = new CompanionAppServer();
        _server.Port = 9000;
        _server.StartServer();
        Debug.Log("Server gestartet");
        starting = false;
    }

    void Update()
    {
        if (_server != null)
        {
            _server.OnUpdate();
        }
    }

    void OnDestroy()
    {
        if (_server != null)
        {
            _server.Dispose();
            Debug.Log("Server gestoppt");
        }
    }
    void OnDisable()
    {
        _server.SetEnabled(false);   
    }
}

