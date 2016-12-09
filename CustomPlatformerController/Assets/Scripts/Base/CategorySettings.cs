using System;

namespace Assets.Scripts.Base
{
    [Serializable]
    public struct CategorySettings
    {
        public string categoryName;
        public int handle;
        public int fight;
        public int hide;
        public int Total
        {
            get
            {
                return (fight + handle + hide);
            }
        }
    }
}
