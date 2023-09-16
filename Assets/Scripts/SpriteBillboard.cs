// Key code from this tutorial: https://www.youtube.com/watch?v=FjJJ_I9zqJo
// Paper Mouse Games - Unity Billboard Sprite script

using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool FreezeXZAxis = false;

    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        
        if(FreezeXZAxis){
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        } else {
            transform.rotation = Camera.main.transform.rotation;
        }
        
    }

    public void SetSprite(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            spriteRenderer.sprite = sprites[index];
        }
    }
    
}
