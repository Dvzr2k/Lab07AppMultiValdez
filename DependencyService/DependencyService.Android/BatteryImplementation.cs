using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DependencyService.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;

[assembly: Dependency(typeof(BatteryImplementation))]
namespace DependencyService.Droid
{
    internal class BatteryImplementation :IBattery
    {
        public BatteryImplementation()
        {
        }
        public int RemainingChargePercent
        {
            get
            {
                try
                {
                    using (var filter = new
                    IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Android.App.Application.Context.RegisterReceiver(null, filter))
                        {
                            var level =
                            battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                            var scale =
                            battery.GetIntExtra(BatteryManager.ExtraScale, -1);
                            return (int)Math.Floor(level * 100D / scale);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
        public DependencyService.BatteryStatus Status
        {
            get
            {
                try
                {
                    using (var filter = new
                    IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery =
                        Android.App.Application.Context.RegisterReceiver(null, filter))
                        {
                            int status =
                            battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status ==
                            (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;
                            var chargePlug =
                            battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug ==
                            (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug ==
                            (int)BatteryPlugged.Wireless;
                            isCharging = (usbCharge || acCharge ||
                            wirelessCharge);
                            if (isCharging)
                                return DependencyService.BatteryStatus.Charging;
                            switch (status)
                            {
                                case (int)BatteryStatus.Charging:
                                    return DependencyService.BatteryStatus.Charging;
                                case (int)BatteryStatus.Discharging:
                                    return DependencyService.BatteryStatus.Discharging;
                                case (int)BatteryStatus.Full:
                                    return DependencyService.BatteryStatus.Full;
                                case (int)BatteryStatus.NotCharging:
                                    return DependencyService.BatteryStatus.NotCharging;
                                default:
                                    return DependencyService.BatteryStatus.Unknown;
                            }
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
        public PowerSource PowerSource
        {
            get
            {
                try
                {
                    using (var filter = new
                    IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery =
                        Android.App.Application.Context.RegisterReceiver(null, filter))
                        {
                            int status =
                            battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status ==
                            (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;
                            var chargePlug =
                            battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug ==
                            (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug ==
                            (int)BatteryPlugged.Wireless;
                            isCharging = (usbCharge || acCharge ||
                            wirelessCharge);
                            if (!isCharging)
                                return DependencyService.PowerSource.Battery;
                            else if (usbCharge)
                                return DependencyService.PowerSource.Usb;
                            else if (acCharge)
                                return DependencyService.PowerSource.Ac;
                            else if (wirelessCharge)
                                return DependencyService.PowerSource.Wireless;
                            else
                                return DependencyService.PowerSource.Other;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");

                    throw;
                }
            }
        }
    }
}