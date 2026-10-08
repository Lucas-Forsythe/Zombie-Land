using UnityEngine;
using UnityEngine.UI;

public class MenuGIfMaker : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float swapTime = 0.5f;

    private Image image;
    private float timer;
    private bool useSprite1 = true;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= swapTime)
        {
            timer = 0f;

            if (useSprite1)
            {
                image.sprite = sprite2;
                useSprite1 = false;
            }
            else
            {
                image.sprite = sprite1;
                useSprite1 = true;
            }
        }
    }
}