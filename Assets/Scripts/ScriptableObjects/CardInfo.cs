using UnityEngine;

[CreateAssetMenu(fileName = "New Card Info", menuName = "Card/CardInfo", order = 0)]
public class CardInfo : ScriptableObject
{
    [SerializeField] private Sprite _picture;
    [SerializeField] private Sprite _back;

    public Sprite Picture => _picture;
    public Sprite Back => _back;
}