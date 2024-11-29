using UnityEngine;

public class FlowerNumber : MonoBehaviour 
{
    [SerializeField] Flowers flower;

    public Flowers Flower { get => flower; set => flower = value; }
}
