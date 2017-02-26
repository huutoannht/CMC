using LamNghiep.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.Common
{
    public static class ListExtensions
    {
       
        public static List<PhongBan> DeepCopy(List<PhongBan> objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (List<PhongBan>)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
