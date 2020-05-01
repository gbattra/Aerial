using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller _controller;
    public Controller Controller => _controller;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = new Controller();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_controller.LeftStickHorizontal);
        Debug.Log(_controller.LeftStickVertical);
    }
}
