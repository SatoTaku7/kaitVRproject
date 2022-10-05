using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class post_setting : MonoBehaviour
{
    public PostProcessVolume volume;
    public ColorGrading post;
    public float H;                            //���C�g�̐F�A�F����ς���
    public float S;�@�@�@�@�@�@�@�@�@   //���C�g�̐F�A�ʓx��ς���
    public float V;                        //���C�g�̐F�A�������ǂꂾ�������邩�����߂鐔�l�A��{�I�ɏ�L�O�Ń��C�g�̐F�𐧌䂷��A�]����RGB�Ƃ͈Ⴄ
    
    // Start is called before the first frame update
    void Start()
    {
        volume = transform.GetComponent<PostProcessVolume>();
        //var post2 = volume.profile.settings;
        //post = post2 as ColorGrading;

        foreach (PostProcessEffectSettings item in volume.profile.settings)
        {
            if (item as ColorGrading)
            {
                post = item as ColorGrading;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        post.colorFilter.Override(Color.HSVToRGB(H, S, V));

    }

}
