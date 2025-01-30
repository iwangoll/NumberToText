using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NrToText
{
    /// <summary>
    /// Converts number in text
    /// </summary>
    public class WandleZahlInText
    {
        private long m_nr;
        private enum NrTypes { Count, Feminin, Masc };
        private NrTypes nrt = NrTypes.Count;
        public WandleZahlInText(long i)
        {
            m_nr = i;
        }
        public override string ToString()
        {
            const long milliard = (int)1e9;
            const long million = (int)1e6;
            const long tausend = (int)1e3;
            if (m_nr < tausend)
                return NullTo999();
            string ret = "";
            int md = (int)(m_nr / milliard);
            if (md != 0)
            {
                WandleZahlInText c = new WandleZahlInText(md);
                c.nrt = NrTypes.Feminin;
                ret = ret + c.NullTo999(); // to throw an exception if too big!
                if (md == 1)
                    ret = ret + "milliarde";
                else
                    ret = ret + "milliarden"; // echt milliarder :)
            }
            int mn = (int)((m_nr - md * milliard) / million);
            if (mn != 0)
            {
                WandleZahlInText v = new WandleZahlInText(mn);
                v.nrt = NrTypes.Feminin;
                ret = ret + v.NullTo999();
                if (mn == 1)
                    ret = ret + "millione";
                else
                    ret = ret + "millionen";
            }
            int tz = (int)((m_nr - md * milliard - mn * million) / tausend);
            if (tz != 0)
            {
                WandleZahlInText v = new WandleZahlInText(tz);
                v.nrt = NrTypes.Masc;  // eintausend !!
                ret = ret + v.NullTo999() + "tausend";
            }
            int einheit = (int)(m_nr - md * milliard - mn * million - tz * tausend);
            if (einheit != 0)
            {
                WandleZahlInText b = new WandleZahlInText(einheit);
                ret = ret + b.ToString();
            }
            return ret;
        }

        private string NullTo999()
        {
            if (m_nr > 999)
                throw new Exception();
            if (m_nr < 100)
                return NullTo99();
            int hund = (int)(m_nr / 100);
            string ret = "";
            if (hund != 0)
            {
                WandleZahlInText c = new WandleZahlInText(hund);
                c.nrt = NrTypes.Masc; // neuter ist gleich
                ret = c.ToString() + "hundert";
            }

            // im Original WandleZahlInText c1 = new WandleZahlInText(m_nr % 100);
            long modulo100 = m_nr % 100;
            string textModulo100 = modulo100 == 0 ? "" : new WandleZahlInText(modulo100).ToString();
            ret = ret + textModulo100;
            return ret;
        }

        private string NullTo99()
        {
            if (m_nr > 99)
                throw new Exception();
            if (m_nr < 13)
                return NullTo12();
            if (13 <= m_nr && m_nr < 20)
            {
                WandleZahlInText c = new WandleZahlInText(m_nr - 10);
                return c.GetNrCompForm() + "zehn";

            }

            // bigger then 20
            int deci = (int)m_nr / 10;
            int unit = (int)m_nr % 10;
            string ret = "";
            if (unit != 0)
            {
                WandleZahlInText c = new WandleZahlInText(unit);
                ret = c.ToString() + "und";
            }
            WandleZahlInText x = new WandleZahlInText(deci);
            // im Original ret = ret + x.GetNrCompForm() + "zig";
            ret = ret + x.GetNrCompForm() + (x.m_nr == 3 ? "ßig" : "zig");
            return ret;
        }

        private string GetNrCompForm()
        {
            if (m_nr > 9)
                throw new Exception();
            string r = NullTo12();
            if (m_nr == 2)
                return "zwan"; // für zwanzig
            if (m_nr == 6)
                return "sech"; // für siebzig und siebzehn
            if (m_nr == 7)
                return "sieb"; // für siebzig und siebzehn
            return r; // correct number
        }





        private string NullTo12()
        {
            switch (m_nr)
            {
                case 0:
                    return "nul";
                case 1:
                    {
                        return GetEins();
                    }
                case 2:
                    return "zwei";
                case 3:
                    return "drei";
                case 4:
                    return "vier";
                case 5:
                    return "fünf";
                case 6:
                    return "sechs";
                case 7:
                    return "sieben";
                case 8:
                    return "acht";
                case 9:
                    return "neun";
                case 10:
                    return "zehn";
                case 11:
                    return "elf";
                case 12:
                    return "zwölf";
                default:
                    throw new Exception();
            }
        }

        private string GetEins()
        {
            switch (nrt)
            {
                case NrTypes.Count:
                    return "eins";
                case NrTypes.Masc:
                    return "ein";
                case NrTypes.Feminin:
                    return "eine";
            }
            return "";
        }
    }


}

