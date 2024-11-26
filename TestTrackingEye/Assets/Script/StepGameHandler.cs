using UnityEngine;

public class StepGameHandler : MonoBehaviour
{
    public int score;

    virtual public void ChnageValue(int value)
    {
        score = value;
    }
    virtual public int GetValue()
    {
        return score;
    }
    virtual public void GetSelection(int i)
    {
        score = i;
      
    }

    virtual public void EndStep()
    {

    }
}
