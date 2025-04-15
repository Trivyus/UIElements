using UnityEngine;

public class InterfaceSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _objectToClose;
    [SerializeField] private GameObject _objectlToOpen;

    public void Open()
    {
        _objectlToOpen.SetActive(true);
        _objectToClose.SetActive(false);
    }

    public void Close()
    {
        _objectlToOpen.SetActive(false);
        _objectToClose.SetActive(true);
    }
}
