using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("Interop_PlacerValues")]
    public class InteropPlacerValues
    {
        public int Id { get; set; }
        public int InteropPlacerTypeId { get; set; }
        public int IdentifierType { get; set; }
        public int EntityId { get; set; }
        public int PlacerValue { get; set; }
    }
}
