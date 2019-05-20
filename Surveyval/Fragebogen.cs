using System;
using System.Collections.Generic;

namespace Surveyval
{
    [Serializable]
    class Fragebogen
    {
        public String strName { get; set; }
        public List<Frage> Fragen { get; set; }

        public Fragebogen()
        {
            strName = "";
            Fragen = new List<Frage>();
        }

        public Fragebogen(String name, List<Frage> fragen)
        {
            strName = name;
            Fragen = new List<Frage>(fragen);
        }

        public bool Equals(Fragebogen obj)
        {
            if (obj == null) return false;
            if (obj.strName.Equals(strName))
                return true;
            else
                return false;

            //return base.Equals(obj);
        }

        internal Boolean isContaining(Frage tmp)
        {
            foreach (Frage item in Fragen)
            {
                if (item.strFragetext.Equals(tmp.strFragetext))
                    return true;
            }
            return false;
        }

        internal Boolean isContaining(String name)
        {
            foreach (Frage item in Fragen)
            {
                if (item.strFragetext.Equals(name))
                    return true;
            }
            return false;
        }

        internal Boolean removeQuestion(String s)
        {
            int i = 0;
            foreach (Frage item in Fragen)
            {
                if (item.strFragetext.Equals(s))
                {
                    i = Fragen.IndexOf(item);
                }
            }

            try
            {
                Fragen.RemoveAt(i);
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return false;
                throw;
            }
        }
    }
}
