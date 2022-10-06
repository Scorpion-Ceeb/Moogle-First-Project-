namespace MoogleLogic;

public class ReadQuery : IReadQuery
{
    public string[] Read(string[] wordsQuery, Searcher searcher)
    {
        int distanceOp = 0;
        List<string> words = new List<string>();
        List<Operator> tempOperators = new List<Operator>();

        for (int i = 0; i < wordsQuery.Length; i++)
        {
            if(wordsQuery[i] == "~" && i != wordsQuery.Length - 1 && wordsQuery[i+1].Any(word => char.IsLetterOrDigit(word)) && Operator.CleanWord(wordsQuery[i+1]).ToLower() != words.Last()){
                Operator.AddDistanceOperator(tempOperators, words.Last(), Operator.CleanWord(wordsQuery[i+1]).ToLower(), distanceOp++);
            }
            else if(wordsQuery[i].Any(word => char.IsLetterOrDigit(word))){
                words.Add(Operator.CutWithOutOperator(tempOperators, wordsQuery[i]).ToLower());
            }
        }
        searcher.Operators = tempOperators.ToArray();
        return Contains(words.ToArray(), searcher);
    }
    private string[] Contains(string[] wordsToFind, Searcher searcher){
        //Revisar en "info" si la palabras con o sin operadores se encuentran en los documentos
        #region variables
        List<string> tempPaths = new List<string>(searcher.Paths);

        List<string> tempWordsToFind = new List<string>(wordsToFind);

        searcher.WordsMask = new bool[wordsToFind.Length];
        searcher.WordsList = new string[wordsToFind.Length];

        Array.Copy(wordsToFind, searcher.WordsList, searcher.WordsList.Length);
        #endregion

        //Reviso las palabras que contengan operadores de existencia en los documentos para eliminar
        //el documento de la busqueda si existe la palabra o no dependiendo del operador(!, ^)
        tempWordsToFind = CommonWords(tempWordsToFind, searcher.Paths, searcher.Info);
        if(searcher.Operators==null)
        {
            for (int i = 0; i < wordsToFind.Length; i++)
            {
                if(NoExistsWord(wordsToFind[i], searcher.Paths, tempWordsToFind, searcher.Info)) 
                    searcher.WordsMask[i] = true;
            }
        }
        else{
            List<Operator> newOper = new List<Operator>(searcher.Operators);
            Operator? oper = Operator.ReturnOperator('~', searcher.Operators);
            newOper.Remove(oper);
            for (int i = 0; i < wordsToFind.Count(); i++)
            {
                // Deben estar las dos palabras del operador distancia
                if(Operator.ContainsOperator(wordsToFind[i], '~', searcher.Operators) && NoExistsWord(wordsToFind[i], searcher.Paths, tempWordsToFind, searcher.Info)){
                    
                    //Si no se encuentra alguna de las dos perdera el operador de distancia
                    for (int j = 0; j < oper?.WordAndTimes.Count(); j++)
                    {
                        for (int k = 0; k < oper.WordAndTimes[j].Count(); k++)
                        {
                            if(oper.WordAndTimes[j][k].Word == wordsToFind[i]){
                                searcher.WordsMask[i] = true;
                                oper.WordAndTimes.RemoveAt(j);
                                break;
                            }
                        }
                    }
                    if(oper?.WordAndTimes.Count()==0) oper = null;
                }
                else if(Operator.ContainsOperator(wordsToFind[i], '!', searcher.Operators)){
                    //Remueve el path y la palabra que contenga el operador !(Porque ya se cumple la funcion)
                    for (int j = 0; j < searcher.Paths.Length; j++)
                    {
                        if(searcher.Info[searcher.Paths[j]].ContainsKey(wordsToFind[i].ToLower()))
                            tempPaths.Remove(searcher.Paths[j]);
                    }
                    tempWordsToFind.Remove(wordsToFind[i]);
                }
                else if(Operator.ContainsOperator(wordsToFind[i], '^', searcher.Operators)){
                    //Remueve el path que no contenga la palbra del operador ^
                    for (int j = 0; j < searcher.Paths.Length; j++){
                        if(!searcher.Info[searcher.Paths[j]].ContainsKey(wordsToFind[i]))
                            tempPaths.Remove(searcher.Paths[j]);
                    }
                }
                else if(Operator.ContainsOperator(wordsToFind[i], '*', searcher.Operators)){
                    if(NoExistsWord(wordsToFind[i], searcher.Paths, tempWordsToFind, searcher.Info)) 
                        searcher.WordsMask[i] = true;
                }
                else{
                    if(NoExistsWord(wordsToFind[i], searcher.Paths, tempWordsToFind, searcher.Info)) 
                        searcher.WordsMask[i] = true;
                }
            }
            if(oper != null){
                newOper.Add(oper);
                Operator.SearchDistance(searcher.Paths, searcher.Info, searcher.Texts, searcher.Operators);
            }
            searcher.Operators = newOper.ToArray();
        }

        searcher.Paths = tempPaths.ToArray();
        wordsToFind = tempWordsToFind.ToArray();
        return  wordsToFind;
    }
    private List<string> CommonWords(List<string> wordsToFind, string[] paths, AllTextsInfo info){
        //Chequea si en la List exiten palabras comunes(IDF=0)
        //Las elimina de la busqueda
        List<string> tempWordsToFind = new List<string>(wordsToFind);
        for (int i = 0; i < tempWordsToFind.Count(); i++)
        {
            for (int j = 0; j < paths.Length; j++)
            {
                if(info[paths[j]].ContainsKey(tempWordsToFind[i]) && 
                    info[paths[j]][tempWordsToFind[i]].TimesInCorpus == info.Keys.Count() || tempWordsToFind[i] == "")
                {
                    wordsToFind.Remove(tempWordsToFind[i]);
                }
            }
        }
        return wordsToFind;
    }
    private bool NoExistsWord(string word, string[] paths, List<string> wordsCopy, AllTextsInfo info){
        //Devuelve True o False dependiendo si la palabra no exite o si en el corpus
        int count = 0;
        int i = 0;
        while(count == 0 && i < paths.Length){
            if(info[paths[i++]].ContainsKey(word)){
                count++;
            }
        }
        if(count==0){
            wordsCopy.Remove(word);
            return true;
        }
        return false;
    }
}