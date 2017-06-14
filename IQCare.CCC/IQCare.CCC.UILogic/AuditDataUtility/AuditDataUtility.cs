using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Entities.Common;

namespace IQCare.CCC.UILogic.AuditDataUtility
{
    public class AuditDataUtility
    {
        public static string Serializer<T>(List<T> objectVariable) where T:BaseEntity
        {
            StringBuilder sbData = new StringBuilder();
            XmlSerializer objectXmlSerializer = new XmlSerializer(objectVariable.GetType());
            List<T> listBaseEntities = new List<T>();
            StringWriter swWriter = new StringWriter();

            if (objectVariable.Count > 0)
            {
                if (objectVariable[0].AuditData != null)
                {
                    listBaseEntities =
                        (List<T>)objectXmlSerializer.Deserialize(new StringReader(objectVariable[0].AuditData));
                }           

                listBaseEntities.Add(objectVariable[0]);
            }
            else
            {
                listBaseEntities.Add(objectVariable[0]);
            }

            swWriter = new StringWriter(sbData);
            objectXmlSerializer.Serialize(swWriter, listBaseEntities);

            return sbData.ToString();
        }
    }
}
