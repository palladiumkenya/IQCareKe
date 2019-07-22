using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.Common.Core.Models
{
    public class AppStateStoreObjects
    {
        public int Id { get; set; }
        public int AppStateStoreId { get; set; }
        public string AppStateObject { get; set; }

        [ForeignKey("AppStateStoreId")]
        public virtual AppStateStore AppStateStore { get; set; }
    }
}