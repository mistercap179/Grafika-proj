using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public enum Mjesto { Slobodno, Zauzeto, Vod };

    public class MjestoMatrica
    {
        public Mjesto Polje;
        public int PathId;
    }

    public struct ObjekatEES
    {
        public int x, y;
        public long id;
    };

    public interface IQItem {
        bool Valid { get; set; }
    };

    public struct QItem: IQItem
    {
        public int x, y, dist, xR, yR;
        public IQItem Parent;
        public bool VodPresek;
        public bool Valid { get; set; }
    };

    public struct Linija
    {
        public long firstEnd, secondEnd;
        public string name;
    }

}
