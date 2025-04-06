using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesAssembly
{
    public class AbilitySelector : MonoBehaviour
    {
        [SerializeField] private AbilityType _abilityType;
        [SerializeField] private Color _inActiveColor;
        
        private Color _defaultColor;
        private Image _image;
        private Button _button;
        private AbilityExecutor _abilityExecutor;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
            _button.onClick.AddListener(OnClick);
            ServiceLocatorController.TryWaitDependenciesInjected(Setup);
            
        }

        private void Setup()
        {
            _abilityExecutor = ServiceLocatorController.Resolve<AbilitiesSystemContainer>().ResolveDependency<AbilityExecutor>();
            _abilityExecutor.OnAbilitySelected += OnAbilitySelected;
            
            UpdateState(_abilityExecutor.AbilityType);
        }

        private void OnDestroy()
        {
            _abilityExecutor.OnAbilitySelected -= OnAbilitySelected;
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _abilityExecutor.EquipAbility(_abilityType);
        }
        
        private void OnAbilitySelected(AbilityType type)
        {
            UpdateState(type);
        }
        
        private void UpdateState(AbilityType currentAbilityType)
        {
            _image.color = currentAbilityType == _abilityType ? _defaultColor : _inActiveColor;
        }
    }
}