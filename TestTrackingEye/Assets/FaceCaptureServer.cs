using UnityEngine;
using Unity.LiveCapture.CompanionApp;

public class LiveCaptureServer : MonoBehaviour
{
    private CompanionAppServer _server;

    void Start()
    {
        // Direkt eine Instanz des CompanionAppServer erstellen
        _server = new CompanionAppServer();
        _server.Port = 9000;
        _server.StartServer();
        Debug.Log("Server gestartet");
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
}

