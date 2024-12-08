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
    int[] rotateConst = new int[4] { 90, 180, -90, -180 };

    [SerializeField] float duration = 1.25f;

    int lastValueBowlPostion = -1;
    int lastValuePistfullPull = -1;

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
        int randomValue;
        do
        {
           randomValue = Random.Range(0, ankers.Count);
        } while (randomValue == lastValueBowlPostion);

        int bowlPostion = randomValue;


        int offsetPistfull;
        int pifulPostion;

        do
        {
           offsetPistfull = possibleMove[Random.Range(0, possibleMove.Length)];
           pifulPostion = bowlPostion + offsetPistfull;

            // Validate
            if ((pifulPostion) < 0 | pifulPostion >= ankers.Count)
            {
                pifulPostion = bowlPostion + offsetPistfull*-1;
            }
        } while (pifulPostion == lastValuePistfullPull && pifulPostion == lastValueBowlPostion);

        lastValueBowlPostion = bowlPostion;
        lastValuePistfullPull = pifulPostion;

        return new Vector2 (bowlPostion, pifulPostion);

    }
    public void Shuffle()
    {     
        Vector2 newPostions = GenerateNewPostions();
       
        // Reset
        foreach (SelectOnTime i in Selector)
        {
            i.SetValue(-1);
        }

        Selector[(int)newPostions.y].SetValue(1);
        Selector[(int)newPostions.x].SetValue(2);

        StartCoroutine(MoveBothObject(ankers[(int)newPostions.x].position, ankers[(int)newPostions.y].position));


    }
    public IEnumerator MoveBothObject(Vector3 bowlNewPos,Vector3 pistillNewPos)
    {
        StartCoroutine(MoveToLocalPositionZero(bowl.transform, bowlNewPos, pistillNewPos, duration));
        StartCoroutine(MoveToLocalPositionZero(pistill.transform, pistillNewPos, bowlNewPos, duration));
        yield return new WaitForSeconds(duration);
        CodeEventHandler.Trigger_SelectionSetBlock(true);
    }


    private IEnumerator MoveToLocalPositionZero(Transform child, Vector3 targetPostion,Vector3 targetOfPair, float duration)
    {
        Vector3 startPosition = child.position;
        Quaternion startRotation = child.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            child.localPosition = Vector3.Lerp(startPosition, targetPostion, elapsed / duration);

            Vector3 directionToTarget = targetOfPair - child.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            child.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);


            yield return null;
        }
        child.position = targetPostion;
        child.LookAt(targetOfPair);

    }
}
