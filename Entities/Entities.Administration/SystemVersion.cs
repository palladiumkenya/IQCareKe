using System;

namespace Entities.Administration
{
    [Serializable]
    public class  SystemVersion
    {
        public int Id {get;set;}
        public string AppVersion {get;set;}
        public string DBVersion {get;set;}
        public DateTime ReleaseDate {get;set;}
        public string VersionName { get; set; }
    }
}
