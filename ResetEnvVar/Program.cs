using System;
using System.IO;


namespace ResetEnvVar
{
	public class Program
    {
        //MACHINE
        public static void deleteEnvVarMCH(string variavel)
        {
    		try
            {
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);

                if(t == null)
                {
	                Console.WriteLine(variavel + " não existe.");
	            }
                else
                {
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Apagando.");
	            }
    		}
            catch(Exception e)
            {
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        public static void setEnvVarMCH(string variavel, string valor)
        {
    		try
            {
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);

	            if(t == null || t != valor)
                {
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.Machine);
		        	string s = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Variável " + variavel + " criada. Seu valor é: " + s + ".");
		        }
                else
                {
	                Console.WriteLine(variavel + " já está configurada.");
	            }
    		}
            catch(Exception e)
            {
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //USER
		public static void deleteEnvVarUSR(string variavel)
        {
    		try
            {
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);

	            if(t == null)
                {
	                Console.WriteLine(variavel + " não existe.");
	            }
                else
                {
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.User);
		        	Console.WriteLine("Apagando.");
	            }
    		}
            catch(Exception e)
            {
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        public static void setEnvVarUSR(string variavel, string valor)
        {
    		try
            {
	        	Console.WriteLine("Variável " + variavel + ":");
	        	string t = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);

                if(t == null || t != valor)
                {
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.User);
		        	string s = Environment.GetEnvironmentVariable(variavel, EnvironmentVariableTarget.User);
		        	Console.WriteLine("Variável " + variavel + " criada. Seu valor é: " + s + ".");
		        }
                else
                {
	                Console.WriteLine(variavel + " já está configurada.");
	            }
    		}
            catch(Exception e)
            {
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //BOTH
        public static void setEnvVar(string variavel, string valor)
        {
            setEnvVarUSR(variavel, valor);
            setEnvVarMCH(variavel, valor);
        }

        static void Main(string[] args)
        {
            if(args.Length == 0 || (args.Length == 1 && args[0] == "-i"))
            {
                string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string mainFolder = Path.Combine(local, "VLibras");
                string python = Path.Combine(mainFolder, @"Python");
                string pathVLibras = Path.Combine(mainFolder, @"VLibras");

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
            else if(args.Length == 1 && args[0] == "-u")
            {
                deleteEnvVarUSR("PATH_VLIBRAS");
                deleteEnvVarUSR("AELIUS_DATA");
                deleteEnvVarUSR("HUNPOS_TAGGER");
                deleteEnvVarUSR("NLTK_DATA");
                deleteEnvVarUSR("TRANSLATE_DATA");
                deleteEnvVarUSR("PYTHONPATH");
            }
            else
            {
                Console.WriteLine("Argumentos inválidos.");
            }
        }
    }
}
