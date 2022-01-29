using UnityEngine;

namespace Shared.Sources.Stats
{
    public class Flag
    {
        private int _flagCounter;

        public bool IsSet
        {
            get => _flagCounter > 0;

            set => _flagCounter += value ? 1 : -1;
        }

        public int Count => _flagCounter;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public Flag()
        {
            _flagCounter = 0;
        }

        public Flag Set()
        {
            _flagCounter++;
            
            return this;
        }

        public Flag Unset()
        {
            _flagCounter--;

            return this;
        }
        
        public Flag Clear(int counterValue = 0)
        {
            _flagCounter = counterValue;

            return this;
        }

        public Flag Clamp(int minValue = 0)
        {
            _flagCounter = _flagCounter > 0 ? 0 : _flagCounter;
            
            return this;
        }

        public Flag Clamp(int minValue, int maxValue)
        {
            _flagCounter = Mathf.Clamp(_flagCounter, minValue, maxValue);
                
            return this;
        }
        
        public Flag OverrideFrom(Flag flag)
        {
            _flagCounter = flag._flagCounter;

            return this;
        }

        public Flag Override(Flag flag)
        {
            flag._flagCounter = _flagCounter;

            return flag;
        }

        public Flag Clone()
        {
            return Override(new Flag());
        }
    }
}
