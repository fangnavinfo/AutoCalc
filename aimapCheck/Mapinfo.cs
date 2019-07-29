using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapinfo
{
    class Mapdata
    {
        internal Point AddPoint(double lat, double lon, double speed, double direct, string datetime)
        {
            Point p = new Point(lat, lon, speed, direct, datetime);
            listPoint.Add(p);

            return p;
        }

        internal void WriteTab(string path)
        {
            var dataset = EBop.MapObjects.MapInfo.MiApi.mitab_c_create(path, "tab", "CoordSys Earth Projection 1, 0", 180, -180, 180, -180);
            if (dataset == IntPtr.Zero)
            {
                throw new Exception("Failed to create: " + path + " error: " + EBop.MapObjects.MapInfo.MiApi.mitab_c_getlasterrormsg());
            }

            var flagIndex = (int)EBop.MapObjects.MapInfo.MiApi.mitab_c_add_field((System.IntPtr)dataset, "photoflag", 2, 8, 0, 0, 0);
            var speedIndex = (int)EBop.MapObjects.MapInfo.MiApi.mitab_c_add_field((System.IntPtr)dataset, "speed", 5, 12, 2, 0, 0);
            var directIndex = (int)EBop.MapObjects.MapInfo.MiApi.mitab_c_add_field((System.IntPtr)dataset, "direct", 5, 12, 2, 0, 0);
            var datetimeIndex = (int)EBop.MapObjects.MapInfo.MiApi.mitab_c_add_field((System.IntPtr)dataset, "datetime", 1, 20, 0, 0, 0);

            listPoint.ForEach(point =>
            {
                var feature = EBop.MapObjects.MapInfo.MiApi.mitab_c_create_feature((System.IntPtr)dataset, 1);
              
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_points(feature, 0, 1, ref point.lon, ref point.lat);
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_symbol(feature, 41, 10, 256*255);
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_field(feature, flagIndex, point.flag.ToString());
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_field(feature, speedIndex, point.speed.ToString());
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_field(feature, directIndex, point.direct.ToString());
                EBop.MapObjects.MapInfo.MiApi.mitab_c_set_field(feature, datetimeIndex, point.datetime.ToString());
                EBop.MapObjects.MapInfo.MiApi.mitab_c_write_feature(dataset, feature);
                EBop.MapObjects.MapInfo.MiApi.mitab_c_destroy_feature(feature);
            });

            EBop.MapObjects.MapInfo.MiApi.mitab_c_close((System.IntPtr)dataset);

            if (EBop.MapObjects.MapInfo.MiApi.mitab_c_getlasterrorno() != 0)
            {
                throw new Exception("Last Error: " + EBop.MapObjects.MapInfo.MiApi.mitab_c_getlasterrormsg());
            }
        }

        private List<Point> listPoint = new List<Point>();
    }

    class Point
    {
        public Point(double lat, double lon, double speed, double direct, string datetime)
        {
            this.lat = lat;
            this.lon = lon;
            this.speed = speed;
            this.direct = direct;
            this.datetime = datetime;
        }

        internal void SetFlag(bool flag)
        {
            this.flag = flag ? 1 : 0;
        }

        public double lat;
        public double lon;
        public double speed;
        public double direct;
        public string datetime;
        public int flag;
    }
}
