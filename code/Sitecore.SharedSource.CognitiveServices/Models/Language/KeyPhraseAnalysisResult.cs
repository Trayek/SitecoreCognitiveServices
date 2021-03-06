﻿
namespace Sitecore.SharedSource.CognitiveServices.Models.Language
{
    public class KeyPhraseAnalysisResult : IKeyPhraseAnalysisResult
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public SentimentDocumentResult KeyPhraseAnalysis { get; set; }

        public string HighlightPhrases(string htmlEntity, string cssClass)
        {
            string s = FieldValue;
            foreach (string p in KeyPhraseAnalysis.KeyPhrases)
            {
                if (string.IsNullOrEmpty(p))
                    continue;

                s = s.Replace(p, $"<{htmlEntity} class=\"{cssClass}\">{p}</{htmlEntity}>");
            }

            return s;
        }
    }
}