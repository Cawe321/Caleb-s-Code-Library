using UnityEngine;
using UnityEngine.UI;

namespace CalebCodeLibrary.ImageDownloader.Example
{
    public class ImageDownloaderExampleScript : MonoBehaviour
    {
        public RawImage displayImage;

        /// <summary>
        /// InputField to enter link of URL
        /// </summary>
        public InputField inputField;

        public GameObject waitingMenu;


        void Awake()
        {
            waitingMenu.SetActive(false);
        }

        /// <summary>
        /// Since <see cref="ImageDownloader.DownloadImageAsync(string)"/> is async, the function that calls it should also be async.
        /// </summary>
        public async void StartDownload()
        {
            waitingMenu.SetActive(true); // Displays the waiting menu
            Texture2D downloadedTexture = await ImageDownloader.DownloadImageAsync(inputField.text); // Proceeds to attempt downloading the image using the url given from the inputfield.
            displayImage.texture = downloadedTexture; // Assign the texture to RawImage
            waitingMenu.SetActive(false); // Turns off the waiting menu after the downloading has complete/failed.
        }
    }
}