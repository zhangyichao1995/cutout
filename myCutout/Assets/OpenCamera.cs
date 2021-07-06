using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class OpenCamera : MonoBehaviour
{
    //摄像头图像类，继承自texture
    WebCamTexture tex;
    public GameObject raw_Image;
    RawImage cam_Video;
    public string deviceName;

    // Start is called before the first frame update
    void Start()
    {
        //找到需要将图像显示的物体
        cam_Video = raw_Image.GetComponent<RawImage>();
        //开启协程，获取摄像头图像数据
        StartCoroutine(OpenCam());
    }

    IEnumerator OpenCam()
    {
        //等待用户允许访问
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //如果用户开始访问，开始获取图像
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //先获取设备
            WebCamDevice[] device = WebCamTexture.devices;
            deviceName = device[0].name;
            //然后获取图像
            tex = new WebCamTexture(deviceName, Screen.width, Screen.height, 50);
            //将获取的图像赋值
            cam_Video.texture = tex;
            //开始实施获取
            tex.Play();
        }
    }

}
