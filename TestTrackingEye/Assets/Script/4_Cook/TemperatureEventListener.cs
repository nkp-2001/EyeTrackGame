using UnityEngine;

public class TemperatureEventListener : StepGameHandler
{
    [SerializeField] TemperatureController temperatureController; // Referenz zur Temperatursteuerung
    [SerializeField] private GameObject Steam; // Dampf-Objekt
    [SerializeField] private GameObject Spark; // Funken-Objekt

    public int stepsToDo; // Anzahl der Schritte, die ausgef�hrt werden m�ssen
    private float selectionTimeout = 0.2f; // Zeit, nach der die Temperatur�nderung stoppt
    public float selectionTimer = 0f; // Z�hlt die Zeit seit der letzten Auswahl
    public float timeOutsideOptimal = 0f; // Zeit au�erhalb des optimalen Bereichs
    public float timeInsideOptimal = 0f; // Zeit innerhalb des optimalen Bereichs
    public float penaltyTime = 15f; // Zeit, nach der au�erhalb des Bereichs ein Punkt abgezogen wird
    public float successTime = 10f; // Zeit, die im optimalen Bereich verbracht werden muss, um zu gewinnen
    public bool iswinning;

    private void Start()
    {
        RecipeMangment recipeMangment = FindAnyObjectByType<RecipeMangment>();
        if (recipeMangment != null)
        {
            stepsToDo = recipeMangment.GetCurrentStepData().Value;
        }
        else
        {
            stepsToDo = 1; // Fallback-Wert
        }

        // Initiale Status der Objekte setzen
        SetObjectStatus(false);
    }

    private void OnEnable()
    {
        // Registriere das Event
        CodeEventHandler.BasicSelection += HandleBasicSelection;
    }

    private void OnDisable()
    {
        // Entferne das Event
        CodeEventHandler.BasicSelection -= HandleBasicSelection;
    }

    private void Update()
    {
        // Wenn keine neue Auswahl stattgefunden hat, verringere den Timer
        if (selectionTimer > 0)
        {
            selectionTimer -= Time.deltaTime;
        }
        else
        {
            StopTemperatureChange();
        }

        // �berpr�fe die aktuelle Temperatur
        if (temperatureController.currentTemperature < temperatureController.optimalMin ||
            temperatureController.currentTemperature > temperatureController.optimalMax)
        {
            timeOutsideOptimal += Time.deltaTime;
            SetObjectStatus(false); // Deaktiviere Objekte, wenn au�erhalb des optimalen Bereichs

            if (timeOutsideOptimal >= penaltyTime)
            {
                score++;
                timeOutsideOptimal = 0f; // Zur�cksetzen
            }
        }
        else
        {
            timeInsideOptimal += Time.deltaTime;
            SetObjectStatus(true); // Aktiviere Objekte, wenn im optimalen Bereich

            if (timeInsideOptimal >= successTime && !iswinning)
            {
                iswinning = true;
                stepsToDo--;
                if (stepsToDo <= 0)
                {
                    Debug.Log("Temperatur-Minigame abgeschlossen!");
                    EndStep();
                }
            }
        }
    }

    private void HandleBasicSelection(int value)
    {
        // Setze den Timer zur�ck, da eine neue Auswahl erfolgt ist
        selectionTimer = selectionTimeout;

        // Reagiere auf das Event basierend auf dem Wert
        if (value == 0)
        {
            // Temperatur erh�hen
            Debug.Log("Temperatur erh�hen");
            temperatureController.IncreaseTemperature(true);
            temperatureController.DecreaseTemperature(false); // Sicherstellen, dass Senken gestoppt wird
                                                              // Zus�tzliche Debug-Ausgabe
            Debug.Log($"isIncreasing: {temperatureController.isIncreasing}");
        }
        if (value == 1)
        {
            // Temperatur verringern
            Debug.Log("Temperatur verringern");
            temperatureController.DecreaseTemperature(true);
            temperatureController.IncreaseTemperature(false); // Sicherstellen, dass Erh�hen gestoppt wird
        }
        
    }

    private void StopTemperatureChange()
    {
        // Stoppe sowohl das Erh�hen als auch das Verringern der Temperatur
        temperatureController.IncreaseTemperature(false);
        temperatureController.DecreaseTemperature(false);
    }

    private void SetObjectStatus(bool isActive)
    {
        if (Steam != null) Steam.SetActive(isActive);
        if (Spark != null) Spark.SetActive(isActive);
    }

    public override void EndStep()
    {
        Debug.Log("Temperatur-Minigame abgeschlossen!");
        CodeEventHandler.Trigger_NextStepInRecipe(new RecipeStep(true, score)); // Rezept-Schritt beenden
    }
}
