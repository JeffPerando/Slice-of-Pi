
using Newtonsoft.Json;

namespace Main.Models.ATTOM
{
    // var myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Status
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pagesize")]
        public int Pagesize { get; set; }

        [JsonProperty("transactionID")]
        public string TransactionID { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("fips")]
        public string Fips { get; set; }

        [JsonProperty("apn")]
        public string Apn { get; set; }

        [JsonProperty("attomId")]
        public int AttomId { get; set; }
    }

    public class Lot
    {
        [JsonProperty("lotNum")]
        public string LotNum { get; set; }

        [JsonProperty("lotSize1")]
        public double LotSize1 { get; set; }

        [JsonProperty("lotSize2")]
        public int LotSize2 { get; set; }

        [JsonProperty("poolType")]
        public string PoolType { get; set; }
    }

    public class Area
    {
        [JsonProperty("blockNum")]
        public string BlockNum { get; set; }

        [JsonProperty("locType")]
        public string LocType { get; set; }

        [JsonProperty("countrySecSubd")]
        public string CountrySecSubd { get; set; }

        [JsonProperty("countyUse1")]
        public string CountyUse1 { get; set; }

        [JsonProperty("munCode")]
        public string MunCode { get; set; }

        [JsonProperty("munName")]
        public string MunName { get; set; }

        [JsonProperty("srvyRange")]
        public string SrvyRange { get; set; }

        [JsonProperty("srvySection")]
        public string SrvySection { get; set; }

        [JsonProperty("srvyTownship")]
        public string SrvyTownship { get; set; }

        [JsonProperty("subdName")]
        public string SubdName { get; set; }

        [JsonProperty("taxCodeArea")]
        public string TaxCodeArea { get; set; }
    }

    public class Address
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countrySubd")]
        public string CountrySubd { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2")]
        public string Line2 { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("matchCode")]
        public string MatchCode { get; set; }

        [JsonProperty("oneLine")]
        public string OneLine { get; set; }

        [JsonProperty("postal1")]
        public string Postal1 { get; set; }

        [JsonProperty("postal2")]
        public string Postal2 { get; set; }

        [JsonProperty("postal3")]
        public string Postal3 { get; set; }
    }

    public class Location
    {
        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("distance")]
        public float Distance { get; set; }

        [JsonProperty("geoId")]
        public string GeoId { get; set; }
    }

    public class Summary
    {
        [JsonProperty("archStyle")]
        public string ArchStyle { get; set; }

        [JsonProperty("absenteeInd")]
        public string AbsenteeInd { get; set; }

        [JsonProperty("propClass")]
        public string PropClass { get; set; }

        [JsonProperty("propSubType")]
        public string PropSubType { get; set; }

        [JsonProperty("propType")]
        public string PropType { get; set; }

        [JsonProperty("yearBuilt")]
        public int YearBuilt { get; set; }

        [JsonProperty("propLandUse")]
        public string PropLandUse { get; set; }

        [JsonProperty("propIndicator")]
        public int PropIndicator { get; set; }

        [JsonProperty("legal1")]
        public string Legal1 { get; set; }

        [JsonProperty("quitClaimFlag")]
        public string QuitClaimFlag { get; set; }

        [JsonProperty("REOflag")]
        public string REOflag { get; set; }

        [JsonProperty("levels")]
        public int Levels { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("storyDesc")]
        public string StoryDesc { get; set; }

        [JsonProperty("unitsCount")]
        public int UnitsCount { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }

        [JsonProperty("viewCode")]
        public string ViewCode { get; set; }
    }

    public class Size
    {
        [JsonProperty("bldgSize")]
        public int BldgSize { get; set; }

        [JsonProperty("grossSize")]
        public int GrossSize { get; set; }

        [JsonProperty("grossSizeAdjusted")]
        public int GrossSizeAdjusted { get; set; }

        [JsonProperty("groundFloorSize")]
        public int GroundFloorSize { get; set; }

        [JsonProperty("livingSize")]
        public int LivingSize { get; set; }

        [JsonProperty("sizeInd")]
        public string SizeInd { get; set; }

        [JsonProperty("universalSize")]
        public int UniversalSize { get; set; }
    }

    public class Rooms
    {
        [JsonProperty("bathFixtures")]
        public int BathFixtures { get; set; }

        [JsonProperty("bathsFull")]
        public int BathsFull { get; set; }

        [JsonProperty("bathsTotal")]
        public float BathsTotal { get; set; }

        [JsonProperty("beds")]
        public int Beds { get; set; }

        [JsonProperty("roomsTotal")]
        public int RoomsTotal { get; set; }
    }

    public class Interior
    {
        [JsonProperty("bsmtSize")]
        public int BsmtSize { get; set; }

        [JsonProperty("bsmtType")]
        public string BsmtType { get; set; }

        [JsonProperty("fplcCount")]
        public int FplcCount { get; set; }

        [JsonProperty("fplcInd")]
        public string FplcInd { get; set; }

        [JsonProperty("fplcType")]
        public string FplcType { get; set; }
    }

    public class Construction
    {
        [JsonProperty("condition")]
        public string Condition { get; set; }

        [JsonProperty("roofCover")]
        public string RoofCover { get; set; }

        [JsonProperty("roofShape")]
        public string RoofShape { get; set; }

        [JsonProperty("wallType")]
        public string WallType { get; set; }
    }

    public class Parking
    {
        [JsonProperty("prkgSize")]
        public int PrkgSize { get; set; }

        [JsonProperty("prkgSpaces")]
        public string PrkgSpaces { get; set; }
    }

    public class Utilities
    {
        [JsonProperty("heatingFuel")]
        public string HeatingFuel { get; set; }

        [JsonProperty("heatingType")]
        public string HeatingType { get; set; }
    }

    public class Amount
    {
        [JsonProperty("saleAmt")]
        public int SaleAmt { get; set; }

        [JsonProperty("saleCode")]
        public string SaleCode { get; set; }

        [JsonProperty("saleRecDate")]
        public string SaleRecDate { get; set; }

        [JsonProperty("saleDisclosureType")]
        public int SaleDisclosureType { get; set; }

        [JsonProperty("saleDocType")]
        public string SaleDocType { get; set; }

        [JsonProperty("saleDocNum")]
        public string SaleDocNum { get; set; }

        [JsonProperty("saleTransType")]
        public string SaleTransType { get; set; }
    }

    public class Calculation
    {
        [JsonProperty("pricePerBed")]
        public double PricePerBed { get; set; }

        [JsonProperty("pricePerSizeUnit")]
        public double PricePerSizeUnit { get; set; }
    }

    public class Sale
    {
        [JsonProperty("sequenceSaleHistory")]
        public int SequenceSaleHistory { get; set; }

        [JsonProperty("sellerName")]
        public string SellerName { get; set; }

        [JsonProperty("saleSearchDate")]
        public string SaleSearchDate { get; set; }

        [JsonProperty("saleTransDate")]
        public string SaleTransDate { get; set; }

        [JsonProperty("transactionIdent")]
        public string TransactionIdent { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("calculation")]
        public Calculation Calculation { get; set; }
    }

    public class Building
    {
        [JsonProperty("size")]
        public Size Size { get; set; }

        [JsonProperty("rooms")]
        public Rooms Rooms { get; set; }

        [JsonProperty("interior")]
        public Interior Interior { get; set; }

        [JsonProperty("construction")]
        public Construction Construction { get; set; }

        [JsonProperty("parking")]
        public Parking Parking { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    //WARNING TO FUTURE DEV: THIS WAS BLANK! THESE FIELD ARE GUESSES.
    public class Appraised
    {
        [JsonProperty("appdImprValue")]
        public int AppdImprValue { get; set; }

        [JsonProperty("appdLandValue")]
        public int AppdLandValue { get; set; }

        [JsonProperty("appdTtlValue")]
        public int AppdTtlValue { get; set; }
    }

    public class Owner1
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstNameAndMi")]
        public string FirstNameAndMi { get; set; }
    }

    public class Owner2
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstNameAndMi")]
        public string FirstNameAndMi { get; set; }
    }

    public class Owner3
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstNameAndMi")]
        public string FirstNameAndMi { get; set; }
    }

    public class Owner4
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstNameAndMi")]
        public string FirstNameAndMi { get; set; }
    }

    public class Owner
    {
        [JsonProperty("corporateIndicator")]
        public string CorporateIndicator { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner1")]
        public Owner1 Owner1 { get; set; }

        [JsonProperty("owner2")]
        public Owner2 Owner2 { get; set; }

        [JsonProperty("owner3")]
        public Owner3 Owner3 { get; set; }

        [JsonProperty("owner4")]
        public Owner4 Owner4 { get; set; }

        [JsonProperty("absenteeOwnerStatus")]
        public string AbsenteeOwnerStatus { get; set; }

        [JsonProperty("mailingAddressOneLine")]
        public string MailingAddressOneLine { get; set; }
    }

    public class FirstConcurrent
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("lenderLastName")]
        public string LenderLastName { get; set; }

        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("loanTypeCode")]
        public string LoanTypeCode { get; set; }

        [JsonProperty("deedType")]
        public string DeedType { get; set; }

        [JsonProperty("interestRateType")]
        public string InterestRateType { get; set; }
    }

    public class SecondConcurrent
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

    }

    public class Title
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }
    }

    public class Mortgage
    {
        [JsonProperty("FirstConcurrent")]
        public FirstConcurrent FirstConcurrent { get; set; }

        [JsonProperty("SecondConcurrent")]
        public SecondConcurrent SecondConcurrent { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }
    }

    public class Assessment
    {
        [JsonProperty("appraised")]
        public Appraised Appraised { get; set; }

        [JsonProperty("assessed")]
        public Assessed Assessed { get; set; }

        [JsonProperty("market")]
        public Market Market { get; set; }

        [JsonProperty("tax")]
        public Tax Tax { get; set; }

        [JsonProperty("improvementPercent")]
        public int ImprovementPercent { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("mortgage")]
        public Mortgage Mortgage { get; set; }
    }

    public class Vintage
    {
        [JsonProperty("lastModified")]
        public string LastModified { get; set; }

        [JsonProperty("pubDate")]
        public string PubDate { get; set; }
    }

    public class Assessed
    {
        [JsonProperty("assdImprValue")]
        public int AssdImprValue { get; set; }

        [JsonProperty("assdLandValue")]
        public int AssdLandValue { get; set; }

        [JsonProperty("assdTtlValue")]
        public int AssdTtlValue { get; set; }
    }

    public class Calculations
    {
        [JsonProperty("calcImprInd")]
        public string CalcImprInd { get; set; }

        [JsonProperty("calcImprValue")]
        public int CalcImprValue { get; set; }

        [JsonProperty("calcLandInd")]
        public string CalcLandInd { get; set; }

        [JsonProperty("calcLandValue")]
        public int CalcLandValue { get; set; }

        [JsonProperty("calcTtlInd")]
        public string CalcTtlInd { get; set; }

        [JsonProperty("calcTtlValue")]
        public int CalcTtlValue { get; set; }
    }

    public class Market
    {
        [JsonProperty("mktTtlValue")]
        public int MktTtlValue { get; set; }

        [JsonProperty("mktImprValue")]
        public int? MktImprValue { get; set; }

        [JsonProperty("mktLandValue")]
        public int? MktLandValue { get; set; }
    }

    public class Tax
    {
        [JsonProperty("assessorYear")]
        public int AssessorYear { get; set; }

        [JsonProperty("taxAmt")]
        public double TaxAmt { get; set; }

        [JsonProperty("taxYearAssessed")]
        public int TaxYearAssessed { get; set; }

        [JsonProperty("taxYear")]
        public int? TaxYear { get; set; }
    }

    public class AssessmentHistory
    {
        [JsonProperty("assessed")]
        public Assessed Assessed { get; set; }

        [JsonProperty("calculations")]
        public Calculations Calculations { get; set; }

        [JsonProperty("market")]
        public Market Market { get; set; }

        [JsonProperty("tax")]
        public Tax Tax { get; set; }

        [JsonProperty("lastModified")]
        public string LastModified { get; set; }
    }

    public class Property
    {
        [JsonProperty("identifier")]
        public Identifier Identifier { get; set; }

        [JsonProperty("lot")]
        public Lot Lot { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("utilities")]
        public Utilities Utilities { get; set; }

        [JsonProperty("sale")]
        public Sale Sale { get; set; }

        [JsonProperty("building")]
        public Building Building { get; set; }

        [JsonProperty("assessment")]
        public Assessment? Assessment { get; set; }

        [JsonProperty("vintage")]
        public Vintage Vintage { get; set; }

        [JsonProperty("assessmenthistory")]
        public List<AssessmentHistory>? AssessmentHistory { get; set; }

    }

}
