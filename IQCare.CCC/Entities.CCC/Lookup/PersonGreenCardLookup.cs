using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("GreenCardBlueCard_Transactional")]
    public class PersonGreenCardLookup
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int Ptn_Pk { get; set; }
    }
}
