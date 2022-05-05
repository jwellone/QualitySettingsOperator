using System;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace jwellone
{
	public sealed class QualitySettingsOperator : MonoBehaviour
	{
		static QualitySettingsOperator? _instance = null;

		[SerializeField] Canvas _canvas = null!;
		[SerializeField] RectTransform _contentsTransform = null!;
		[SerializeField] Dropdown _dropDownLevel = null!;
		[SerializeField] Dropdown _dropDownTextureQuality = null!;
		[SerializeField] Dropdown _dropDownAnisotropicTextures = null!;
		[SerializeField] Dropdown _dropDownAntiAliasing = null!;
		[SerializeField] Toggle _toggleSoftParticles = null!;
		[SerializeField] Toggle _toggleRealtimeReflectionProbes = null!;
		[SerializeField] Toggle _toggleBillboardsFaceCameraPosition = null!;
		[SerializeField] InputField _inputResolutionScalingFixedDPIFactor = null!;
		[SerializeField] Toggle _toggleTextureStreaming = null!;
		[SerializeField] Dropdown _dropdownShadowmaskMode = null!;
		[SerializeField] Dropdown _dropdownShadows = null!;
		[SerializeField] Dropdown _dropdownShadowResolution = null!;
		[SerializeField] Dropdown _dropdownShadowProjection = null!;
		[SerializeField] InputField _inputShadowDistance = null!;
		[SerializeField] InputField _inputShadowNearPlaneOffset = null!;
		[SerializeField] Dropdown _dropdownShadowCascades = null!;
		[SerializeField] Dropdown _dropdownSkinWeights = null!;
		[SerializeField] Dropdown _dropdownVSyncCount = null!;
		[SerializeField] InputField _inputLODBias = null!;
		[SerializeField] InputField _inputMaximumLODLevel = null!;
		[SerializeField] InputField _inputParticleRaycastBudge = null!;
		[SerializeField] InputField _inputAsyncUploadTimeSlice = null!;
		[SerializeField] InputField _inputAsyncUploadBufferSize = null!;
		[SerializeField] Toggle _toggleAsyncUploadPersistentBuffer = null!;


		public static Canvas? canvas => _instance?._canvas;

		void Awake()
		{
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);
		}

		void OnDestroy()
		{
			if (_instance == this)
			{
				_instance = null;
			}
		}

		void Start()
		{
			_dropDownLevel.options.Clear();
			foreach (var name in QualitySettings.names)
			{
				_dropDownLevel.options.Add(new Dropdown.OptionData(name));
			}
			_dropDownLevel.value = QualitySettings.GetQualityLevel();
			_dropDownLevel.RefreshShownValue();
			_dropDownLevel.onValueChanged.AddListener(_ =>
			{
				QualitySettings.SetQualityLevel(_dropDownLevel.value);
			});


			_dropDownTextureQuality.options.Clear();
			_dropDownTextureQuality.options.Add(new Dropdown.OptionData("Full Res"));
			_dropDownTextureQuality.options.Add(new Dropdown.OptionData("Half Res"));
			_dropDownTextureQuality.options.Add(new Dropdown.OptionData("Quarter Res"));
			_dropDownTextureQuality.options.Add(new Dropdown.OptionData("Eighth Res"));
			_dropDownTextureQuality.value = QualitySettings.masterTextureLimit;
			_dropDownTextureQuality.RefreshShownValue();
			_dropDownTextureQuality.onValueChanged.AddListener(_ =>
			{
				QualitySettings.masterTextureLimit = _dropDownTextureQuality.value;
			});


			_dropDownAnisotropicTextures.options.Clear();
			foreach (var name in Enum.GetNames(typeof(AnisotropicFiltering)))
			{
				_dropDownAnisotropicTextures.options.Add(new Dropdown.OptionData(name));
			}
			_dropDownAnisotropicTextures.value = (int)QualitySettings.anisotropicFiltering;
			_dropDownAnisotropicTextures.RefreshShownValue();
			_dropDownAnisotropicTextures.onValueChanged.AddListener(_ =>
			{
				QualitySettings.anisotropicFiltering = (AnisotropicFiltering)_dropDownAnisotropicTextures.value;
			});


			_dropDownAntiAliasing.options.Clear();
			_dropDownAntiAliasing.options.Add(new Dropdown.OptionData("Disable"));
			_dropDownAntiAliasing.options.Add(new Dropdown.OptionData("2x Multi Sampling"));
			_dropDownAntiAliasing.options.Add(new Dropdown.OptionData("4x Multi Sampling"));
			_dropDownAntiAliasing.options.Add(new Dropdown.OptionData("8x Multi Sampling"));

			switch (QualitySettings.antiAliasing)
			{
				case 0: _dropDownAntiAliasing.value = 0; break;
				case 2: _dropDownAntiAliasing.value = 1; break;
				case 4: _dropDownAntiAliasing.value = 2; break;
				case 8: _dropDownAntiAliasing.value = 3; break;
			}
			_dropDownAntiAliasing.RefreshShownValue();
			_dropDownAntiAliasing.onValueChanged.AddListener(_ =>
			{
				switch (_dropDownAntiAliasing.value)
				{
					case 0: QualitySettings.antiAliasing = 0; break;
					case 1: QualitySettings.antiAliasing = 2; break;
					case 2: QualitySettings.antiAliasing = 4; break;
					case 3: QualitySettings.antiAliasing = 8; break;
				}
			});


			_toggleSoftParticles.isOn = QualitySettings.softParticles;
			_toggleSoftParticles.onValueChanged.AddListener(_ =>
			{
				QualitySettings.softParticles = _toggleSoftParticles.isOn;
			});


			_toggleRealtimeReflectionProbes.isOn = QualitySettings.realtimeReflectionProbes;
			_toggleRealtimeReflectionProbes.onValueChanged.AddListener(_ =>
			{
				QualitySettings.realtimeReflectionProbes = _toggleRealtimeReflectionProbes.isOn;
			});


			_toggleBillboardsFaceCameraPosition.isOn = QualitySettings.billboardsFaceCameraPosition;
			_toggleBillboardsFaceCameraPosition.onValueChanged.AddListener(_ =>
			{
				QualitySettings.billboardsFaceCameraPosition = _toggleBillboardsFaceCameraPosition.isOn;
			});


			_inputResolutionScalingFixedDPIFactor.text = QualitySettings.resolutionScalingFixedDPIFactor.ToString();
			_inputResolutionScalingFixedDPIFactor.onEndEdit.AddListener(_ =>
			{
				if (float.TryParse(_inputResolutionScalingFixedDPIFactor.text, out var result))
				{
					QualitySettings.resolutionScalingFixedDPIFactor = result;
				}
			});


			_toggleTextureStreaming.isOn = QualitySettings.streamingMipmapsActive;
			_toggleTextureStreaming.onValueChanged.AddListener(_ =>
			{
				QualitySettings.streamingMipmapsActive = _toggleTextureStreaming.isOn;
			});


			_dropdownShadowmaskMode.options.Clear();
			foreach (var name in Enum.GetNames(typeof(ShadowmaskMode)))
			{
				_dropdownShadowmaskMode.options.Add(new Dropdown.OptionData(name));
			}
			_dropdownShadowmaskMode.value = (int)QualitySettings.shadowmaskMode;
			_dropdownShadowmaskMode.RefreshShownValue();
			_dropdownShadowmaskMode.onValueChanged.AddListener(_ =>
			{
				QualitySettings.shadowmaskMode = (ShadowmaskMode)_dropdownShadowmaskMode.value;
			});


			_dropdownShadows.options.Clear();
			foreach (var name in Enum.GetNames(typeof(ShadowQuality)))
			{
				_dropdownShadows.options.Add(new Dropdown.OptionData(name));
			}
			_dropdownShadows.value = (int)QualitySettings.shadows;
			_dropdownShadows.RefreshShownValue();
			_dropdownShadows.onValueChanged.AddListener(_ =>
			{
				QualitySettings.shadows = (ShadowQuality)_dropdownShadows.value;
			});


			_dropdownShadowResolution.options.Clear();
			foreach (var name in Enum.GetNames(typeof(ShadowResolution)))
			{
				_dropdownShadowResolution.options.Add(new Dropdown.OptionData(name));
			}
			_dropdownShadowResolution.value = (int)QualitySettings.shadowResolution;
			_dropdownShadowResolution.RefreshShownValue();
			_dropdownShadowResolution.onValueChanged.AddListener(_ =>
			{
				QualitySettings.shadowResolution = (ShadowResolution)_dropdownShadowResolution.value;
			});


			_dropdownShadowProjection.options.Clear();
			foreach (var name in Enum.GetNames(typeof(ShadowProjection)))
			{
				_dropdownShadowProjection.options.Add(new Dropdown.OptionData(name));
			}
			_dropdownShadowProjection.value = (int)QualitySettings.shadowProjection;
			_dropdownShadowProjection.RefreshShownValue();
			_dropdownShadowProjection.onValueChanged.AddListener(_ =>
			{
				QualitySettings.shadowProjection = (ShadowProjection)_dropdownShadowProjection.value;
			});


			_inputShadowDistance.text = QualitySettings.shadowDistance.ToString();
			_inputShadowDistance.onEndEdit.AddListener(_ =>
			{
				if (float.TryParse(_inputShadowDistance.text, out var result))
				{
					QualitySettings.shadowDistance = result;
				}
			});


			_inputShadowNearPlaneOffset.text = QualitySettings.shadowNearPlaneOffset.ToString();
			_inputShadowNearPlaneOffset.onEndEdit.AddListener(_ =>
			{
				if (float.TryParse(_inputShadowNearPlaneOffset.text, out var result))
				{
					QualitySettings.shadowNearPlaneOffset = result;
				}
			});


			_dropdownShadowCascades.options.Clear();
			_dropdownShadowCascades.options.Add(new Dropdown.OptionData("No Cascades"));
			_dropdownShadowCascades.options.Add(new Dropdown.OptionData("Two Cascades"));
			_dropdownShadowCascades.options.Add(new Dropdown.OptionData("Four Cascades"));

			switch (QualitySettings.shadowCascades)
			{
				case 1: _dropdownShadowCascades.value = 0; break;
				case 2: _dropdownShadowCascades.value = 1; break;
				case 4: _dropdownShadowCascades.value = 2; break;
			}
			_dropdownShadowCascades.RefreshShownValue();
			_dropdownShadowCascades.onValueChanged.AddListener(_ =>
			{
				switch (_dropdownShadowCascades.value)
				{
					case 0: QualitySettings.shadowCascades = 1; break;
					case 1: QualitySettings.shadowCascades = 2; break;
					case 2: QualitySettings.shadowCascades = 4; break;
				}
			});


			_dropdownSkinWeights.options.Clear();
			foreach (var name in Enum.GetNames(typeof(SkinWeights)))
			{
				_dropdownSkinWeights.options.Add(new Dropdown.OptionData(name));
			}

			switch (QualitySettings.skinWeights)
			{
				case SkinWeights.OneBone: _dropdownSkinWeights.value = 0; break;
				case SkinWeights.TwoBones: _dropdownSkinWeights.value = 1; break;
				case SkinWeights.FourBones: _dropdownSkinWeights.value = 2; break;
				case SkinWeights.Unlimited: _dropdownSkinWeights.value = 3; break;
			}

			_dropdownSkinWeights.RefreshShownValue();
			_dropdownSkinWeights.onValueChanged.AddListener(_ =>
			{
				switch (_dropdownSkinWeights.value)
				{
					case 0: QualitySettings.skinWeights = SkinWeights.OneBone; break;
					case 1: QualitySettings.skinWeights = SkinWeights.TwoBones; break;
					case 2: QualitySettings.skinWeights = SkinWeights.FourBones; break;
					case 3: QualitySettings.skinWeights = SkinWeights.Unlimited; break;
				}
			});


			_dropdownVSyncCount.options.Clear();
			_dropdownVSyncCount.options.Add(new Dropdown.OptionData("Don't Sync"));
			_dropdownVSyncCount.options.Add(new Dropdown.OptionData("Every V Blank"));
			_dropdownVSyncCount.options.Add(new Dropdown.OptionData("Every Second V Blank"));
			_dropdownVSyncCount.value = QualitySettings.vSyncCount;
			_dropdownVSyncCount.RefreshShownValue();
			_dropdownVSyncCount.onValueChanged.AddListener(_ =>
			{
				QualitySettings.vSyncCount = _dropdownVSyncCount.value;
			});


			_inputLODBias.text = QualitySettings.lodBias.ToString();
			_inputLODBias.onEndEdit.AddListener(_ =>
			{
				if (float.TryParse(_inputLODBias.text, out var result))
				{
					QualitySettings.lodBias = result;
				}
			});


			_inputMaximumLODLevel.text = QualitySettings.maximumLODLevel.ToString();
			_inputMaximumLODLevel.onEndEdit.AddListener(_ =>
			{
				if (int.TryParse(_inputMaximumLODLevel.text, out var result))
				{
					QualitySettings.maximumLODLevel = result;
				}
			});

			_inputParticleRaycastBudge.text = QualitySettings.particleRaycastBudget.ToString();
			_inputParticleRaycastBudge.onEndEdit.AddListener(_ =>
			{
				if (int.TryParse(_inputParticleRaycastBudge.text, out var result))
				{
					QualitySettings.particleRaycastBudget = result;
				}
			});


			_inputAsyncUploadTimeSlice.text = QualitySettings.asyncUploadTimeSlice.ToString();
			_inputAsyncUploadTimeSlice.onEndEdit.AddListener(_ =>
			{
				if (int.TryParse(_inputAsyncUploadTimeSlice.text, out var result))
				{
					QualitySettings.asyncUploadTimeSlice = result;
				}
			});


			_inputAsyncUploadBufferSize.text = QualitySettings.asyncUploadBufferSize.ToString();
			_inputAsyncUploadBufferSize.onEndEdit.AddListener(_ =>
			{
				if (int.TryParse(_inputAsyncUploadBufferSize.text, out var result))
				{
					QualitySettings.asyncUploadBufferSize = result;
				}
			});


			_toggleAsyncUploadPersistentBuffer.isOn = QualitySettings.asyncUploadPersistentBuffer;
			_toggleAsyncUploadPersistentBuffer.onValueChanged.AddListener(_ =>
			{
				QualitySettings.asyncUploadPersistentBuffer = _toggleAsyncUploadPersistentBuffer.isOn;
			});



			_contentsTransform.anchoredPosition = Vector2.zero;
			_contentsTransform.sizeDelta = Vector2.zero;
			_contentsTransform.anchorMin = new Vector2(Screen.safeArea.xMin / Screen.width, Screen.safeArea.yMin / Screen.height);
			_contentsTransform.anchorMax = new Vector2(Screen.safeArea.xMax / Screen.width, Screen.safeArea.yMax / Screen.height);
		}

		public static void Close()
		{
			_instance?._canvas.gameObject.SetActive(false);
		}

		public static void Show()
		{
			_instance?._canvas.gameObject.SetActive(true);
		}
	}
}