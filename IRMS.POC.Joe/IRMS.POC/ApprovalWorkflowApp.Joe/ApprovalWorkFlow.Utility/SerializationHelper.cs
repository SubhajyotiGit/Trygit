using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApprovalWorkFlow.Utility
{
    public static class SerializationHelper
    {
        public static string SerializeToXml(object target)
        {
            XmlSerializer serializer = new XmlSerializer(target.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, target);
            return writer.ToString();
        }

        public static string SerializeToXml(object target, Encoding encoding)
        {
            XmlSerializer serializer = new XmlSerializer(target.GetType());
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms, encoding);
            serializer.Serialize(writer, target);
            ms.Position = 0;
            StreamReader reader = new StreamReader(ms, encoding);
            return reader.ReadToEnd();
        }

        public static void SerializeToXml(object target, Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(target.GetType());
            serializer.Serialize(stream, target);
        }

        public static object DeserializeXml(TextReader reader, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(reader);
        }

        public static object DeserializeXml(Stream stream, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(stream);
        }

        #region Conversion using XML Serialization

        public static Object ConvertEntity(Object inputObject, System.Type outputType)
        {

            if (inputObject == null)
                return null;
            XmlSerializer inputSerializer = new XmlSerializer(inputObject.GetType());
            XmlSerializer outputSerializer = new XmlSerializer(outputType);

            StringWriter xmlOut = new StringWriter();
            inputSerializer.Serialize(xmlOut, inputObject);

            StringReader xmlIn = new StringReader(xmlOut.GetStringBuilder().ToString());
            Object result = outputSerializer.Deserialize(xmlIn);
            return result;
        }

        #endregion
    }
}
