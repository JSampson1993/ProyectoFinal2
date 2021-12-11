using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNote.Models
{
    public class Notas
    {
        public AudioRecorderService recorder { set; get; }
        public AudioPlayer player { set; get; }
    }
}
