using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using avrwrapper;
using System.Threading;

namespace MorseChat
{
    public partial class Form1 : Form
    {
        static Dictionary<string, string> _letters = new Dictionary<string, string>();
        static Dictionary<char, string> _letters2 = new Dictionary<char, string>();
        static Dictionary<char, AvrBitStream.Morse> _encode = new Dictionary<char, AvrBitStream.Morse>();

        AvrBitStream _bitter = null;

        static Form1()
        {
            _letters["._"] = "A";
            _letters["_..."] = "B";
            _letters["_._."] = "C";
            _letters["_.."] = "D";
            _letters["."] = "E";
            _letters[".._."] = "F";
            _letters["__."] = "G";
            _letters["...."] = "H";
            _letters[".."] = "I";
            _letters[".___"] = "J";
            _letters["_._"] = "K";
            _letters["._.."] = "L";
            _letters["__"] = "M";
            _letters["_."] = "N";
            _letters["___"] = "O";
            _letters[".__."] = "P";
            _letters["__._"] = "Q";
            _letters["._."] = "R";
            _letters["..."] = "S";
            _letters["_"] = "T";
            _letters[".._"] = "U";
            _letters["..._"] = "V";
            _letters[".__"] = "W";
            _letters["_.._"] = "X";
            _letters["_.__"] = "Y";
            _letters["__.."] = "Z";
            _letters[".____"] = "1";
            _letters["..___"] = "2";
            _letters["...__"] = "3";
            _letters["...._"] = "4";
            _letters["....."] = "5";
            _letters["_...."] = "6";
            _letters["__..."] = "7";
            _letters["___.."] = "8";
            _letters["____."] = "9";
            _letters["_____"] = "0";

            foreach (string key in _letters.Keys)
            {
                _letters2[_letters[key][0]] = key;
            }
            //_letters2[' '] = "#";

            _encode['.'] = AvrBitStream.Morse.DIT;
            _encode['_'] = AvrBitStream.Morse.DAH;
            _encode[' '] = AvrBitStream.Morse.CBK;
            _encode['#'] = AvrBitStream.Morse.WBK;
        }
        
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bitter.Dispose();
            _bitter = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _bitter = AvrBitStream.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
                return;
            }
        }

        Thread _thread;
        bool _running = false;
        private void _chkLetTalk_CheckedChanged(object sender, EventArgs e)
        {
            if (_chkLetTalk.Checked)
            {
                int timeout = (int)_spnTimeout.Value;
                _bitter.LetTalk(timeout);
                _bitter.PauseListening(false);
                _thread = new Thread(new ThreadStart(mainThread));
                _running = true;
                _thread.Start();
            }
            else
            {
                _bitter.PauseListening(true);
                _running = false;
                _thread.Join();
                _thread = null;
            }
        }

        private void _btnSendData_Click(object sender, EventArgs e)
        {
            string msg = _txtMessage.Text.ToUpper();
            _txtMessage.Text = "";
            var morse = new List<AvrBitStream.Morse>();
            foreach (string word in msg.Split(' '))
            {
                string code = new string(word.SelectMany((c) => ((c == ' ') ? "#" : (" " + _letters2[c]))).ToArray()) + "#";
                if (morse.Count + code.Length > 64)
                {
                    _bitter.SendPlayData(morse);
                    Thread.Sleep(100);
                    _bitter.SendPlay(morse.Count);
                    Thread.Sleep(360 * morse.Count);
                    morse.Clear();
                }
                morse.AddRange(code.Select((c) => (_encode[c])));
            }
            if (morse.Count > 0)
            {
                _bitter.SendPlayData(morse);
                Thread.Sleep(100);
                _bitter.SendPlay(morse.Count);
                Thread.Sleep(360 * morse.Count);
            }
        }

        enum state
        {
            between_letter, mid_symbol, between_symbol
        }

        private void writeIt(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(writeIt), text);
                return;
            }

            var builder = new StringBuilder(_txtOutput.Text);
            builder.AppendLine(text);
            _txtOutput.Text = builder.ToString();
        }

        private void mainThread()
        {
            Queue<bool> record = new Queue<bool>(_bitter.DequeueAll());
            state curstate = state.between_letter;
            int count = 0;
            string cursym = "";
            string curbits = "";
            List<string> text = new List<string>();
            while (_running)
            {
                if (record.Count == 0)
                {
                    foreach (bool b in _bitter.DequeueAll())
                        record.Enqueue(b);
                    continue;
                }

                bool bit = record.Dequeue();
                curbits += bit ? "=" : " ";
                switch (curstate)
                {
                    case state.between_letter:
                        if (bit)
                        {
                            curstate = state.mid_symbol;
                            count = 1;
                        }
                        else
                            curbits = "";
                        break;
                    case state.mid_symbol:
                        if (bit)
                            count++;
                        else
                        {
                            curstate = state.between_symbol;
                            string nextchar = (count > 5) ? "_" : ".";
                            cursym += nextchar;
                            count = 1;
                        }
                        break;
                    case state.between_symbol:
                        if (bit)
                        {
                            curstate = state.mid_symbol;
                            count = 1;
                        }
                        else if (count > 5)
                        {
                            curstate = state.between_letter;
                            text.Add(cursym);
                            writeIt(curbits);
                            writeIt(cursym);
                            cursym = "";
                            curbits = "";
                        }
                        else
                        {
                            count++;
                        }
                        break;
                }
            }
        }

        private void _rdoDevC_CheckedChanged(object sender, EventArgs e)
        {
            _bitter.SetDevice(_rdoDevC.Checked ? AvrBitStream.Device.C : AvrBitStream.Device.B);
        }
    }
}
