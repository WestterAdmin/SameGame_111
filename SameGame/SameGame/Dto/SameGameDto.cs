using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SameGame
{
    public class SameGameDto
    {
        public int index_C { get; set; }
        public int index_D { get; set; }        
        public int C { get; set; }
        public int D { get; set; }
        public PictureBox Ball { get; set; }

        public string Image { get; set; }
        public bool IsHover { get; set; }
    }
}
