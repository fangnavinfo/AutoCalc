using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace exif
{
    class Exif
    {
        /// <summary>
        /// 设置图片的经纬高
        /// </summary>
        /// <param name="IN_File">文件路径</param>
        /// <param name="IN_Lat">纬度</param>
        /// <param name="IN_Lng">经度</param>
        /// <param name="IN_Alt">高程</param>
        /// <param name="IN_Save">保存路径</param>
        public static void PRV_Operate(string IN_File, double IN_Lat, double IN_Lng, double IN_Alt, string time, string IN_Save = null)
        {
            Image image = Image.FromFile(IN_File);
            //构建版本
            byte[] _version = { 2, 2, 0, 0 };
            PRV_SetProperty(image, _version, 0x0000, 1);
            //设置南北半球
            PRV_SetProperty(image, BitConverter.GetBytes('N'), 0x0001, 2);
            //设置纬度
            PRV_SetProperty(image, PRV_GetLatlngByte(IN_Lat), 0x0002, 5);
            //设置东西半球
            PRV_SetProperty(image, BitConverter.GetBytes('E'), 0x0003, 2);
            //设置经度
            PRV_SetProperty(image, PRV_GetLatlngByte(IN_Lng), 0x0004, 5);

            Encoding _Encoding = Encoding.UTF8;
            PRV_SetProperty(image, _Encoding.GetBytes(time + '\0'), 0x132, 2);

            //设置高度在海平面上还是下
            byte[] _altref = { 0 };//海平面上
            PRV_SetProperty(image, _altref, 0x0005, 1);
            //设置高度
            byte[] _alt = new byte[8];
            //类型为5可以通过分子/分母的形式表示小数,先乘后除
            int v1 = (int)(IN_Alt * 10000);
            int v2 = 10000;
            Array.Copy(BitConverter.GetBytes(v1), 0, _alt, 0, 4);
            Array.Copy(BitConverter.GetBytes(v2), 0, _alt, 4, 4);
            PRV_SetProperty(image, _alt, 0x0006, 5);

            if (IN_Save == null)
            {
                // save to a memorystream
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);

                // dispose old image
                image.Dispose();

                // save new image to same filename
                Image newImage = Image.FromStream(ms);
                newImage.Save(IN_File);
                return;
            }

            image.Save(IN_Save);
            image.Dispose();
        }

        /// <summary>
        /// 设置图片参数
        /// </summary>
        /// <param name="IN_Image">图片</param>
        /// <param name="IN_Content">byte[] 要写入的内容</param>
        /// <param name="IN_Id">字段ID</param>
        /// <param name="IN_Type">值类型</param>
        private static void PRV_SetProperty(Image IN_Image, byte[] IN_Content, int IN_Id, short IN_Type)
        {
            PropertyItem pi = IN_Image.PropertyItems[0];
            pi.Id = IN_Id;
            pi.Type = IN_Type;
            pi.Value = IN_Content;
            pi.Len = pi.Value.Length;
            IN_Image.SetPropertyItem(pi);
        }

        /// <summary>
        /// 经纬度转byte[]
        /// </summary>
        /// <param name="IN_Latlng">待处理的经度或纬度</param>
        /// <returns></returns>
        private static byte[] PRV_GetLatlngByte(double IN_Latlng)
        {
            double temp;
            temp = Math.Abs(IN_Latlng);
            int degrees = (int)Math.Truncate(temp);
            temp = (temp - degrees) * 60;
            int minutes = (int)Math.Truncate(temp);
            temp = (temp - minutes) * 60;
            //分母设大提高精度
            int secondsNominator = (int)Math.Truncate(10000000 * temp);
            int secondsDenoninator = 10000000;
            byte[] result = new byte[24];
            Array.Copy(BitConverter.GetBytes(degrees), 0, result, 0, 4);
            Array.Copy(BitConverter.GetBytes(1), 0, result, 4, 4);
            Array.Copy(BitConverter.GetBytes(minutes), 0, result, 8, 4);
            Array.Copy(BitConverter.GetBytes(1), 0, result, 12, 4);
            Array.Copy(BitConverter.GetBytes(secondsNominator), 0, result, 16, 4);
            Array.Copy(BitConverter.GetBytes(secondsDenoninator), 0, result, 20, 4);
            return result;
        }
    }
}
