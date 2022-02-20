using UnityEngine;
using UnityEditor;
using static System.Collections.Specialized.BitVector32;

namespace CalebCodeLibrary.ReadMeUtility
{
	[CustomEditor(typeof(ReadMeObject))]
	public class ReadMeEditor : Editor
	{
		static readonly float _spacing = 16f;

		public override void OnInspectorGUI()
		{
			var readme = (ReadMeObject)target; // Gets the ScriptableObject "ReadMeObject
			Init(readme);

			if (!string.IsNullOrEmpty(readme.titleText))
            {
				GUILayout.Label(readme.titleText, TitleStyle);
			}
			GUILayout.Space(_spacing);

			// End this function when there are no sections
			if (readme.sections == null || readme.sections.Length == 0)
				return;

			foreach (var section in readme.sections)
			{
				UpdateStyle(section);
				if (!string.IsNullOrEmpty(section.headingText))
				{
					GUILayout.Label(section.headingText, HeadingStyle);
				}

				if (!string.IsNullOrEmpty(section.bodyText))
				{
					GUILayout.Label(System.Text.RegularExpressions.Regex.Unescape(section.bodyText), BodyStyle);
					
				}
				if (!string.IsNullOrEmpty(section.linkText))
				{
					GUILayout.Space(_spacing / 2);
					if (LabelTheLink(new GUIContent(section.linkText)))
					{
						Application.OpenURL(section.url);
					}
				}
				GUILayout.Space(_spacing);
			}
		}


		GUIStyle LinkStyle { get { return _linkStyle; } }
		[SerializeField] GUIStyle _linkStyle;

		GUIStyle TitleStyle { get { return _titleStyle; } }
		[SerializeField] GUIStyle _titleStyle;

		GUIStyle HeadingStyle { get { return _headingStyle; } }
		[SerializeField] GUIStyle _headingStyle;

		GUIStyle BodyStyle { get { return _bodyStyle; } }
		[SerializeField] GUIStyle _bodyStyle;

		void Init(ReadMeObject readMeObject)
		{
			_titleStyle = new GUIStyle()
			{
				fontSize = readMeObject.titleSize,
				fontStyle = readMeObject.titleFontStyle,
				wordWrap = true,
			};
			_titleStyle.normal.textColor = readMeObject.titleColor;
		}

		/// <summary>
		/// Updates the style of the given section.
		/// </summary>
		/// <param name="section">The section which this function should update style according to.</param>
		void UpdateStyle(ReadMeObject.ReadMeSection section)
		{
			_headingStyle = new GUIStyle()
			{
				fontSize = section.headingSize,
				fontStyle = section.headingFontStyle,
				wordWrap = true,
			};
			_headingStyle.normal.textColor = section.headingColor;

			_bodyStyle = new GUIStyle()
			{
				wordWrap = true,
				fontSize = section.bodySize,
				fontStyle = section.bodyFontStyle,
				richText = true,
			};
			_bodyStyle.normal.textColor = section.bodyColor;

			_linkStyle = new GUIStyle()
			{
				wordWrap = true,
				fontSize = section.linkSize,
				fontStyle = section.linkFontStyle,
			};
			// Match selection color which works nicely for both light and dark skins
			_linkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
			_linkStyle.stretchWidth = false;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="label"></param>
		/// <param name="options"></param>
		/// <returns> Returns bool of whether the user clicked on the link.</returns>
		bool LabelTheLink(GUIContent label, params GUILayoutOption[] options)
		{
			var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

			Handles.BeginGUI();
			Handles.color = LinkStyle.normal.textColor;
			Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
			Handles.color = Color.white;
			Handles.EndGUI();

			EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

			return GUI.Button(position, label, LinkStyle);
		}
	}
}

