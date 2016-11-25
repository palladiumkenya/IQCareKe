using System;

namespace Entities.Billing
{
    [Serializable]
    public class ItemType
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string ContainerName
        {
            get;
            set;
        }
        public string ColumnItemName
        {
            get;
            set;
        }
        public string ColumnItemIdentifier
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public bool DeleteFlag
        {
            get;
            set;
        }
    }
}
