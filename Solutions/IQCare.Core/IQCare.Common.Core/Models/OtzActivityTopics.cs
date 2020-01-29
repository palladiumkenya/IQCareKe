using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class OtzActivityTopics
    {
        public int Id { get; set; }
        public int ActivityFormId { get; set; }
        public int TopicId { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
