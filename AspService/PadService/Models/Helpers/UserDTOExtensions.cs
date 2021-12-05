using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PadService.Models.Helpers
{
    public static class UserDTOExtensions
    {
        public static string ToXml(this List<UserDTO> users)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(users.GetType());
            var xmlString = "";
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, users);
                xmlString = textWriter.ToString();

            }

            return xmlString;
        }
    }
}
