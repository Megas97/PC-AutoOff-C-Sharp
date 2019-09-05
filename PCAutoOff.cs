//https://www.codeproject.com/Tips/480049/Shut-Down-Restart-Log-off-Lock-Hibernate-or-Sleep -- Operations for PC
//https://stackoverflow.com/questions/102567/how-to-shut-down-the-computer-from-c-sharp -- No console window flash
//https://social.msdn.microsoft.com/Forums/vstudio/en-US/660a1f75-b287-4565-bfdd-75105e0a5527/c-wait-for-x-seconds?forum=netfxbcl -- Timer to wait 'x' milliseconds (app freezes)

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PCAutoOff
{
    class Program
    {
        static void Main(string[] args)
        {
            int timeForActions = 0;
            int timeValue = 0;
            string timeString = "";
            Console.WriteLine("This program will let you change power states on your computer after a set interval.");
            Console.WriteLine("Available measurements: ");
            Console.WriteLine("1 - Seconds");
            Console.WriteLine("2 - Minutes");
            Console.WriteLine("3 - Hours");
            Console.Write("Please choose time measurement: ");
            bool validTimeTypeChoice = false;
            bool validTimeChoice = false;
            int timeChoice = 0;
            while (validTimeTypeChoice == false)
            {
                try
                {
                    timeChoice = Convert.ToInt32(Console.ReadLine());
                    if ((timeChoice > 0) && (timeChoice < 4))
                    {
                        validTimeTypeChoice = true;
                    }
                    else
                    {
                        Console.Write("Please choose a measurement from the list: ");
                    }
                }
                catch (System.Exception ex)
                {
                    Console.Write(ex.Message + " Please choose a measurement: ");
                }
            }
            switch (timeChoice)
            {
                case 1:
                    Console.WriteLine("Time measurement chosen: seconds.");
                    Console.Write("Please enter number of seconds after which action will activate: ");
                    while (validTimeChoice == false)
                    {
                        try
                        {
                            int secs = Convert.ToInt32(Console.ReadLine());
                            while (secs < 0)
                            {
                                Console.Write("Please choose a positive value: ");
                                secs = Convert.ToInt32(Console.ReadLine());
                            }
                            timeForActions = secs * 1000;
                            timeValue = secs;
                            if (secs == 1)
                            {
                                timeString = " second.";
                            }
                            else
                            {
                                timeString = " seconds.";
                            }
                            validTimeChoice = true;
                        }
                        catch (System.Exception ex)
                        {
                            Console.Write(ex.Message + " Please enter number of seconds: ");
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("Time measurement chosen: minutes.");
                    Console.Write("Please enter number of minutes after which action will activate: ");
                    while (validTimeChoice == false)
                    {
                        try
                        {
                            int mins = Convert.ToInt32(Console.ReadLine());
                            while (mins < 0)
                            {
                                Console.Write("Please choose a positive value: ");
                                mins = Convert.ToInt32(Console.ReadLine());
                            }
                            timeForActions = mins * 60 * 1000;
                            timeValue = mins;
                            if (mins == 1)
                            {
                                timeString = " minute.";
                            }
                            else
                            {
                                timeString = " minutes.";
                            }
                            validTimeChoice = true;
                        }
                        catch (System.Exception ex)
                        {
                            Console.Write(ex.Message + " Please enter number of minutes: ");
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Time measurement chosen: hours.");
                    Console.Write("Please enter number of hours after which action will activate: ");
                    while (validTimeChoice == false)
                    {
                        try
                        {
                            int hrs = Convert.ToInt32(Console.ReadLine());
                            while (hrs < 0)
                            {
                                Console.Write("Please choose a positive value: ");
                                hrs = Convert.ToInt32(Console.ReadLine());
                            }
                            timeForActions = hrs * 3600 * 1000;
                            timeValue = hrs;
                            if (hrs == 1)
                            {
                                timeString = " hour.";
                            }
                            else
                            {
                                timeString = " hours.";
                            }
                            validTimeChoice = true;
                        }
                        catch (System.Exception ex)
                        {
                            Console.Write(ex.Message + " Please enter number of hours: ");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("You haven't chosen a time measurement!");
                    break;
            }
            Console.WriteLine("Available actions: ");
            Console.WriteLine("1 - Shutdown");
            Console.WriteLine("2 - Restart");
            Console.WriteLine("3 - Hibernate");
            Console.WriteLine("4 - Sleep");
            Console.WriteLine("5 - Log Off");
            Console.WriteLine("6 - Lock");
            Console.Write("Please choose an action: ");
            bool validActionChoice = false;
            int actionChoice = 0;
            while (validActionChoice == false)
            {
                try
                {
                    actionChoice = Convert.ToInt32(Console.ReadLine());
                    if ((actionChoice > 0) && (actionChoice < 7)){
                        validActionChoice = true;
                    }
                    else
                    {
                        Console.Write("Please choose an action from the list: ");
                    }
                }catch (System.Exception ex)
                {
                    Console.Write(ex.Message + " Please choose an action: ");
                }
            }
            switch (actionChoice)
            {
                case 1:
                    Console.WriteLine("Shutting down computer after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Shutting down computer.");
                    var psi1 = new ProcessStartInfo("shutdown", "/s /t 0");
                    psi1.CreateNoWindow = true;
                    psi1.UseShellExecute = false;
                    Process.Start(psi1);
                    break;
                case 2:
                    Console.WriteLine("Restarting computer after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Restarting computer.");
                    var psi2 = new ProcessStartInfo("shutdown", "/r /t 0");
                    psi2.CreateNoWindow = true;
                    psi2.UseShellExecute = false;
                    Process.Start(psi2);
                    break;
                case 3:
                    Console.WriteLine("Hibernating computer after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Hibernating computer.");
                    SetSuspendState(true, true, true);
                    break;
                case 4:
                    Console.WriteLine("Putting computer to sleep after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Putting computer to sleep.");
                    SetSuspendState(false, true, true);
                    break;
                case 5:
                    Console.WriteLine("Logging off computer after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Logging off computer.");
                    ExitWindowsEx(0, 0);
                    break;
                case 6:
                    Console.WriteLine("Locking computer after " + timeValue + timeString);
                    System.Threading.Thread.Sleep(timeForActions);
                    Console.WriteLine("Locking computer.");
                    LockWorkStation();
                    break;
                default:
                    Console.WriteLine("You haven't chosen an action!");
                    break;
            }
            Environment.Exit(0);
        }

        //Hibernate & Sleep
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        //Lock
        [DllImport("user32")]
        public static extern void LockWorkStation();

        //Log off
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
    }
}