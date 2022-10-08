using System.Text;

namespace MoogleLogic;

public class LoadInfo : ILoadInfo
{
    public Document[] Load(string[] paths)
    {
        List<Document> documents = new List<Document>();

        for(int i = 0; i < paths.Length; i++)
        {
            TextInfo info = new TextInfo();
            StringBuilder textBuilder = new StringBuilder();

            LoadTextFromStream(paths[i], textBuilder);
            
            string[] text = BuildText(textBuilder);
            
            LoadTextInfo(text, info, documents);

            documents.Add(new Document(text, paths[i], info));
        }
        return documents.ToArray();
    }
    private void LoadTextFromStream(string path, StringBuilder text)
    {
        string wordTemp = "";
        using (StreamReader sr = new StreamReader($@"{path}")){
            while((wordTemp = sr.ReadLine()!) != null)
                text.Append(wordTemp + " ");
        }
    }
    private void LoadTextInfo(string[] text, TextInfo info, List<Document> documents)
    {
        int wordIndex = 0;

        for (int j = 0; j < text.Length; j++)
        {
            string word = text[j].ToLower();
            
            if(!info.TryAdd(word, new WordProp(1, 1, wordIndex))){
                info[word].Times++;
                info[word].Indexes.Add(wordIndex);
            }
            else
            {
                for (int k = 0; k < documents.Count(); k++)
                {
                    if(documents[k].Info.ContainsKey(word)){
                        documents[k].Info[word].TimesInCorpus += 1;
                        info[word].TimesInCorpus = documents[k].Info[word].TimesInCorpus;
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