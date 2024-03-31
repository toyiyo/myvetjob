using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace myvetjob.Localization
{
    public static class myvetjobLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(myvetjobConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(myvetjobLocalizationConfigurer).GetAssembly(),
                        "myvetjob.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
