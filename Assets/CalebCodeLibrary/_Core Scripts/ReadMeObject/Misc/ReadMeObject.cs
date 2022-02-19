using System;
using UnityEngine;

namespace CalebCodeLibrary.ReadMeUtility
{
	[CreateAssetMenu(fileName = "Read Me", menuName = "Read Me File")]
	public class ReadMeObject : ScriptableObject
	{
		public string titleText;
		public int titleSize = 24;
		public Color titleColor = Color.black;
		public FontStyle titleFontStyle = FontStyle.Bold;

		public ReadMeSection[] sections;
		public bool loadedLayout;

		[Serializable]
		public class ReadMeSection
		{
			public string headingText = "A Read Me Header";
			public int headingSize = 18;
			public Color headingColor = Color.white;
			public FontStyle headingFontStyle = FontStyle.Bold;
			
			public string bodyText = "Some Read Me Text";
			public int bodySize = 14;
			public Color bodyColor = Color.white;
			public FontStyle bodyFontStyle = FontStyle.Normal;

			public string linkText = "A Read Me Link Text";
			public int linkSize = 14;
			public Color linkTextColor = Color.white;
			public FontStyle linkFontStyle = FontStyle.Italic;

			public string url = "An URL";
		}
	}
}
