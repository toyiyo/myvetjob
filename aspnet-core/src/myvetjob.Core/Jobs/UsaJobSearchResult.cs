// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

namespace myvetjob.Jobs
{
    public class Details
    {
        public string MajorDuties { get; set; }
        public string Education { get; set; }
        public string Requirements { get; set; }
        public string Evaluations { get; set; }
        public string HowToApply { get; set; }
        public string WhatToExpectNext { get; set; }
        public string RequiredDocuments { get; set; }
        public string Benefits { get; set; }
        public string BenefitsUrl { get; set; }
        public string OtherInformation { get; set; }
        public List<string> KeyRequirements { get; set; }
        public string JobSummary { get; set; }
        public WhoMayApply WhoMayApply { get; set; }
        public string LowGrade { get; set; }
        public string HighGrade { get; set; }
        public string SubAgencyName { get; set; }
        public string OrganizationCodes { get; set; }
    }

    public class GradeBucket
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
    }

    public class JobCategory
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class JobCategoryCode
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
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
    }

    public class Organization
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
    }

    public class PositionFormattedDescription
    {
        public string Content { get; set; }
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

    public class PositionOfferingTypeCode
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
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

    public class PositionScheduleTypeCode
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
    }

    public class Refiners
    {
        public List<Organization> Organization { get; set; }
        public List<GradeBucket> GradeBucket { get; set; }
        public List<SalaryBucket> SalaryBucket { get; set; }
        public List<PositionOfferingTypeCode> PositionOfferingTypeCode { get; set; }
        public List<PositionScheduleTypeCode> PositionScheduleTypeCode { get; set; }
        public List<JobCategoryCode> JobCategoryCode { get; set; }
    }

    public class UsaJobSearchResult
    {
        public string LanguageCode { get; set; }
        public SearchParameters SearchParameters { get; set; }
        public SearchResult SearchResult { get; set; }
    }

    public class SalaryBucket
    {
        public string RefinementName { get; set; }
        public string RefinementCount { get; set; }
        public string RefinementToken { get; set; }
        public string RefinementValue { get; set; }
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
        public double RelevanceRank { get; set; }
    }

    public class UserArea
    {
        public Details Details { get; set; }
        public bool IsRadialSearch { get; set; }
        public Refiners Refiners { get; set; }
        public string NumberOfPages { get; set; }
    }

    public class WhoMayApply
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

}