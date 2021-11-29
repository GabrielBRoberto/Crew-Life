using UnityEngine;

[CreateAssetMenu(fileName = "Assento", menuName = "Tipos/Assento", order = 1)]
[System.Serializable]
public class Assento : ScriptableObject
{
    [Header("Objeto 3D")]
    public GameObject objeto3D;
    [Header("Passageiro GameObject")]
    public GameObject passageiroGameObject;
}
