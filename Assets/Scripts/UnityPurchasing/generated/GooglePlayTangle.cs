// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XpwFKzdRoc5Ns8heQw2ROIy0Hitd72xPXWBrZEfrJeuaYGxsbGhtbjQoj3mo7FjlLD3Xmv4IjtFF50Gq6Ty+bbXG9wyqTcUWxmkLPRKWI5PvbGJtXe9sZ2/vbGxtzDB1bN9kIY1JNfDIffxPYf3qSI/gkjsvnWku16RI6RQoatBR8EG7VLeTXs96XG7kZTAV8LQgR4m7hEEv0QjA1BZBZPsrHKI/qD0oYYlnhldRIS2obe9ng9j4tOIHMeVdi47f+jU8ZWzInSYxGCVCuNjN4SRVI3s6P/GYXmQbiqM02ULbG1Wp5G/wHkdKP/cY+PDTuW+8iUVLFapBr5Z0fK4ZNSD1QNYdNzkDhgV81Xiyduae9vPOdqEJKGL2sxXgXYVEMm9ubG1s");
        private static int[] order = new int[] { 4,4,3,11,4,9,13,7,10,9,12,11,12,13,14 };
        private static int key = 109;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
