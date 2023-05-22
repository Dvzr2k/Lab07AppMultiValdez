using DependencyService.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(DependencyService.Droid.Services.QrScanningService))]
namespace DependencyService.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var options = new MobileBarcodeScanningOptions();
            var scanner = new MobileBarcodeScanner();

            var context = Xamarin.Essentials.Platform.CurrentActivity;  

            var scanResult = await scanner.Scan(context, options);
            return scanResult.Text;
        }
    }
}