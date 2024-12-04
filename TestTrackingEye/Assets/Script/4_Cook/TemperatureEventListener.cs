using UnityEngine;

public class TemperatureEventListener : StepGameHandler
{
    [SerializeField] TemperatureController temperatureController; // Referenz zur Temperatursteuerung
    public int stepsToDo; // Anzahl der Schritte, die ausgeführt werden müssen
    private float selectionTimeout = 0.2f; // Zeit, nach der die Temperaturänderung stoppt
    public float selectionTimer = 0f; // Zählt die Zeit seit der letzten Auswahl
    public float timeOutsideOptimal = 0f; // Zeit außerhalb des optimalen Bereichs
    public float timeInsideOptimal = 0f; // Zeit innerhalb des optimalen Bereichs
    public const float penaltyTime = 10f; // Zeit, nach der außerhalb des Bereichs ein Punkt abgezogen wird
    public const float successTime = 13f; // Zeit, die im optimalen Bereich verbracht werden muss, um zu gewinnen
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

        // Überprüfe die aktuelle Temperatur
        if (temperatureController.currentTemperature < temperatureController.optimalMin ||
            temperatureController.currentTemperature > temperatureController.optimalMax)
        {
            timeOutsideOptimal += Time.deltaTime;


            if (timeOutsideOptimal >= penaltyTime)
            {
                score++;
                timeOutsideOptimal = 0f; // Zurücksetzen
               
            }
        }
        else
        {
            timeInsideOptimal += Time.deltaTime;
            

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
        // Setze den Timer zurück, da eine neue Auswahl erfolgt ist
        selectionTimer = selectionTimeout;

        // Reagiere auf das Event basierend auf dem Wert
        if (value == 0)
        {
            // Temperatur erhöhen
            temperatureController.IncreaseTemperature(true);
            temperatureController.DecreaseTemperature(false); // Sicherstellen, dass Senken gestoppt wird
        }
        else if (value == 1)
        {
            // Temperatur verringern
            temperatureController.DecreaseTemperature(true);
            temperatureController.IncreaseTemperature(false); // Sicherstellen, dass Erhöhen gestoppt wird
        }
        else
        {
            StopTemperatureChange();
        }
    }

    private void StopTemperatureChange()
    {
        // Stoppe sowohl das Erhöhen als auch das Verringern der Temperatur
        temperatureController.IncreaseTemperature(false);
        temperatureController.DecreaseTemperature(false);
    }

    public override void EndStep()
    {
        Debug.Log("Temperatur-Minigame abgeschlossen!");
        CodeEventHandler.Trigger_NextStepInRecipe(new RecipeStep(true, score)); // Rezept-Schritt beenden
    }
}
