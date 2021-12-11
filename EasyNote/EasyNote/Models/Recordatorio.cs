using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNote.Models
{
    public class Recordatorio
    {
        public String notasId { set; get; }
        public String notas_Fecha { set; get; }
        public Byte[] notas_Image { set; get; }
        public String notas_Audio { set; get; }
        public String NotasDescrip { set; get; }
        public String userId { set; get; }
    }
}
