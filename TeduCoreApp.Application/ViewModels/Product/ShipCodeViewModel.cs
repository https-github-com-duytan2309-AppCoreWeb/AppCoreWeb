using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Application.ViewModels.Product
{
    public class ShipCodeViewModel
    {
        public long Id { get; set; }

        //Hãng Vận Chuyển
        public string Carriers { get; set; }

        //Thời Gian Vận Chuyển

        public DateTime DeliveryTime { get; set; }

        //Chi Phí Thu Hộ
        public decimal CollectionFee { get; set; }

        //Code Theo Quận
        public string ZipCode { get; set; }

        //Tồng Chi Phí
        public decimal Total { get; set; }

        public int IdAddress { get; set; }
    }
}