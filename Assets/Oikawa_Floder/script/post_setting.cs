using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class post_setting : MonoBehaviour
{
    public PostProcessVolume volume;
    public ColorGrading post;
    public float H;                            //ライトの色、色相を変える
    public float S;　　　　　　　　　   //ライトの色、彩度を変える
    public float V;                        //ライトの色、白黒をどれだけ混ぜるかを決める数値、基本的に上記三つでライトの色を制御する、従来のRGBとは違う
    
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
