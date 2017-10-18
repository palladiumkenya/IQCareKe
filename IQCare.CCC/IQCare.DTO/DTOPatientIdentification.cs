using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.DTO
{
    public class DtoPatientIdentification
    {
        
        public DTOIdentifier ExternalPatientId { get; set; }
        public List<DTOIdentifier> InternalPatientId { get; set; }
        public PatientNameDto PatientName { get; set; }
    }
}
