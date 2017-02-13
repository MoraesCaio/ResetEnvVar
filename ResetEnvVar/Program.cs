using System;
using System.IO;


namespace ResetEnvVar
{
	class Program
    {
        //MACHINE
        static void deleteEnvVarMCH(string variavel){
    		try{
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);
	            if(t == null){
	                Console.WriteLine(variavel + " não existe.");
	            }else{
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Apagando.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        static void setEnvVarMCH(string variavel, string valor){
    		try{
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);
	            if(t == null || t != valor){
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.Machine);
		        	string s = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Variável " + variavel + " criada. Seu valor é: " + s + ".");
		        }else{
	                Console.WriteLine(variavel + " já está configurada.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //USER
		static void deleteEnvVarUSR(string variavel){
    		try{
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);
	            if(t == null){
	                Console.WriteLine(variavel + " não existe.");
	            }else{
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.User);
		        	Console.WriteLine("Apagando.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        static void setEnvVarUSR(string variavel, string valor){
    		try{
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);
	            if(t == null || t != valor){
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.User);
		        	string s = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);
		        	Console.WriteLine("Variável " + variavel + " criada. Seu valor é: " + s + ".");
		        }else{
	                Console.WriteLine(variavel + " já está configurada.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //BOTH
        static void setEnvVar(string variavel, string valor){
            setEnvVarUSR(variavel, valor);
            setEnvVarMCH(variavel, valor);
        }

        static void Main(string[] args)
        {
            //ex: GetCurrentDirectory().FullName -> folder1/folder2/folder3/
            //GetParent() -> folder1/folder2/
            //Combine(dir, @"Python") -> folder1/folder2/Python/
        	string python = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"Python");
            string pathVLibras = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"VLibras");

            //SETTING VLIBRAS VARIABLES
            setEnvVarUSR("PATH_VLIBRAS",   pathVLibras);
            setEnvVarUSR("AELIUS_DATA",    pathVLibras + @"\aelius_data");
            setEnvVarUSR("HUNPOS_TAGGER",  pathVLibras + @"\bin\hunpos-tag.exe");
            setEnvVarUSR("NLTK_DATA",      pathVLibras + @"\nltk_data");
            setEnvVarUSR("TRANSLATE_DATA", pathVLibras + @"\translate\data");

            //SETTING PYTHONPATH (PORTABLE VERSION - 2.7.12)
            string pythonPath = python+@";" + 
	            python + @"\Scripts;" + 
	            python + @"\Lib\site-packages;" + 
	            pathVLibras + @";" + 
	            pathVLibras + @"\bin;" + 
	            pathVLibras + @"\translate\src;" + 
	            pathVLibras + @"\Clipboard;" + 
	            pathVLibras + @"\update;" + 
	            pathVLibras + @"\nltk_data;" + 
	            pathVLibras + @"\Aelius";

            setEnvVarUSR("PYTHONPATH", pythonPath);

            Console.WriteLine("Variáveis configuradas.");
        }
    }
}
