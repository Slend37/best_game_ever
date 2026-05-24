using System;
using System.Collections.Generic;
using System.Linq;
using BestGameEver.Core;
using BestGameEver.Core.Components;

namespace BestGameEver
{
    public class KeyRemapper
    {
        private Dictionary<ConsoleKey, Direction> _keyBindings;
        private ConsoleKey _remapActivationKey;
        private bool _isRemappingMode;
        private Direction? _pendingRemapDirection;
        private readonly IGameRenderer _renderer;
        private readonly IGameInputHandler _inputHandler;
        
        public event Action<Direction, ConsoleKey> OnKeyRemapped;
        
        public KeyRemapper(IGameRenderer renderer, IGameInputHandler inputHandler)
        {
            _renderer = renderer;
            _inputHandler = inputHandler;
            _remapActivationKey = ConsoleKey.R;
            _isRemappingMode = false;
            _pendingRemapDirection = null;
            InitializeDefaultBindings();
        }
        
        private void InitializeDefaultBindings()
        {
            _keyBindings = new Dictionary<ConsoleKey, Direction>
            {
                { ConsoleKey.UpArrow, Direction.Up },
                { ConsoleKey.RightArrow, Direction.Right },
                { ConsoleKey.DownArrow, Direction.Down },
                { ConsoleKey.LeftArrow, Direction.Left },
                { ConsoleKey.W, Direction.Up },
                { ConsoleKey.D, Direction.Right },
                { ConsoleKey.S, Direction.Down },
                { ConsoleKey.A, Direction.Left }
            };
        }
        
        public bool TryGetDirection(ConsoleKey key, out Direction direction)
        {
            return _keyBindings.TryGetValue(key, out direction);
        }
        
        public bool IsRemapKey(ConsoleKey key)
        {
            return key == _remapActivationKey;
        }
        
        public bool IsInRemappingMode()
        {
            return _isRemappingMode;
        }
        
        public void ProcessRemappingInput(ConsoleKey key)
        {
            if (!_isRemappingMode || !_pendingRemapDirection.HasValue)
                return;
                
            if (key == ConsoleKey.Escape)
            {
                CancelRemapping();
                return;
            }

            if (_keyBindings.ContainsKey(key))
            {
                var existingDirection = _keyBindings[key];
                _renderer.Clear();
                _renderer.DrawText($"Warning! Key {key} is already used for {existingDirection} movement.");
                _renderer.DrawText("Press Y to overwrite, any other key to cancel...");
                
                var confirmKey = _inputHandler.GetKey();
                if (confirmKey != ConsoleKey.Y)
                {
                    CancelRemapping();
                    return;
                }
            }
            
            RemapKey(_pendingRemapDirection.Value, key);
            
            _renderer.Clear();
            _renderer.DrawText($"Key for {_pendingRemapDirection.Value} changed to {key}!");
            System.Threading.Thread.Sleep(1000);
            
            CancelRemapping();
        }
        
        public void OpenRemappingMenu()
        {
            _isRemappingMode = true;
            _renderer.Clear();
            
            string[] menu = {
                "=== KEY REMAPPING MENU ===",
                "Select direction to remap:",
                $"1. Up - Current key: {GetKeyForDirection(Direction.Up)}",
                $"2. Right - Current key: {GetKeyForDirection(Direction.Right)}",
                $"3. Down - Current key: {GetKeyForDirection(Direction.Down)}",
                $"4. Left - Current key: {GetKeyForDirection(Direction.Left)}",
                "5. Reset to default",
                "ESC. Exit remapping mode"
            };
            
            foreach (var line in menu)
            {
                _renderer.DrawText(line);
            }
            
            var choice = _inputHandler.GetKey();
            
            switch (choice)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    StartRemapping(Direction.Up);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    StartRemapping(Direction.Right);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    StartRemapping(Direction.Down);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    StartRemapping(Direction.Left);
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    ResetToDefaultBindings();
                    _isRemappingMode = false;
                    break;
                default:
                    _isRemappingMode = false;
                    break;
            }
        }
        
        private void StartRemapping(Direction direction)
        {
            _pendingRemapDirection = direction;
            _renderer.Clear();
            _renderer.DrawText($"Press new key for {direction} movement...");
            _renderer.DrawText("Press ESC to cancel");
        }
        
        private void CancelRemapping()
        {
            _pendingRemapDirection = null;
            _isRemappingMode = false;
        }
        
        public void RemapKey(Direction direction, ConsoleKey newKey)
        {
            var oldBinding = _keyBindings.FirstOrDefault(x => x.Value == direction);
            if (oldBinding.Key != ConsoleKey.NoName)
            {
                _keyBindings.Remove(oldBinding.Key);
            }
            
            _keyBindings[newKey] = direction;
            
            SaveKeyBindings();
            
            OnKeyRemapped?.Invoke(direction, newKey);
        }
        
        private ConsoleKey GetKeyForDirection(Direction direction)
        {
            var binding = _keyBindings.FirstOrDefault(x => x.Value == direction);
            return binding.Key != ConsoleKey.NoName ? binding.Key : ConsoleKey.None;
        }
        
        public void ResetToDefaultBindings()
        {
            InitializeDefaultBindings();
            SaveKeyBindings();
            _renderer.Clear();
            _renderer.DrawText("Bindings reset to default!");
            System.Threading.Thread.Sleep(1000);
        }
        
        private void SaveKeyBindings()
        {
            try
            {
                var config = new
                {
                    KeyBindings = _keyBindings.ToDictionary(
                        kvp => kvp.Key.ToString(),
                        kvp => kvp.Value.ToString()
                    )
                };
                
                var json = System.Text.Json.JsonSerializer.Serialize(config);
                System.IO.File.WriteAllText("keybindings.json", json);
            }
            catch { }
        }
        
        public void LoadKeyBindings()
        {
            try
            {
                if (System.IO.File.Exists("keybindings.json"))
                {
                    var json = System.IO.File.ReadAllText("keybindings.json");
                }
            }
            catch { }
        }
        
        public Dictionary<Direction, ConsoleKey> GetCurrentBindings()
        {
            var bindings = new Dictionary<Direction, ConsoleKey>();
            
            foreach (var direction in new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left })
            {
                var key = GetKeyForDirection(direction);
                if (key != ConsoleKey.None)
                {
                    bindings[direction] = key;
                }
            }
            
            return bindings;
        }
    }
}