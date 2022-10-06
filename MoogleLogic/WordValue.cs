namespace MoogleLogic;

public class WordValue : IWordValue
{
    public void Calculate(string[] paths, AllTextsInfo info, TextsPerDoc textPerDoc)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            foreach (string word in info[paths[i]].Keys)
            {
                TFIDFCalculate(info[paths[i]][word], info[paths[i]][word].Times,
                    textPerDoc[paths[i]].Count(), info.Keys.Count(), info[paths[i]][word].TimesInCorpus);
            }
        } 
    }
    private void TFIDFCalculate(WordProp wordInfo, int times, int totalOfWords, int totalOfDocs, int totalWhereWasFound){
        
        wordInfo.Score = (float)(TF(times, totalOfWords)*IDF(totalOfDocs, totalWhereWasFound));
    }
    private double TF(int times, int totalOfWords){
        double tfValue = (double)times/(double)totalOfWords;
        return tfValue;
    }
    private double IDF(int totalOfDocs, int totalWhereWasFound){
        if(totalWhereWasFound==0){
            return 0;
        }
        else{
            double idfValue = Math.Log(((double)totalOfDocs/(double)totalWhereWasFound));
            return idfValue;
        }
    }
}