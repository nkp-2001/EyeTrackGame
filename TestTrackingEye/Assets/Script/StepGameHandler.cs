using UnityEngine;

public class StepGameHandler : MonoBehaviour
{
    int score;

    virtual public void ChnageValue(int value)
    {
        score = value;
    }
    virtual public int GetValue()
    {
        return score;
    }

    virtual public void EndStep()
    {

    }
}
