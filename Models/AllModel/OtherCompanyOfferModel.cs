using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class OtherCompanyOfferModel
    {
        public int OtherCompanyOfferId { get; set; }
        public string CompanyName { get; set; }
        public string Offer { get; set; }
        public string OfferValidity { get; set; }
        public string EligibleForOffer { get; set; }
        public string CreatedBy { get; set; }

        public static OtherCompanyOfferModel ConvertOtherCompanyOfferModel(DataRow row)
        {
            return new OtherCompanyOfferModel
            {
                OtherCompanyOfferId = row.Table.Columns.Contains("COMPANY_OFFER_ID") ? Convert.ToInt32(row["COMPANY_OFFER_ID"]) : 0,
                CompanyName = row.Table.Columns.Contains("COMPANY_NAME") ? Convert.ToString(row["COMPANY_NAME"]) : "",
                Offer = row.Table.Columns.Contains("OFFER") ? Convert.ToString(row["OFFER"]) : "",
                OfferValidity = row.Table.Columns.Contains("OFFER_VALIDITY") ? Convert.ToString(row["OFFER_VALIDITY"]) : "",
                EligibleForOffer = row.Table.Columns.Contains("ELIGIBLE_FOR_OFFER") ? Convert.ToString(row["ELIGIBLE_FOR_OFFER"]) : "",
                CreatedBy = row.Table.Columns.Contains("CREATE_BY") ? Convert.ToString(row["CREATE_BY"]) : "",
            };
        }
    }
}
