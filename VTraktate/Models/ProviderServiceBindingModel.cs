using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTraktate.Domain;

namespace VTraktate.Models
{
    public class ProviderServiceBindingModel
    {
        public int? Id { get; set; }
        public int ServiceTypeId { get; set; }
        public int qaStars { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public string Comment { get; set; }
        public int uomId { get; set; }
        public int currencyId { get; set; }
        public int ProviderId { get; set; }

        public static Service ToService(ProviderServiceBindingModel model, int providerId)
        {
            var item = new Service
            {
                ServiceTypeId = model.ServiceTypeId,

                CurrencyId = model.currencyId,
                ProviderId = providerId,

                QA = new QA
                {
                    Comment = model.Comment,
                    Grade = 0,
                    Stars = (Stars)model.qaStars
                },
                Rate = new ServiceRate
                {
                    MaxRate = model.MaxRate,
                    Minrate = model.MinRate
                },
                ServiceUOMId = model.uomId
            };
            return item;
        }
    }
}