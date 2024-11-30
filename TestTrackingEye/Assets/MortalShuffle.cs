using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalShuffle : MonoBehaviour
{

    [SerializeField] SelectOnTime[] Selector;
    List<Transform> ankers = new List<Transform>();

    [SerializeField] GameObject pistill;
    [SerializeField] GameObject bowl;
    int[] possibleMove = new int[4] {1, 3, -1, - 3};

    [SerializeField] float duration = 1.25f;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (SelectOnTime i in Selector)
        {
            ankers.Add(i.gameObject.transform.GetChild(0).transform);
        }
        Shuffle();
    }
    private Vector2 GenerateNewPostions()
    {
        int bowlPostion = Random.Range(0, ankers.Count);
        int offsetPistfull = possibleMove[Random.Range(0, possibleMove.Length)];
        int pifulPostion = bowlPostion + offsetPistfull;

        // Validate
        if ((pifulPostion) < 0 | pifulPostion >= ankers.Count)
        {
            pifulPostion = bowlPostion + offsetPistfull*-1;
        }

        return new Vector2 (bowlPostion, pifulPostion);

    }
    public void Shuffle()
    {     
        Vector2 newPostions = GenerateNewPostions();
        bowl.transform.SetParent(ankers[(int)newPostions.x]);
        pistill.transform.SetParent(ankers[(int)newPostions.y]);

        // Reset
        foreach (SelectOnTime i in Selector)
        {
            i.SetValue(-1);
        }

        Selector[(int)newPostions.y].SetValue(1);
        Selector[(int)newPostions.x].SetValue(2);

        StartCoroutine(MoveBothObject());


    }
    public IEnumerator MoveBothObject()
    {
        StartCoroutine(MoveToLocalPositionZero(bowl.transform, pistill.transform, duration));
        StartCoroutine(MoveToLocalPositionZero(pistill.transform, bowl.transform, duration));
        yield return new WaitForSeconds(duration);
        CodeEventHandler.Trigger_SelectionSetBlock(true);
    }


    private IEnumerator MoveToLocalPositionZero(Transform child, Transform targetRot, float duration)
    {
        Vector3 startPosition = child.localPosition;
        Quaternion startRotation = child.localRotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            child.localPosition = Vector3.Lerp(startPosition, Vector3.zero, elapsed / duration);

            // child.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(targetRot.position - child.transform.position), elapsed / duration);

            yield return null;
        }

        child.localPosition = Vector3.zero; 
    }
}
