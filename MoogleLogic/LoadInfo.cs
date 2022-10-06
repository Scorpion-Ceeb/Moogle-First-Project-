using System.Text;

namespace MoogleLogic;

public class LoadInfo : ILoadInfo
{
    public void Load(string[] paths, AllTextsInfo info, TextsPerDoc textsPerDoc)
    {
        for(int i = 0; i < paths.Length; i++)
        {
            info.TryAdd(paths[i], new TextInfo());
            textsPerDoc.Add(paths[i], new List<string>());
            StringBuilder textBuilder = new StringBuilder();
            string wordTemp = "";

            LoadTextFromStream(paths[i], wordTemp, textBuilder);
            
            string[] text = BuildText(textBuilder);
            
            LoadTextInfo(text, paths, info, textsPerDoc, i);
        }
    }
    private void LoadTextFromStream(string path, string tempWord, StringBuilder text)
    {
        using (StreamReader sr = new StreamReader($@"{path}")){
            while((tempWord = sr.ReadLine()) != null)
                text.Append(tempWord + " ");
        }
    }
    private void LoadTextInfo(string[] text, string[] paths, AllTextsInfo info, TextsPerDoc textsPerDoc, int i)
    {
        int wordIndex = 0;

        for (int j = 0; j < text.Length; j++)
        {
            textsPerDoc[paths[i]].Add(text[j]);
            string word = text[j].ToLower();
            
            if(!info[paths[i]].TryAdd(word, new WordProp(1, 1, wordIndex))){
                info[paths[i]][word].Times++;
                info[paths[i]][word].Indexes.Add(wordIndex);
            }
            else
            {
                for (int k = 1; k <= i; k++)
                {
                    if(info[paths[i-k]].ContainsKey(word)){
                        info[paths[i-k]][word].TimesInCorpus += 1;
                        info[paths[i]][word].TimesInCorpus = info[paths[i-k]][word].TimesInCorpus;
                    }
                }
            }
            wordIndex++; 
        }
    }
    private string[] BuildText(StringBuilder builder)
    {
        return builder.ToString().Split(new char[]{' ', '.', ',', ':', ';', '(',')', '-', '¡', '!','<', '>', '¿', '?', '—', '«', '»'}, StringSplitOptions.RemoveEmptyEntries);
    }
}