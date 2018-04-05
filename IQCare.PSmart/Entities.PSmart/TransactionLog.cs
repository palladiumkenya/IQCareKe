using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.PSmart
{
    [Serializable]
    [Table("PSmartTransactionLog")]
    public class TransactionLog
    {
        private int _userId = 0;

        public int? Id { get; set; }
        public int TransactionType { get; set; }
        public Guid? TranLogId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Request { get; set; }
        public string LogMessage { get; set; }
        public int UserId { get => _userId; set => _userId = value; }
    }
}
