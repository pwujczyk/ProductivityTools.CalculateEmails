using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public class CalculationDayDB
    {
        public int CalculateEmailsId { get; set; }
        public DateTime Date { get; set; }
        public int MailCountAdd { get; set; }
        public int MailCountSent { get; set; }
        public int MailCountProcessed { get; set; }
        public int TaskCountAdded { get; set; }
        public int TaskCountRemoved { get; set; }
        public int TaskCountFinished { get; set; }
    }
}
