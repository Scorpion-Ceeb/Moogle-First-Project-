namespace MoogleLogic;

public class WordsValue : IWordsValue
{
    public void GetValue(Document[] docs)
    {
        for (int i = 0; i < docs.Length; i++)
        {
            foreach (string word in docs[i].Info.Keys)
            {
                TFIDFCalculate(docs[i].Info[word], docs[i].Text.Count(), docs[i].Info.Keys.Count());
            }
        } 
    }
    private void TFIDFCalculate(WordProp wordInfo, int totalOfWords, int totalOfDocs){
        
        wordInfo.Score = (float)(TF(wordInfo.Times, totalOfWords)*IDF(totalOfDocs, wordInfo.TimesInCorpus));
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