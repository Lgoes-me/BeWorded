using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScrollTexture : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Vector2 speed;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.material = new Material(_image.material); //Clone the original material
    }

    void FixedUpdate()
    {
        _image.material.mainTextureOffset += speed * Random.Range(-0.2f, 1f) * Time.deltaTime;
    }
}