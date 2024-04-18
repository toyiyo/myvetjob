// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

namespace myvetjob.Jobs
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Details
    {
        public string JobSummary { get; set; }
        public WhoMayApply WhoMayApply { get; set; }
        public string LowGrade { get; set; }
        public string HighGrade { get; set; }
        public string PromotionPotential { get; set; }
        public string OrganizationCodes { get; set; }
        public string Relocation { get; set; }
        public List<string> HiringPath { get; set; }
        public string TotalOpenings { get; set; }
        public string AgencyMarketingStatement { get; set; }
        public string TravelCode { get; set; }
        public string DetailStatusUrl { get; set; }
        public List<string> MajorDuties { get; set; }
        public string Education { get; set; }
        public string Requirements { get; set; }
        public string Evaluations { get; set; }
        public string HowToApply { get; set; }
        public string WhatToExpectNext { get; set; }
        public string RequiredDocuments { get; set; }
        public string Benefits { get; set; }
        public string BenefitsUrl { get; set; }
        public bool BenefitsDisplayDefaultText { get; set; }
        public string OtherInformation { get; set; }
        public List<string> KeyRequirements { get; set; }
        public string WithinArea { get; set; }
        public string CommuteDistance { get; set; }
        public string ServiceType { get; set; }
        public string AnnouncementClosingType { get; set; }
        public string AgencyContactEmail { get; set; }
        public string SecurityClearance { get; set; }
        public string DrugTestRequired { get; set; }
        public List<string> AdjudicationType { get; set; }
        public bool TeleworkEligible { get; set; }
        public bool RemoteIndicator { get; set; }
        public string AgencyContactPhone { get; set; }
        public string SubAgencyName { get; set; }
        public string ApplyOnlineUrl { get; set; }
        public string PreviewQuestionnaireurl { get; set; }
        public string PositionSensitivitiy { get; set; }
        public List<string> MCOTags { get; set; }
    }

    public static class DetailsExtensions
    {
        public static string GetFormattedDetails(this Details details)
        {
            return $"Job Summary: {details.JobSummary}\n" +
                   $"Agency Marketing Statement: {details.AgencyMarketingStatement}\n" +
                   $"Major Duties: {details.MajorDuties}\n" +
                   $"Education: {details.Education}\n" +
                   $"Requirements: {details.Requirements}\n" +
                   $"Evaluations: {details.Evaluations}\n" +
                   $"How To Apply: {details.HowToApply}\n" +
                   $"What To Expect Next: {details.WhatToExpectNext}\n" +
                   $"Required Documents: {details.RequiredDocuments}\n" +
                   $"Other Information: {details.OtherInformation}";
        }
    }

    public class JobCategory
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class JobGrade
    {
        public string Code { get; set; }
    }

    public class MatchedObjectDescriptor
    {
        public string PositionID { get; set; }
        public string PositionTitle { get; set; }
        public string PositionURI { get; set; }
        public List<string> ApplyURI { get; set; }
        public string PositionLocationDisplay { get; set; }
        public List<PositionLocation> PositionLocation { get; set; }
        public string OrganizationName { get; set; }
        public string DepartmentName { get; set; }
        public List<JobCategory> JobCategory { get; set; }
        public List<JobGrade> JobGrade { get; set; }
        public List<PositionSchedule> PositionSchedule { get; set; }
        public List<PositionOfferingType> PositionOfferingType { get; set; }
        public string QualificationSummary { get; set; }
        public List<PositionRemuneration> PositionRemuneration { get; set; }
        public DateTime PositionStartDate { get; set; }
        public DateTime PositionEndDate { get; set; }
        public DateTime PublicationStartDate { get; set; }
        public DateTime ApplicationCloseDate { get; set; }
        public List<PositionFormattedDescription> PositionFormattedDescription { get; set; }
        public UserArea UserArea { get; set; }
        public string SubAgency { get; set; }
    }

    public class PositionFormattedDescription
    {
        public string Label { get; set; }
        public string LabelDescription { get; set; }
    }

    public class PositionLocation
    {
        public string LocationName { get; set; }
        public string CountryCode { get; set; }
        public string CountrySubDivisionCode { get; set; }
        public string CityName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class PositionOfferingType
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class PositionRemuneration
    {
        public string MinimumRange { get; set; }
        public string MaximumRange { get; set; }
        public string RateIntervalCode { get; set; }
        public string Description { get; set; }
    }

    public class PositionSchedule
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Root
    {
        public string LanguageCode { get; set; }
        public SearchParameters SearchParameters { get; set; }
        public SearchResult SearchResult { get; set; }
    }

    public class SearchParameters
    {
    }

    public class SearchResult
    {
        public int SearchResultCount { get; set; }
        public int SearchResultCountAll { get; set; }
        public List<SearchResultItem> SearchResultItems { get; set; }
        public UserArea UserArea { get; set; }
    }

    public class SearchResultItem
    {
        public string MatchedObjectId { get; set; }
        public MatchedObjectDescriptor MatchedObjectDescriptor { get; set; }
        public int RelevanceRank { get; set; }
    }

    public class UserArea
    {
        public Details Details { get; set; }
        public bool IsRadialSearch { get; set; }
        public string NumberOfPages { get; set; }
    }

    public class WhoMayApply
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }


    public static class PositionScheduleExtensions
    {
        public static EmploymentType ToEmploymentType(this PositionSchedule positionSchedule)
        {
            return positionSchedule.Code switch
            {
                "1" => EmploymentType.FullTime,
                "2" => EmploymentType.PartTime,
                "3" => EmploymentType.ShiftWork,
                "4" => EmploymentType.Intermittent,
                "5" => EmploymentType.JobSharing,
                "6" => EmploymentType.MultipleSchedules,
                _ => EmploymentType.FullTime, // Default to FullTime if the code is not recognized
            };
        }
    }

}