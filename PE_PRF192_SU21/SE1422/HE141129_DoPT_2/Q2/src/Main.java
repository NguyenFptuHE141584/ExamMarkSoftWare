// ======= DO NOT EDIT MAIN FUNCTION ============
import java.util.*;
import java.io.*;
class Main
{   
   public static void f1(ArrayList<Integer> a){     
        int index = -1;
        for (int i = 0; i < a.size(); i++) {
           if(a.get(i) % 2 == 0){
               index = i;
           }
        }
        a.set(index, a.get(index)+1);
   }
   public static void main(String args[]) throws Exception
   {
       BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
       ArrayList<Integer> a = new ArrayList<>();
       System.out.print("Enter number of item: ");
       int n = Integer.parseInt(in.readLine());
       for(int i = 0; i < n; i++){
           System.out.print("Number " + i + " (th): ");
           a.add(Integer.parseInt(in.readLine()));
       }       
       f1(a);
       System.out.println("OUTPUT:");
       for(int i = 0; i < n; i++){
           System.out.println(a.get(i));
       }
   }
 }
