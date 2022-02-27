using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CalebCodeLibrary.ImageDownloader
{
    public static class ImageDownloader
    {
        public static async Task<Texture2D> DownloadImageAsync(string imageUrl)
        {
            UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(imageUrl);

            UnityWebRequestAsyncOperation requestAsync = unityWebRequest.SendWebRequest();

            while (!requestAsync.isDone)
                await Task.Delay(10); // checks every 0.01s
            
            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(unityWebRequest);
            }
            else
            {
                Debug.LogError($"ImageDownloader: Error getting image from {unityWebRequest.url}. Reason: {unityWebRequest.error}");
                return null;
            }

        }
    }
}
