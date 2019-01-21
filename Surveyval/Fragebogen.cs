﻿using System;
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
    }
}
