using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NrToText
{
    enum EnumTest
    {
        Von1bis100,
        /// <summary>
        /// für das Erzeugen von Serienbriefen im Excel:
        /// man ermittelt die aktuellen Beträge, übergibt sie durch Komma getrennt an InTextZeilen. 
        /// Das Ergebnis speichert man in einer extra Tabelle der Mappe
        /// und fügt (z.B. in der Tabelle für den Serienbrief) mittels SVERWEIS die Bezeichnung 
        /// für die Zahl in der Spalte "Betrag" in die Spalte "BetragText" ein.
        /// </summary>
        SpezielleZahlenFuerExcel_SVERWEIS, 
        /// <summary>
        /// klappt es auch für große Zahlen
        /// </summary>
        GrosseZahlen,
        /// <summary>
        /// im Original 37 -> siebenunddreizig
        /// </summary>
        Dreissig,
        /// <summary>
        /// im Original 100 -> einhundertnul
        /// </summary>
        HundertRund  
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            WasTun(EnumTest.SpezielleZahlenFuerExcel_SVERWEIS);
            //Console.ReadKey();
        }

        private static void WasTun(EnumTest wasTun)
        {
            string name = wasTun.ToString();
            Debug.WriteLine($"{name}:\n{new string('=', name.Length+1)}");
            switch (wasTun)
            {
                case EnumTest.Von1bis100:
                    for (int i = 1; i < 100; i++)
                    {
                        Debug.WriteLine(InText(i));
                    }
                    break;
                case EnumTest.SpezielleZahlenFuerExcel_SVERWEIS:
                    Debug.WriteLine(InTextZeilen(10, 12, 15, 20, 24, 25, 30, 35, 40, 45, 50, 57, 60, 65, 69, 70, 75, 100, 130, 144, 150, 165, 180, 200, 300, 311, 500));
                    break;
                case EnumTest.GrosseZahlen:
                    Debug.WriteLine(InTextZeilen(101215, 2024253035));
                    break;
                case EnumTest.Dreissig:
                    Debug.WriteLine(InText(37));
                    break;
                case EnumTest.HundertRund:
                    Debug.WriteLine(InText(100));
                    break;
                default:
                    break;
            }
        }

        private static string InTextZeilen(params int[] arrayV)
        {
            List<string> list = arrayV.ToList().Select(x => InText(x)).ToList();
            return String.Join("\r\n", list);
        }
        private static string InText(int v)
        {
            return $"{v}\t{new WandleZahlInText(v).ToString()}";
        }
    }
}
