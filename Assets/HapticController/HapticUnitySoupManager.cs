#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public enum NotificationFeedback
{
    Success,
    Warning,
    Error
}

public enum ImpactFeedback
{
    Light,
    Medium,
    Heavy
}

public static class HapticUnitySoupManager
{
    /// <summary>
    /// Gives the player haptic feedback if the device and OS being played on supports it, if not it does a normal vibration
    /// </summary>
    public static void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(AndroidHapticManager.HapticFeedback());
#elif UNITY_IOS && !UNITY_EDITOR
        Vibrate(Impact(ImpactFeedback.Medium));
#else
        VibrateUnity();
#endif
    }

    /// <summary>
    /// An overloaded method where you can change the impact feedback type
    /// </summary>
    /// <param name="impactFeedback">Impact feedback type: Light/Medium/Heavy</param>
    public static void Vibrate(ImpactFeedback impactFeedback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(AndroidHapticManager.HapticFeedback());
#elif UNITY_IOS && !UNITY_EDITOR
        Vibrate(Impact(impactFeedback));
#else
        VibrateUnity();
#endif
    }

    /// <summary>
    /// An overloaded method where you can change the notification feedback type
    /// </summary>
    /// <param name="notificationFeedback">Notification feedback type: Success/Warning/Error</param>
    public static void Vibrate(NotificationFeedback notificationFeedback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibrate(AndroidHapticManager.HapticFeedback());
#elif UNITY_IOS && !UNITY_EDITOR
        Vibrate(Notification(notificationFeedback));
#else
        VibrateUnity();
#endif
    }

    /// <summary>
    /// Standard device vibration
    /// </summary>
    private static void VibrateUnity()
    {
        UnityEngine.Handheld.Vibrate();
    }

    //private static void Vibrate(bool isHapticSupported)
    //{
    //    if (!isHapticSupported)
    //        VibrateUnity();
    //}


    private static bool Notification(NotificationFeedback feedback = NotificationFeedback.Success)
    {
        return _unityHapticNotification((int)feedback);
    }

    private static bool Impact(ImpactFeedback feedback = ImpactFeedback.Medium)
    {
        return _unityHapticImpact((int)feedback);
    }

    #region DllImport

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern bool _unityHapticNotification(int type);
    [DllImport("__Internal")]
    private static extern bool _unityHapticImpact(int style);
#else
    private static bool _unityHapticNotification(int type) { return false; }
    private static bool _unityHapticImpact(int style) { return false; }
#endif

    #endregion
}