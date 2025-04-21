using UnityEngine;

public class PlayerMoveIntent : MonoBehaviour
{
    public GameObject Up, Down, Left, Right;
    public GameObject HarvestUp, HarvestDown, HarvestLeft, HarvestRight;
    public GameObject TurnToUp, TurnToDown, TurnToLeft, TurnToRight;
    public void SetInent(GameObject obj)
    {
        NoInent();
        obj.SetActive(true);
    }
    public void NoInent()
    {
        Up.SetActive(false);
        Down.SetActive(false);
        Left.SetActive(false);
        Right.SetActive(false);
        HarvestUp.SetActive(false);
        HarvestDown.SetActive(false);
        HarvestLeft.SetActive(false);
        HarvestRight.SetActive(false);
        TurnToUp.SetActive(false);
        TurnToRight.SetActive(false);
        TurnToLeft.SetActive(false);
        TurnToDown.SetActive(false);
    }
}
