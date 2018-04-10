using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Queue
{
    [Serializable]
    [Table("vw_WaitingQueue")]
    public  class WaitingQueue
    { 
       [Key]
        public int QueueId { get; set; }
        public string QueueName { get; set; }

        public int CodeId { get; set; }

       [NotMapped]
        public int Length { get; set; }
    }
}
