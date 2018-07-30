using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CalculateEmails.CalculateEmails
{
    //todo: remove
    //public static class Serializer
    //{
    //    public static T Deserialize<T>(this string toDeserialize) where T : class
    //    {
    //        if (string.IsNullOrEmpty(toDeserialize)) return null;
    //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
    //        StringReader textReader = new StringReader(toDeserialize);
    //        return (T)xmlSerializer.Deserialize(textReader);
    //    }

    //    public static string Serialize<T>(this T toSerialize)
    //    {
    //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
    //        StringWriter textWriter = new StringWriter();
    //        xmlSerializer.Serialize(textWriter, toSerialize);
    //        return textWriter.ToString();
    //    }
    //}
}
