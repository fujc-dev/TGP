using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TGP.WinUI
{
    public partial class Form1 : Form
    {

        Color[] colors = new Color[] { Color.FromArgb(255, 39, 94, 176), Color.FromArgb(255, 37, 92, 175), Color.FromArgb(255, 51, 105, 206), Color.FromArgb(255, 53, 135, 226) };
        Color[] highlightColors = new Color[] { Color.FromArgb(255, 59, 114, 196), Color.FromArgb(255, 57, 112, 185), Color.FromArgb(255, 71, 125, 226), Color.FromArgb(255, 73, 155, 246) };
        Color[] pressedColors = new Color[] { Color.FromArgb(255, 79, 134, 216), Color.FromArgb(255, 77, 132, 215), Color.FromArgb(255, 91, 145, 246), Color.FromArgb(255, 93, 175, 255) };


        public Form1()
        {
            InitializeComponent();
        }
    }
}
