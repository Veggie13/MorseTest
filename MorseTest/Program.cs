using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using avrwrapper;
using System.Collections;

namespace MorseTest
{
    class Program
    {
        static bool _val = false;
        static List<bool> _record = new List<bool>();
        static AutoResetEvent _evt = new AutoResetEvent(false);
        static Dictionary<string, string> _letters = new Dictionary<string, string>();
        static Dictionary<char, string> _letters2 = new Dictionary<char, string>();
        static string _msg = "";
        static object _locker = new object();

        enum state
        {
            between_letter, mid_symbol, between_symbol
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nKey);
        [DllImport("kernel32.dll")]
        static extern bool Beep(int freq, int duration);

        static int GetKeyState(byte cKey)
        {
            return (int)GetKeyState((int)cKey);
        }

        static Program()
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
        }

#if false
        static void MyThread()
        {
            bool last = false;
            state curstate = state.between_letter;
            int count = 0;
            string cursym = "";
            while (true)
            {
                _evt.WaitOne();
                _evt.Reset();
                bool cur;
                lock (_locker)
                {
                    cur = _val;
                    _record.Add(cur);
                }
                switch (curstate)
                {
                    case state.between_letter:
                        if (cur)
                        {
                            curstate = state.mid_symbol;
                            count = 1;
                            //Console.WriteLine("mid");
                        }
                        break;
                    case state.mid_symbol:
                        if (!cur)
                        {
                            cursym += (count > 10000) ? "_" : ".";
                            curstate = state.between_symbol;
                            count = 1;
                            //Console.WriteLine("bw_s");
                        }
                        else
                        {
                            count++;
                        }
                        break;
                    case state.between_symbol:
                        if (cur)
                        {
                            curstate = state.mid_symbol;
                            count = 1;
                            //Console.WriteLine("mid");
                        }
                        else
                        {
                            count++;
                            if (count > 30000)
                            {
                                curstate = state.between_letter;
                                count = 1;
                                _msg += _letters[cursym];
                                Console.WriteLine(_letters[cursym]);
                                cursym = "";
                            }
                        }
                        break;
                }
                last = cur;
            }
        }
#endif

        static void Main(string[] args)
        {
            AvrBitStream bitter = null;
            try
            {
                bitter = AvrBitStream.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

            bitter.LetTalk(30);

            Queue<bool> record = new Queue<bool>(bitter.DequeueAll());
            state curstate = state.between_letter;
            int count = 0;
            string cursym = "";
            string curbits = "";
            List<string> text = new List<string>();
            while (true)
            {
                Thread.Sleep(1000);
                if (record.Count == 0)
                {
                    foreach (bool b in bitter.DequeueAll())
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
                            Console.WriteLine(curbits);
                            Console.WriteLine(cursym);
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
    }
}
