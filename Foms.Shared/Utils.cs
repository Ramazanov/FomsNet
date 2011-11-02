namespace Foms.Shared
{
    public class Utils
    {
        public static string[] Splitter(string chaine)
        {   
            string delimeter = ",;=% ";
            char[] separateur = delimeter.ToCharArray();
            return  chaine.Split(separateur);
        }
    }
}


        



    
