namespace Utils
{
    public static class UT
    {
        public static void QuitGame()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // This line runs in the built application (Windows, Mac, mobile, etc.)
            Application.Quit();
#endif
        }
    }
}