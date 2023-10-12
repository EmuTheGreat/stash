using System;
using System.Collections.Generic;

using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var list = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i++)
            {
                for (var j = i + 1; j < documents.Count; j++)
                {
                    list.Add(LevenshteinDistance(documents[i], documents[j]));
                }
            }
            return list;
        }

        public static ComparisonResult LevenshteinDistance(DocumentTokens first, DocumentTokens second)
        {
            var matrix = new double[first.Count + 1, second.Count + 1];
            for (var i = 0; i <= second.Count; ++i) 
                matrix[0, i] = i;
            for (var i = 0; i <= first.Count; ++i) 
                matrix[i, 0] = i;
            for (var i = 1; i <= first.Count; ++i)
                for (var j = 1; j <= second.Count; ++j)
                {
                    if (first[i - 1] == second[j - 1]) matrix[i, j] = matrix[i - 1, j - 1];
                    else
                    {
                        var tokenDistance = Math.Min(TokenDistanceCalculator
                            .GetTokenDistance(first[i - 1], second[j - 1]) + matrix[i - 1, j - 1]
                            , 1 + matrix[i, j - 1]);
                        matrix[i, j] = Math.Min(tokenDistance, 1 + matrix[i - 1, j]);
                    }
                }
            return new ComparisonResult(first, second, matrix[first.Count, second.Count]);
        }
    }
}
