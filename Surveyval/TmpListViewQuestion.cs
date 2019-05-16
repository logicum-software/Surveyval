using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveyval
{
    class TmpListViewQuestion
    {
        public String strName { get; set; }
        public Boolean IsChecked { get; set; }

        public TmpListViewQuestion()
        {
            strName = "";
            IsChecked = false;
        }

        public TmpListViewQuestion(String strTmp, Boolean bTmp)
        {
            strName = strTmp;
            IsChecked = bTmp;
        }
    }
}
