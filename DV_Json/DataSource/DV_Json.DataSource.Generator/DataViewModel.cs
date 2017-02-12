using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DV_Json.DataSource.Generator
{
    public class DataPointViewModel
    {
        public DataPointColor color;
        public float declination;
        public float rightAscension;
    }

    // #55acee
    public class DataPointColor
    {
        public float r = 0.4f;
        public float g = 0.75f;
        public float b = 0.95f;
        public float a = 1;
    }
}
