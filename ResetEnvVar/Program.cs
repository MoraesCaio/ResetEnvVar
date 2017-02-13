using System;
using System.Diagnostics;
using System.Deployment.Application;
using System.Reflection;
using Microsoft.Win32;
using System.IO;


namespace ResetEnvVar
{
    class Program
    {
        //MACHINE
        static void apagarVarEnvMCH(string variavel){
    		try{
	        	Console.WriteLine("Variável "+variavel+":");
	        	string t = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.Machine);
	            if(t == null){
	                Console.WriteLine(variavel+" não existe.");
	            }else{
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.Machine));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Apagando.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        static void criarVarEnvMCH(string variavel, string valor){
    		try{
	        	Console.WriteLine("Variável "+variavel+":");
	        	string t = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.Machine);
	            if(t == null || t != valor){
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.Machine);
		        	string s = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.Machine);
		        	Console.WriteLine("Variável "+variavel+" criada. Seu valor é: "+s+".");
		        }else{
	                Console.WriteLine(variavel+" já existe.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //USER
		static void apagarVarEnvUSR(string variavel){
    		try{
	        	Console.WriteLine("Variável "+variavel+":");
	        	string t = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.User);
	            if(t == null){
	                Console.WriteLine(variavel+" não existe.");
	            }else{
		        	Console.WriteLine(Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.User));
		        	Environment.SetEnvironmentVariable(variavel, "", EnvironmentVariableTarget.User);
		        	Console.WriteLine("Apagando.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        static void criarVarEnvUSR(string variavel, string valor){
    		try{
	        	Console.WriteLine("Variável "+variavel+":");
	        	string t = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.User);
	            if(t == null || t != valor){
		        	Console.WriteLine("Criando.");
		        	Environment.SetEnvironmentVariable(variavel, valor, EnvironmentVariableTarget.User);
		        	string s = Environment.GetEnvironmentVariable(variavel,EnvironmentVariableTarget.User);
		        	Console.WriteLine("Variável "+variavel+" criada. Seu valor é: "+s+".");
		        }else{
	                Console.WriteLine(variavel+" já existe.");
	            }
    		}catch(Exception e){
    			Console.WriteLine("Erro {0}", e);
    		}
        }

        //BOTH
        static void criarVarEnv(string variavel, string valor){
            criarVarEnvUSR(variavel, valor);
            criarVarEnvMCH(variavel, valor);
        }

        static string thisProcessPath(){
            Process[] proc = Process.GetProcesses();
            foreach(Process p in proc){
                if(p.ProcessName.Contains("ResetEnvVar.exe")){
                    return p.Modules[0].FileName;
                }
            }
            return "";
        }
        static void Main(string[] args)
        {
            //var currentPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //currentPath = currentPath.Replace("ResetEnvVar.exe","");
            //var currentPath = Directory +GetParent(thisProcessPath()).FullName, @"VLibras\");
            string pathVLibras = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,@"VLibras");// currentPath; //@"..\VLibras";
        	criarVarEnvUSR("PATH_VLIBRAS", pathVLibras);
        	criarVarEnvUSR("AELIUS_DATA", pathVLibras + @"\aelius_data");
        	criarVarEnvUSR("HUNPOS_TAGGER", pathVLibras + @"\bin\hunpos-tag.exe");
        	criarVarEnvUSR("NLTK_DATA", pathVLibras + @"\nltk_data");
        	criarVarEnvUSR("TRANSLATE_DATA", pathVLibras + @"\translate\data");

       		//CONIFGURAÇÃO DA VARIÁVEL PYTHONPATH
        	string python = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"Python");
            //@"..\Python";//pythonReg.GetValue(null).ToString();
            //python = python.Remove(python.Length - 1).ToString();
            string valor = python+@";"+
	            python + @"\Scripts;"+
	            python + @"\Lib\site-packages;"+
	            pathVLibras + @";"+
	            pathVLibras + @"\bin;"+
	            pathVLibras + @"\translate\src;"+
	            pathVLibras + @"\Clipboard;"+
	            pathVLibras + @"\update;"+
	            pathVLibras + @"\nltk_data;"+
	            pathVLibras + @"\Aelius";
	            if(Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine) != null ||
                    Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine) == ""){
		            string pythonPathAtual = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine);
                    if(pythonPathAtual.Contains(python) && pythonPathAtual.Contains(@"\Aelius")){
                        int comeco = pythonPathAtual.IndexOf(python);
                        int fim = pythonPathAtual.IndexOf(@"\Aelius");
                        string substring = pythonPathAtual.Substring(comeco, fim - comeco + (@"\Aelius".Length + 1));
                        pythonPathAtual = pythonPathAtual.Remove(comeco, substring.Length);
                    }

                    criarVarEnvMCH("PYTHONPATH", valor+";"+pythonPathAtual);

                }else{
                    Console.WriteLine("else");
	            	criarVarEnvMCH("PYTHONPATH", valor);
	            }

	            string pathPython2 = python+@";"+
                python +@"\Scripts;";

	        	if(Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine) != null){
	        		string varPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
                    if(varPath.Contains(python) && varPath.Contains(@"\Scripts")){
                        int comeco = varPath.IndexOf(python);
                        int fim = varPath.LastIndexOf(@"\Scripts");
                        string substring = varPath.Substring(comeco, fim - comeco + (@"\Scripts".Length+1));
                        varPath = varPath.Remove(comeco, substring.Length);
                    }
	        		if(!varPath.Contains(pathPython2)){
                        if(varPath.EndsWith(";")){
                            criarVarEnvMCH("Path", varPath+pathPython2);
                        }else{
    	        			criarVarEnvMCH("Path", varPath+";"+pathPython2);
                        }
	        		}
	        	}else{
	        		criarVarEnvMCH("Path", pathPython2);
	        	}
            Console.WriteLine("Variáveis configuradas.");
            Console.ReadKey();
        }
    }
}
