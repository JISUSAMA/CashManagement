
using UnityEngine;
using UnityEngine.UI;

public class CameraResolution : MonoBehaviour
{
    /*   Camera camera;
        private void Awake()
        {
            Camera.main.aspect = 16f / 9f;
           // Camera_Resolution();
       }

       void OnPreCull() => GL.Clear(true, true, Color.black);

        //ī�޶� 9:16 ������ ���͹ڽ� ���ִ� �Լ�
        void Camera_Resolution()
        {
           camera = GetComponent<Camera>();

            //ī�޶� ������Ʈ�� Viewport Rect
            Rect rt = camera.rect;

            //���θ�� 16:9
            float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
            float scale_width = 1f / scale_height;

            if (scale_height < 1)
            {
                rt.height = scale_height;
                rt.y = (1f - scale_height) / 2f;
            }
            else
            {
                rt.width = scale_width;
                rt.x = (1f - scale_width) / 2f;
            }

            camera.rect = rt;
        }*/
    public Camera camera_cam;
    public CanvasScaler[] Canvases;
    void OnPreCull() => GL.Clear(true, true, Color.black);
    private void Start()
    {
        //  SetResolution(); // �ʱ⿡ ���� �ػ� ����
        //Camera2View();
        SetCameraAsect();
    }

    // �ػ� �����ϴ� �Լ� 
    public void SetResolution()
    {
        int setWidth = 1080; // ����� ���� �ʺ�
        int setHeight = 2120; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            camera_cam.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {

            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            camera_cam.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

    public void Camera2View()
    {
        Rect rt = camera_cam.rect;

        float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);   //����
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
        }
        else
        {
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 0;
            }
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        camera_cam.rect = rt;
    }
    public void SetCameraAsect()
    {

        float targetaspect = 1080.0f / 2120.0f;
        float windowaspect = 0;

#if UNITY_EDITOR
        windowaspect = (float)camera_cam.pixelWidth / (float)camera_cam.pixelHeight;
#else
            windowaspect = (float)Screen.width / (float)Screen.height;
#endif
        float scaleheight = windowaspect / targetaspect;
        //      Camera camera = UnityEngine.Camera.main;

        if (scaleheight < 1.0f)
        {
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 0;
            }
            Rect rect = camera_cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera_cam.rect = rect;
        }
        else
        { // add pillarbox 
            for (int i = 0; i < Canvases.Length; i++)
            {
                Canvases[i].matchWidthOrHeight = 1;
            }
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera_cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera_cam.rect = rect;
        }
    }

  /*  private void Update()
    {
        //  if (Input.GetMouseButtonDown(0))
        //  {
        //      SoundFunction.Instance.TouchScreen_sound();
        //  }
        //
        //#if UNITY_ANDROID || UNITY_IOS
        //
        //        // Touch myTouch = Input.GetTouch(0);
        //        Touch[] myTouches = Input.touches;
        //        if (Input.touchCount > 0)
        //        {
        //            if (Input.touchCount == 0)
        //            {
        //                SoundFunction.Instance.TouchScreen_sound();
        //            }
        //        }
        //#endif  
    }*/
    //  private void Creat_touch()
    //  {
    //      Vector3 mPosition = ca.ScreenToWorldPoint(Input.mousePosition);
    //      mPosition.z = 100;
    //      //  Debug.Log(mPosition);
    //      Debug.DrawLine(Vector3.zero, mPosition, Color.red);
    //     
    //  }
    //
    //  private void Creat_touch(Vector3 _touchPos)
    //  {
    //      Vector3 mPosition = ca.ScreenToWorldPoint(_touchPos);
    //      mPosition.z = 100;
    //      //   Debug.Log(mPosition);
    //      Debug.DrawLine(Vector3.zero, mPosition, Color.red);
    //  }
}
