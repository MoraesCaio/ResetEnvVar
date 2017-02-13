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
        static void setEnvVarMCH(string variavel){
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

        static void setEnvVarMCH(string variavel, string valor){
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
		static void setEnvVarUSR(string variavel){
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

        static void setEnvVarUSR(string variavel, string valor){
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
        static void setEnvVar(string variavel, string valor){
            setEnvVarUSR(variavel, valor);
            setEnvVarMCH(variavel, valor);
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
            string pathVLibras = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,@"VLibras");
        	setEnvVarUSR("PATH_VLIBRAS", pathVLibras);
        	setEnvVarUSR("AELIUS_DATA", pathVLibras + @"\aelius_data");
        	setEnvVarUSR("HUNPOS_TAGGER", pathVLibras + @"\bin\hunpos-tag.exe");
        	setEnvVarUSR("NLTK_DATA", pathVLibras + @"\nltk_data");
        	setEnvVarUSR("TRANSLATE_DATA", pathVLibras + @"\translate\data");

       		//CONIFGURAÇÃO DA VARIÁVEL PYTHONPATH
        	string python = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"Python");
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

                    setEnvVarMCH("PYTHONPATH", valor+";"+pythonPathAtual);

                }else{
                    Console.WriteLine("else");
	            	setEnvVarMCH("PYTHONPATH", valor);
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
                            setEnvVarMCH("Path", varPath+pathPython2);
                        }else{
    	        			setEnvVarMCH("Path", varPath+";"+pathPython2);
                        }
	        		}
	        	}else{
	        		setEnvVarMCH("Path", pathPython2);
	        	}
            Console.WriteLine("Variáveis configuradas.");
        }
    }
}
