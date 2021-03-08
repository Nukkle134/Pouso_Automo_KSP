using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using System;

namespace ksp
{

    class Program
    {

        static void Main(string[] args)
        {
            // Conexão com o KRPC
            Console.WriteLine($"Tentando conectar!");
            var connection_ = new Connection(name: "kr2pc");
            var krpc = connection_.KRPC();
            bool reached_off_alture = false;
            bool reached_off_alture2 = false;
            bool pushed_last_burn = false;
            using (var connection = new Connection())
            {
                var spaceCenter = connection.SpaceCenter();
                var vessel = spaceCenter.ActiveVessel;
                var control = vessel.Control;
                vessel.Name = "SpaceFlightAcademy";
                var srfFrame = vessel.Orbit.Body.ReferenceFrame;
                var flightInfo = vessel.Flight();
                System.Threading.Thread.Sleep(5000);
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\contagem.wav");
                player.Play();
                Console.WriteLine(flightInfo.MeanAltitude);
                Console.WriteLine($"10");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"9");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"8");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"7");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"6");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"5");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"4");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"3");
                control.Throttle = 1;
                control.ActivateNextStage();
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"2");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"1");

                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"GO FOR LAUNCH!");
                control.RCS = true;
                control.Gear = false;
                control.SAS = true;
                control.SASMode = 0;
                control.ActivateNextStage();
                control.Throttle = 1;
                vessel.Flight();
                System.Threading.Thread.Sleep(10000);
                System.Threading.Thread.Sleep(1000);
                reached_off_alture = true;
                System.Threading.Thread.Sleep(12500);
                reached_off_alture2 = true;
                while (true)
                {
                    if (reached_off_alture2 == true)
                    {
                        control.Parachutes = true;
                        control.ActivateNextStage();
                        System.Threading.Thread.Sleep(5000);
                        control.Throttle = 0.00f;
                        control.SASMode = SASMode.Radial;
                        control.SetActionGroup(1, true);
                        control.Brakes = true;
                        break;

                    }
                    System.Threading.Thread.Sleep(0);
                }

                while (true)
                {
                    var srfSpeed = vessel.Flight(srfFrame).Speed;

                    if (srfSpeed < 0.5 && pushed_last_burn == true)
                    {
                        control.Throttle = 0;
                        control.Brakes = false;
                        System.Threading.Thread.Sleep(500);
                        control.Gear = false;
                        break;
                    }
                    if (flightInfo.MeanAltitude <= 800 && flightInfo.MeanAltitude >= 701 && reached_off_alture == true)
                    {
                        control.Gear = true;
                    }
                    else if (flightInfo.SurfaceAltitude <= 860)
                    {

                        Console.WriteLine("Throttle:" + (float)((1 * (Math.Sqrt(srfSpeed / 4)) / 1.5) / 1.2));
                        control.Throttle = (float)((1 * (Math.Sqrt(srfSpeed / 4)) / 1.5) / 2);
                        pushed_last_burn = true;
                        if ((float)((1 * (Math.Sqrt(srfSpeed / 4)) / 1.5) / 1.77) < 0.26)
                        {
                            control.Throttle = 0;
                            control.Brakes = false;
                            System.Threading.Thread.Sleep(1500);

                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(0);
                }
            }
            Console.WriteLine($"Connected Sucessfuly");
            Console.ReadKey();

        }
    }
}
