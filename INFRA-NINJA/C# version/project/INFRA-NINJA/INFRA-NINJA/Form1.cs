using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFRA_NINJA
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        // constants for the mouse_input() API function
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        int mode = 0;
        int currkey=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            INFRA_NINJA.Form1.ActiveForm.Close();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hwinit();
           
        }
       



        void hwinit()
        {
            bool isconnected = false;
            int portitr = 0;
            while (isconnected == false)
            {
                Console.WriteLine(portitr);
                
                try{
                    infraninja.PortName = "COM" + portitr;
                    infraninja.Open();
                    if (infraninja.IsOpen)
                    {
                       

                            isconnected = true;
                            Console.WriteLine(infraninja.PortName.ToString());
                            radioButton1.Select();
                            
                        
                    }
                    
                }
                    catch{
                        
                        Console.WriteLine("searching for infra-ninja on:"+infraninja.PortName.ToString());
                        isconnected = false;
                        portitr++;
                        
                    }



                if (portitr>=100)
                {
                    Console.WriteLine("failed to find a infra-ninja device");
                    isconnected = false;
                    return;
                    
                }

               
            }



            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        //menu 2011283611
        //center 2011249307
        //up 2011254939
        //dn 2011246747
        //left 2011271323
        //right 2011259035
        //play 2011298459
        //persist 4294967295
            if (infraninja.IsOpen && infraninja.BytesToRead > 0)
            {
                string currentin = infraninja.ReadLine();
                Console.WriteLine(currentin);
                //mode selection

                if(currentin.Contains("2011283611")==true)
                {
                    currkey = 6;
                    
                    if (mode == 0)
                    {
                        label2.BackColor = System.Drawing.Color.Teal;
                        label1.BackColor = System.Drawing.Color.WhiteSmoke;
                        mode = 1;

                        return;
                    }
                    if (mode == 1)
                    {
                        label1.BackColor = System.Drawing.Color.Teal;
                        label2.BackColor = System.Drawing.Color.WhiteSmoke;
                        mode = 0;
                        return;
                    }
                    
                }
                //click
                if (mode == 0 && currentin.Contains("2011249307"))
                {
                    currkey = 3;
                    mouse_event(MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);

                }
           //upmouse
                if (mode == 0 && currentin.Contains("2011254939"))
                {
                    currkey = 1;
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 5);
                }
                if (mode == 0 && currentin.Contains("4294967295") && currkey == 1)
                {
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 30);
                }
                //dnmouse
                if (mode == 0 && currentin.Contains("2011246747"))
                {
                    currkey = 5;
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 5);
                }
                if (mode == 0 && currentin.Contains("4294967295") && currkey == 5)
                {
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 30);
                }
                //leftmouse
                if (mode == 0 && currentin.Contains("2011271323"))
                {
                    currkey = 2;
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X-5, Cursor.Position.Y );
                }
                if (mode == 0 && currentin.Contains("4294967295") && currkey == 2)
                {
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X-30, Cursor.Position.Y );
                }
                //rightmouse
                if (mode == 0 && currentin.Contains("2011259035"))
                {
                    currkey = 4;
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X + 5, Cursor.Position.Y);
                }
                if (mode == 0 && currentin.Contains("4294967295") && currkey == 4)
                {
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X + 30, Cursor.Position.Y);
                }

                //-------------------------------media mode---------------------------------------------------
                //center
                if (mode == 1 && currentin.Contains("2011249307"))
                {
                    currkey = 3;

                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VOLUME_MUTE);

                }
                //vol+
                if (mode == 1 && currentin.Contains("2011254939"))
                {
                    currkey = 1;

                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VOLUME_UP);
                    
                }
                if (mode == 1 && currentin.Contains("4294967295") && currkey == 1)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VOLUME_UP);
                }
                //vol-
                if (mode == 1 && currentin.Contains("2011246747"))
                {
                    currkey = 5;
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VOLUME_DOWN);
                }
                if (mode == 1 && currentin.Contains("4294967295") && currkey == 5)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.VOLUME_DOWN);
                }
                //prev track
                if (mode == 1 && currentin.Contains("2011271323"))
                {
                    currkey = 2;
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.LEFT);
                }
                if (mode == 1 && currentin.Contains("4294967295") && currkey == 2)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.LEFT);
                }
                //next track
                if (mode == 1 && currentin.Contains("2011259035"))
                {
                    currkey = 4;
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.RIGHT);
                }
                if (mode == 1 && currentin.Contains("4294967295") && currkey == 4)
                {
                    WindowsInput.InputSimulator.SimulateKeyPress(WindowsInput.VirtualKeyCode.RIGHT);
                }
                //play pause
                if (mode == 1 && currentin.Contains("2011275419"))
                {
                    currkey = 7;
                    WindowsInput.InputSimulator.SimulateKeyDown(WindowsInput.VirtualKeyCode.SPACE);
                    
                }
                if (mode == 1 && currentin.Contains("4294967295") && currkey == 7)
                {
                    WindowsInput.InputSimulator.SimulateKeyDown(WindowsInput.VirtualKeyCode.SPACE);
                }

            }



        }

    }
}