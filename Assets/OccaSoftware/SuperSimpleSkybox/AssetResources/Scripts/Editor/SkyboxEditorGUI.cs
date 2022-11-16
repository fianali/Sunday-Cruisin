using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Rendering;
using UnityEngine.Rendering;
using System.IO;

namespace OccaSoftware.SuperSimpleSkybox.Editor
{
    public class SkyboxEditorGUI : ShaderGUI
    {
        Material t;


		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			t = materialEditor.target as Material;
			MaterialEditor e = materialEditor;

			// Ground
			MaterialProperty groundColor = FindProperty("_GroundColor", properties);
			MaterialProperty groundEnabled = FindProperty("_GroundEnabled", properties);
			MaterialProperty groundFadeAmount = FindProperty("_GroundFadeAmount", properties);

			// Sky Colors
			MaterialProperty skyColorBlend = FindProperty("_SkyColorBlend", properties);
			MaterialProperty horizonColorDay = FindProperty("_HorizonColorDay", properties);
			MaterialProperty skyColorDay = FindProperty("_SkyColorDay", properties);
			MaterialProperty horizonColorNight = FindProperty("_HorizonColorNight", properties);
			MaterialProperty skyColorNight = FindProperty("_SkyColorNight", properties);
			MaterialProperty horizonSaturationFalloff = FindProperty("_HorizonSaturationFalloff", properties);
			MaterialProperty horizonSaturationAmount = FindProperty("_HorizonSaturationAmount", properties);

			// Sun
			MaterialProperty sunColorZenith = FindProperty("_SunColorZenith", properties);
			MaterialProperty sunColorHorizon = FindProperty("_SunColorHorizon", properties);
			MaterialProperty sunAngularDiameter = FindProperty("_SunAngularDiameter", properties);
			MaterialProperty sunFalloffIntensity = FindProperty("_SunFalloffIntensity", properties);
			MaterialProperty sunFalloff = FindProperty("_SunFalloff", properties);
			MaterialProperty sunSkyLightingEnabled = FindProperty("_SunSkyLightingEnabled", properties);

			// Sunset
			MaterialProperty sunsetHorizontalFalloff = FindProperty("_SunsetHorizontalFalloff", properties);
			MaterialProperty sunsetVerticalFalloff = FindProperty("_SunsetVerticalFalloff", properties);
			MaterialProperty sunsetRadialFalloff = FindProperty("_SunsetRadialFalloff", properties);
			MaterialProperty sunsetIntensity = FindProperty("_SunsetIntensity", properties);

			// Clouds
			MaterialProperty cloudTexture = FindProperty("_CloudTexture", properties);
			MaterialProperty cloudWindSpeed = FindProperty("_CloudWindSpeed", properties);
			MaterialProperty cloudiness = FindProperty("_Cloudiness", properties);
			MaterialProperty cloudSharpness = FindProperty("_CloudSharpness", properties);
			MaterialProperty cloudFalloff = FindProperty("_CloudFalloff", properties);
			MaterialProperty cloudScale = FindProperty("_CloudScale", properties);
			MaterialProperty cloudColorDay = FindProperty("_CloudColorDay", properties);
			MaterialProperty cloudColorNight = FindProperty("_CloudColorNight", properties);

			// Stars
			MaterialProperty starTexture = FindProperty("_StarTexture", properties);
			MaterialProperty starHorizonFalloff = FindProperty("_StarHorizonFalloff", properties);
			MaterialProperty starScale = FindProperty("_StarScale", properties);
			MaterialProperty starSpeed = FindProperty("_StarSpeed", properties);
			MaterialProperty starIntensity = FindProperty("_StarIntensity", properties);
			MaterialProperty starDaytimeBrightness = FindProperty("_StarDaytimeBrightness", properties);
			MaterialProperty starSaturation = FindProperty("_StarSaturation", properties);
			MaterialProperty proceduralStarsEnabled = FindProperty("_ProceduralStarsEnabled", properties);

			// Moon
			MaterialProperty moonAngularDiameter = FindProperty("_MoonAngularDiameter", properties);
			MaterialProperty moonFalloffAmount = FindProperty("_MoonFalloff", properties);
			MaterialProperty moonColor = FindProperty("_MoonColor", properties);


			DrawGroundProperties();
			DrawSkyProperties();
			DrawSunProperties();
			DrawMoonProperties();
			DrawStarProperties();
			DrawCloudProperties();
			Validate();

			void DrawGroundProperties()
			{
				EditorGUILayout.LabelField("Ground Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(groundEnabled, "Ground Enabled");
				if (groundEnabled.floatValue == 1)
				{
					e.ShaderProperty(groundColor, "Ground Color");
					e.ShaderProperty(groundFadeAmount, "Ground Fade Amount");
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			void DrawSkyProperties()
			{
				EditorGUILayout.LabelField("Sky Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(skyColorBlend, "Horizon-Sky Blend");

				EditorGUILayout.LabelField("Day Colors", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(horizonColorDay, "Horizon");
				e.ShaderProperty(skyColorDay, "Sky");
				EditorGUI.indentLevel--;

				EditorGUILayout.LabelField("Night Colors", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(horizonColorNight, "Horizon");
				e.ShaderProperty(skyColorNight, "Sky");
				EditorGUI.indentLevel--;

				EditorGUILayout.LabelField("Horizon Saturation", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(horizonSaturationAmount, "Amount");
				e.ShaderProperty(horizonSaturationFalloff, "Falloff");
				EditorGUI.indentLevel--;

				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			void DrawSunProperties()
			{
				// Sun
				EditorGUILayout.LabelField("Sun Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(sunAngularDiameter, "Angular Diameter");
				e.ShaderProperty(sunColorHorizon, "Horizon Color");
				e.ShaderProperty(sunColorZenith, "Zenith Color");
				e.ShaderProperty(sunSkyLightingEnabled, "Sky Lighting Enabled");
				if (sunSkyLightingEnabled.floatValue == 1)
				{
					e.ShaderProperty(sunFalloff, "Falloff Amount");
					e.ShaderProperty(sunFalloffIntensity, "Falloff Intensity");

					EditorGUILayout.LabelField("Sunset Settings", EditorStyles.boldLabel);
					EditorGUI.indentLevel++;
					e.ShaderProperty(sunsetIntensity, "Intensity");
					e.ShaderProperty(sunsetRadialFalloff, "Radial Falloff");
					e.ShaderProperty(sunsetHorizontalFalloff, "Horizontal Falloff");
					e.ShaderProperty(sunsetVerticalFalloff, "Vertical Falloff");
					EditorGUI.indentLevel--;
				}

				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			void DrawMoonProperties()
			{
				EditorGUILayout.LabelField("Moon Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				e.ShaderProperty(moonAngularDiameter, "Angular Diameter");
				e.ShaderProperty(moonColor, "Color");
				e.ShaderProperty(moonFalloffAmount, "Falloff Amount");
				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			void DrawStarProperties()
			{
				// Stars
				EditorGUILayout.LabelField("Star Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				StarControlState starControlState = (StarControlState)proceduralStarsEnabled.floatValue;
				StarControlState cachedState = starControlState;
				starControlState = (StarControlState)EditorGUILayout.EnumPopup("Rendering Method", starControlState);
				proceduralStarsEnabled.floatValue = (float)starControlState;

				if (cachedState != starControlState)
				{
					e.PropertiesChanged();
					if (starControlState == StarControlState.Procedural)
						starHorizonFalloff.floatValue = 0;
					else
						starHorizonFalloff.floatValue = 1;
				}


				if ((StarControlState)proceduralStarsEnabled.floatValue == StarControlState.Procedural)
				{
					e.ShaderProperty(starIntensity, "Brightness");
					e.ShaderProperty(starDaytimeBrightness, "Daytime Brightness");
					e.ShaderProperty(starHorizonFalloff, "Horizon Falloff");
					e.ShaderProperty(starSaturation, "Saturation");
				}
				else
				{
					EditorGUILayout.LabelField("Texture Settings", EditorStyles.boldLabel);
					EditorGUI.indentLevel++;
					starTexture.textureValue = (Texture)EditorGUILayout.ObjectField("Texture", starTexture.textureValue, typeof(Texture), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
					if (starTexture.textureValue != null)
					{
						e.ShaderProperty(starScale, "Scale");
						e.ShaderProperty(starSpeed, "Rotation Speed");
						EditorGUI.indentLevel--;
						EditorGUILayout.LabelField("Look Settings", EditorStyles.boldLabel);
						EditorGUI.indentLevel++;
						e.ShaderProperty(starIntensity, "Brightness");
						e.ShaderProperty(starDaytimeBrightness, "Daytime Brightness");
						e.ShaderProperty(starHorizonFalloff, "Horizon Falloff");
						e.ShaderProperty(starSaturation, "Saturation");
						EditorGUI.indentLevel--;
					}
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}
			void DrawCloudProperties()
			{
				EditorGUILayout.LabelField("Cloud Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				EditorGUILayout.LabelField("Texture Settings", EditorStyles.boldLabel);
				EditorGUI.indentLevel++;
				Texture cachedTexture = cloudTexture.textureValue;
				cloudTexture.textureValue = (Texture)EditorGUILayout.ObjectField("Texture", cloudTexture.textureValue, typeof(Texture), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
				if (cachedTexture == null && cloudTexture.textureValue != null)
					cloudiness.floatValue = 0.5f;

				if (cloudTexture.textureValue != null)
				{
					Vector2Int s = EditorGUILayout.Vector2IntField("Scale", new Vector2Int((int)cloudScale.vectorValue.x, (int)cloudScale.vectorValue.y));
					cloudScale.vectorValue = new Vector4(s.x, s.y, 0, 0);

					cloudWindSpeed.vectorValue = EditorGUILayout.Vector2Field("Speed", cloudWindSpeed.vectorValue);

					EditorGUI.indentLevel--;
					EditorGUILayout.LabelField("Look Settings", EditorStyles.boldLabel);
					EditorGUI.indentLevel++;
					e.ShaderProperty(cloudiness, "Cloudiness");
					e.ShaderProperty(cloudSharpness, "Threshold");
					e.ShaderProperty(cloudFalloff, "Zenith Falloff");
					EditorGUI.indentLevel--;

					EditorGUILayout.LabelField("Color Settings", EditorStyles.boldLabel);
					EditorGUI.indentLevel++;
					e.ShaderProperty(cloudColorDay, "Day");
					e.ShaderProperty(cloudColorNight, "Night");
					EditorGUI.indentLevel--;
				}
				else
				{
					cloudiness.floatValue = 0;
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}

			void Validate()
			{
				sunAngularDiameter.floatValue = Mathf.Max(sunAngularDiameter.floatValue, 0);
				sunFalloff.floatValue = Mathf.Max(sunFalloff.floatValue, 0);
				sunFalloffIntensity.floatValue = Mathf.Max(sunFalloffIntensity.floatValue, 0);

				moonAngularDiameter.floatValue = Mathf.Max(moonAngularDiameter.floatValue, 0);
				moonFalloffAmount.floatValue = Mathf.Max(moonFalloffAmount.floatValue, 0);

				starSaturation.floatValue = Mathf.Max(starSaturation.floatValue, 0);
			}
		}


		private enum StarControlState
		{
			Texture,
			Procedural
		}
	}
}
