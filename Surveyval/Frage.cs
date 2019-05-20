using System;
using System.Windows.Controls;

namespace Surveyval
{
    [Serializable]
    class Frage
    {
        public String strFragetext { get; set; }
        public int nAntwortart { get; set; }
        public Boolean bInSelected { get; set; }

        public Frage()
        {
            strFragetext = "";
            nAntwortart = 0;
            bInSelected = false;
        }

        public Frage(String fragetext, int antwortart)
        {
            strFragetext = fragetext;
            nAntwortart = antwortart;
            bInSelected = false;
        }
    }
}
