using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCTO_Progetto01
{
    class Paginazione
    {
        public int PageLenght { get; set; }
        public int CurrentPage { get; set; }

        public Paginazione(int pageLenght, int currentPage)
        {
            PageLenght = pageLenght;
            CurrentPage = currentPage;
        }
    }
}
