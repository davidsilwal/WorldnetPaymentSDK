using CorePayments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, CoreAPIListener
    {
        private readonly decimal Amount = 19;
        private TransactionInfo TransactionInfo = new TransactionInfo();
        //private CoreSale coreSales;

        public MainWindow()
        {
            InitializeComponent();

            Terminal.Instance.RegisterLogListener(this);
            Terminal.Instance.Initialize(this);
            Terminal.Instance.SetMode(CoreMode.TEST);
            Terminal.Instance.SetLogLevel(LogLevel.LEVEL_FULL);
            Terminal.Instance.InitWithConfiguration("3666001", "TalonSecret123");
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            CoreSale saleDevice = new CoreSale(Math.Round(2m, 2, MidpointRounding.ToEven));
            Terminal.Instance.ProcessSale(saleDevice);
            //OnRequestSetAmount(saleDevice);

         

            //if (Terminal.Instance.GetSettings() == null || Terminal.Instance.GetTerminalUrl().Trim().Equals(""))
            //{

            //    Dispatcher.Invoke(() => labelStatus.Text = "Selected Terminal is not valid.");

            //    return;
            //}
            //if ((Amount == 0) && !Terminal.Instance.IsDelayedAuthEnabled())
            //{
            //    Dispatcher.Invoke(() => labelStatus.Text = "Amount cannot be empty.");
            //    return;
            //}

            //CoreSale saleDevice;
            //if (Terminal.Instance.IsDelayedAuthEnabled())
            //{
            //    saleDevice = new CoreSale();
            //}
            //else
            //{
            //    saleDevice = new CoreSale(Math.Round(Amount, 2, MidpointRounding.ToEven));
            //}

            //if (Terminal.Instance.GetDevice() == DeviceEnum.IDTECH)
            //{
            //    //Dispatcher.Invoke(() => PaymentProcessingStatus.Text = "SWIPE_OR_INSERT_OR_TAP");

            //    saleDevice.coreTransactionInputMethod = CoreTransactionInputMethod.SWIPE_OR_INSERT_OR_TAP;
            //    saleDevice.emvType = CorePayments.CoreEmvType.STANDARD;

            //    //Process sale
            //    saleDevice.signatureCollection = SignatureCollection.AUTOMATIC;

            //    addTipOrTaxToSale(saleDevice);
            //    Terminal.Instance.ProcessSale(saleDevice);

            //    Dispatcher.Invoke(() => labelStatus.Text = "Processing sale");
            //}




        }
  


        private void addTipOrTaxToSale(CoreSale coreSale)
        {

           CoreTip tip = new CoreTip();
            tip.tipType = TipType.FIXED_AMOUNT;
            tip.amount = Math.Round(18m, 2, MidpointRounding.ToEven);
        }


        public void OnBalanceInquiryRetrieved(CoreResponse balanceInquiryResponse)
        {
            throw new NotImplementedException();
        }

        public void OnBillingAddressRequired(CoreSaleKeyed coreSale)
        {
            throw new NotImplementedException();
        }

        public void OnError(CoreError error, string message)
        {
            labelStatus.Text = message;
        }

        public void OnGiftCardDataReturned(Dictionary<string, string> giftCardData)
        {
            throw new NotImplementedException();
        }

        public void OnOfflineSaleRequest(CoreSale saleRequest)
        {
            throw new NotImplementedException();
        }

        public void OnRequestSetAmount(CoreSale coreSale)
        {
           //
            Terminal.Instance.IsDelayedAuthEnabled();
            coreSale.amount = 10;
            Terminal.Instance.SubmitAmount(coreSale, true);

            //Terminal.Instance.EnableDelayedAuth(20);
            //coreSale.amount = 2;
            //Terminal.Instance.SubmitAmount(coreSale, true);
            ////labelDetected.Text = "";
            //labelDetected.Text = "Submited Amout...";

            //coreSales = coreSale;    
            // if (Terminal.Instance.IsDelayedAuthEnabled() == true)

            //if (coreSale.isDelayedAuthEnabled == true)
            //  {

            //Terminal.Instance.SubmitAmount(coreSales, true);
            //decimal amount = 0;
            //int i = 0;
            //while (amount == 0 && i < 3)
            //{
            //    try
            //    {
            //        amount = 5;
            //    }
            //    catch (Exception e)
            //    {
            //        i++;
            //        amount = 0;
            //    }
            //}
            //if (amount == 0)
            //{
            //    Terminal.Instance.CancelTransaction();

            //    return;
            //}
            //coreSale.amount = amount;
            //Terminal.Instance.SubmitAmount(new CoreSale(8), true);
            //   }
        }

        public void OnSaleResponse(CoreSaleResponse response)
        {
            Dispatcher.Invoke(() => labelStatus.Text = "Transaction Completed....");
        }

        public void OnSignatureRequired(CoreSignature signature)
        {
            Terminal.Instance.SubmitSignature(new CoreSignature());
            //if (signature.CheckSignature() == true)
            //    labelDetected.Text = "Signature capture";

        }

        public void OnRefundResponse(CoreRefundResponse response)
        {
            throw new NotImplementedException();
        }

        public void OnReversalRetrieved(CoreResponse response)
        {
            throw new NotImplementedException();
        }

        public void OnRequestUpdateResponse(CoreResponse response)
        {
            CoreTip tip = new CoreTip();
            tip.tipType = TipType.FIXED_AMOUNT;
            tip.amount = Math.Round(18m, 2, MidpointRounding.ToEven);
            CoreUpdate update = new CoreUpdate("UNIQUE REF");
            update.tipAdjustment = tip;
            Terminal.Instance.UpdateTransaction(update);
        }

        public void OnMessage(CoreMessage message)
        {
           
            var c = message.ToString();
            if(c == "CARD_DETECTED")
            {
                labelDetected.Text = message.ToString();
            }
            else
            {
                labelStatus.Text = message.ToString();
            }
            if (message == CoreMessage.DELAYED_AUTH_AMOUNT_CONFIRMED)
            {
                Console.WriteLine($"OnMessage() DELAYED_AUTH_AMOUNT_CONFIRMED" + message.ToString());
            }
            if (message == CoreMessage.DELAYED_AUTH_AMOUNT_DECLINED)
            {
                Console.WriteLine($"OnMessage() DELAYED_AUTH_AMOUNT_DECLINED" + message.ToString());
            }


        }

        public void OnDeviceError(CoreDeviceError error)
        {
            labelStatus.Text = error.ToString();
        }

        public void OnSelectApplication(List<string> applications)
        {
            throw new NotImplementedException();
        }

        public void OnDeviceConnected(DeviceEnum type, Dictionary<string, string> deviceInfo)
        {
            Dispatcher.Invoke(() => labelStatus.Text = "Device Connected");

            Terminal.Instance.EnableDelayedAuth(70m);
            Console.WriteLine("Device Connected!");
        }

        public void OnDeviceDisconnected(DeviceEnum type)
        {
            Dispatcher.Invoke(() => labelStatus.Text = "Device DisConnected");

            Console.WriteLine("Device DisConnected!");
        }

        public void OnDeviceInfoReturned(Dictionary<string, string> deviceInfo)
        {
            throw new NotImplementedException();
        }

        public void OnButtonPressed(Button button)
        {
            throw new NotImplementedException();
        }

        public void OnSettingsRetrieved(CoreSettings settings)
        {
            var data = new Dictionary<string, object>();
            data.Add("emvType", CoreEmvType.QUICK_CHIP);
            Terminal.Instance.InitDevice(DeviceEnum.IDTECH, DeviceConnectionType.USB, data);
        }

        public void OnLogMessage(string message)
        {
            Dispatcher.Invoke(() => labelStatus.Text = message);

        }

        public void OnTransactionListResponse(CoreTransactions response)
        {

            //transactionInfo.dateTime = response.transactionSummary.Select(x => x.transactionDate).FirstOrDefault();
            //transactionInfo.orderNumber = response.transactionSummary.Select(x => x.orderId).FirstOrDefault();
            //transactionInfo.uniqueRef = response.transactionSummary.Select(x => x.uniqueRef).FirstOrDefault();


      
            
        }

        public void OnLoginUrlRetrieved(string url)
        {
            throw new NotImplementedException();
        }

        public void OnRequestCloseBatchResponse(CoreCloseBatchResponse response)
        {
            throw new NotImplementedException();
        }

        public void OnTransactionReportRetrieved(CoreTransactionReports response)
        {
            throw new NotImplementedException();
        }

        public void OnCoreSecureCardResponse(CoreSecureCardResponse response)
        {
            throw new NotImplementedException();
        }
    }

    public class TransactionInfo
    {
        public string dateTime { get; set; }
        public string uniqueRef { get; set; }
        public string orderNumber { get; set; }


    }

}
