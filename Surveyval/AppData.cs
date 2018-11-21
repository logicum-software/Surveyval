using System;
using System.Collections.Generic;

namespace Surveyval
{
    [Serializable]
    class AppData
    {
        public List<Fragebogen> appFrageboegen { get; set; }
        public List<Frage> appFragen { get; set; }

        public AppData()
        {
            appFrageboegen = new List<Fragebogen>();
            appFragen = new List<Frage>();
        }
    }
}
