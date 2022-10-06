namespace MoogleLogic;

public class Operator{
    public Operator(char operatorChar, List<List<(string Word, int Times)>> wordAndtimes){
        OperatorCharacter = operatorChar;
        WordAndTimes = wordAndtimes;
    }

#region Static Methods
    public static bool ContainsOperator(string word, char oper, Operator[] operators){

        if(operators == null) return false;
        //Revisar si la palabra contiene algun tipo de operador

        for (int i = 0; i < operators.Length; i++)
        {
            for (int j = 0; j < operators[i].WordAndTimes.Count(); j++)
            {
                for (int k = 0; k < operators[i].WordAndTimes[j].Count(); k++)
                {
                    if((oper == operators[i].OperatorCharacter) && (word.ToLower()==operators[i].WordAndTimes[j][k].Word.ToLower())){
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public static Operator? ReturnOperator(char operChar, Operator[] operators){

        //Devolver el tipo Operator de la palabra indicada
        if(operators == null) return null;

        int i = 0;
        while(i < operators.Length)
        {
            if(operators[i].OperatorCharacter == operChar){
                return operators[i];
            }
            i++;
        }
        return null;
    }
    private static bool CheckExistanceOperator(char operChar, string word, Operator[] operators){
        //Revisa si las palabra contiene otro tipo de operador
        for (int i = 0; i < operators.Length; i++)
        {
            if(operators[i].OperatorCharacter != '*' && operators[i].OperatorCharacter != operChar && operators[i].WordAndTimes[0].Contains((word, 1))){
                return true;
            }
        }
        return false;
    }
    public static int OperatorDistance(AllTextsInfo info, string word, string path, Operator[] operators)
    {
        //Calcula la distancia entre los pares de palabras del operador de distancia
        Operator? oper = Operator.ReturnOperator('~', operators);
        int distance= int.MaxValue;
        string secondWord = "";
        bool found = false;

        for (int i = 0; i < oper.WordAndTimes.Count(); i++)
        {
            for (int j = 0; j < oper.WordAndTimes[i].Count(); j++)
            {
                if((oper.WordAndTimes[i].Contains((word.ToLower(), 1)) && (secondWord = oper.WordAndTimes[i][j].Word) != word.ToLower())){
                    found= true;
                    break;
                }
            }
            if(found)break;
        }

        if(!info[path].ContainsKey(secondWord.ToLower())) return -1;

        for (int i = 0; i < info[path][word.ToLower()].Indexes.Count(); i++)
        {
            for (int j = 0; j < info[path][secondWord.ToLower()].Indexes.Count(); j++)
            {
                int temp = Math.Abs(info[path][word.ToLower()].Indexes[i] - info[path][secondWord.ToLower()].Indexes[j]);
                distance = temp < distance? temp : distance;
            }
        }
        return distance;
    }
    public static string CleanWord(string word){
        //Elimina los caracteres que no son letras al inicio de la palabra
        while (!char.IsLetter(word[0]))
        {
            word = word.Substring(1);
        }
        return word;
    }
    public static Operator[] RemoveOperatorsNotDistance(Operator[] operators){
        //Elimina los operadores que las palabras el operador de ditancia contenga
        Operator? distanceOper = ReturnOperator('~', operators);

        if(distanceOper == null) return operators;

        List<Operator> newOperators = new List<Operator>(operators);

        for (int k = 0; k < distanceOper.WordAndTimes.Count(); k++)
        {
            if(newOperators.Count()==0) break;

            for (int h = 0; h < distanceOper.WordAndTimes[k].Count(); h++)
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    if(operators[i].OperatorCharacter == '~' || operators[i].OperatorCharacter == '*') continue;

                    for (int j = 0; j < operators[i].WordAndTimes.Count(); j++)
                    {
                        if(operators[i].WordAndTimes[j].Contains((distanceOper.WordAndTimes[k][h].Word, 1))){
                            newOperators[i].WordAndTimes[j].Remove((distanceOper.WordAndTimes[k][h].Word, 1));
                            if(newOperators[i].WordAndTimes[j].Count()==0)
                                newOperators.Remove(operators[i]);
                        }
                    }
                }
            }
        }
        return newOperators.ToArray();
    }
    public static void AddDistanceOperator(List<Operator> operators, string firstWord, string secondWord ,int group){
        //Anade los pares de palabras del operador de distancia 
        if(!operators.Exists(j=>j.OperatorCharacter == '~')){
            operators.Add(new Operator('~', new List<List<(string Word, int Times)>>(){ new List<(string Word, int Times)>(){ new ( firstWord, 1), new (secondWord, 1)}}));
        }
        else{
            for (int h = 0; h < operators.Count(); h++)
            {   
                if(operators[h].OperatorCharacter == '~'){
                    operators[h].WordAndTimes.Add(new List<(string Word, int Times)>(){new (firstWord, 1), new (secondWord, 1)});
                }
            }
        }
    }
    public static void AddSimpleOperator(List<Operator> operators, char operChar, string word, int times){
        //Anade las palabras a los operadores distintos del operador de distancia
        if(!operators.Exists(operValue=> operValue.OperatorCharacter == operChar)){
            operators.Add(new Operator(operChar, new List<List<(string Word, int Times)>>(){new List<(string Word, int Times)>(){new (word, times)}}));
        }
        else{
            for (int i = 0; i < operators.Count(); i++)
            {   
                if(operators[i].OperatorCharacter == operChar){
                    operators[i].WordAndTimes[0].Add(new(word, times));
                }
            }
        } 
    }
    public static string CutWithOutOperator(List<Operator> operators, string word){
        //Anade la palabra a la lista de operadores
        if(word.StartsWith('!')){
            while(!char.IsLetter(word[0])){
                word = word.Substring(1);
            }
            if(!CheckExistanceOperator('!', word, operators.ToArray()))
                AddSimpleOperator(operators, '!', word, 1);
            return word;
        }
        else if(word.StartsWith('^')){
            while(!char.IsLetter(word[0])){
                word = word.Substring(1);
            }
            if(!CheckExistanceOperator('^', word, operators.ToArray()))
                AddSimpleOperator(operators, '^', word, 1);
            return word;
        }
        else if(word.StartsWith('*'))
        {
            int times = 0;

            while (!char.IsLetter(word[0]))
            {
                if(word[0] == '*')
                    times+=1;
                word=word.Substring(1);
            }
            AddSimpleOperator(operators, '*', word, times);

            return word;
        }
        else if(!char.IsLetter(word[0])){

            while(!char.IsLetter(word[0])){
                word= word.Substring(1);
            }
            return word;
        }
        else{
            return word;
        }
    }
    public static int OperatorsTimes(string word, char operChar, Operator[] operators){

        //Revisar cuantas veces se repite el operador '*' en la palabra correspondiente

        for (int i = 0; i < operators.Length; i++)
        {
            for (int j = 0; j < operators[i].WordAndTimes.Count(); j++)
            {
                for (int k = 0; k < operators[i].WordAndTimes[j].Count(); k++)
                {
                    if((operChar == operators[i].OperatorCharacter) && (word.ToLower()==operators[i].WordAndTimes[j][k].Word.ToLower())){
                        return operators[i].WordAndTimes[j][k].Times;
                    }
                }
            }
        }
        return 0;
    } 
    public static void SearchDistance(string[] paths, AllTextsInfo info, TextsPerDoc textPerDoc, Operator[] operators){
        //Metodo para buscar para realizar la accion del operador de cercania
        //Calculando la menor distancia entre las palabra escritas por el usaurio en el texto
        Operator? operDistance = Operator.ReturnOperator('~', operators);

        foreach (string path in paths)
        {
            for(int i = 0; i <  operDistance?.WordAndTimes.Count(); i++)
            {
                for (int j = 0; j < operDistance.WordAndTimes[i].Count(); j++)
                {   
                    if(info[path].ContainsKey(operDistance.WordAndTimes[i][j].Word)){
                        int distance = Operator.OperatorDistance(info, operDistance.WordAndTimes[i][j].Word, path, operators);
                        if(distance != -1){
                            info[path][operDistance.WordAndTimes[i][j].Word].PlusValue = (((float)textPerDoc[path].Count() - 
                                        distance)/(float)textPerDoc[path].Count()) * 2;
                        }
                    }
                }
            }
        }
    }
#endregion

#region #region Fields and Variables
    public char OperatorCharacter{get;private set;}
    public List<List<(string Word, int Times)>> WordAndTimes{get; private set;}
    
#endregion

}