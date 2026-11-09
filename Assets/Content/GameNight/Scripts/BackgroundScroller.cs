using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] RawImage _background;
    [SerializeField] float _scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        _background.uvRect = new Rect(_background.uvRect.position + new Vector2(_scrollSpeed, 0f) * Time.deltaTime, _background.uvRect.size);
    }
}
