using System;
namespace BS{
    public class RowLabels{
         public const string Labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";   

         /// <summary>
         /// Return the index value of chracter witin the <see cref="Labels"/> string. 
         /// </summary>
         /// <param name="lable"></param>
         /// <returns>-1 if not found, 0 or more if found.</returns>
         public static int GetLabelIndex(string lable){
             if (Labels.Contains(lable)){
                 return Labels.IndexOf(lable);
             }
             return -1; 
         }
    }
}