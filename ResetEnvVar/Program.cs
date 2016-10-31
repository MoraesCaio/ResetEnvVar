using System;
using Microsoft.Win32;
using System.IO;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;//LOCAL MACHINE x64(semPy)->nenhum;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Security.Principal;*/

namespace ResetEnvVar
{
    class Program
    {
        static void apagarVarEnv(string variavel){
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

        static void criarVarEnv(string variavel, string valor){
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

        static void Main(string[] args)
        {
			var lm64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            var lm32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        	var cr64 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            var cr32 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
        	var cu64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            var cu32 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);


        	string pathVLibras = Directory.GetCurrentDirectory();
        	criarVarEnvUSR("PATH_VLIBRAS", pathVLibras);
        	criarVarEnvUSR("AELIUS_DATA", pathVLibras + @"\aelius_data");
        	criarVarEnvUSR("HUNPOS_TAGGER", pathVLibras + @"\bin\hunpos-tag.exe");
        	criarVarEnvUSR("NLTK_DATA", pathVLibras + @"\nltk_data");
        	criarVarEnvUSR("TRANSLATE_DATA", pathVLibras + @"\translate\data");


        	//RECUPERAÇÃO DO PYTHON INSTALL PATH PELO REGEDIT
        	RegistryKey pythonReg = null;
            //LOCAL MACHINE
        	if(lm64.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = lm64.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath");
            }
            if(lm32.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = lm32.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath");
            }
            if(lm32.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = lm32.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath");
            }
            if(lm64.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = lm64.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath");
            }
            //CURRENT USER
            if(cu64.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = cu64.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath");
            }
            if(cu32.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = cu32.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath");
            }
            if(cu32.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = cu32.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath");
            }
            if(cu64.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath") != null){
                pythonReg = cu64.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath");
            }

        	if(pythonReg != null){
        		//CONIFGURAÇÃO DA VARIÁVEL PYTHONPATH
        		string python = pythonReg.GetValue(null).ToString();
        		python = python.Remove(python.Length - 1).ToString();
	            string valor = python+@";"+
	            python+@"\Scripts;"+
	            python+@"\Lib\site-packages;"+
	            pathVLibras+@";"+
	            pathVLibras+@"\bin;"+
	            pathVLibras+@"\translate\src;"+
	            pathVLibras+@"\Clipboard;"+
	            pathVLibras+@"\update;"+
	            pathVLibras+@"\nltk_data;"+
	            pathVLibras+@"\Aelius;";
	            if(Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine) != null){
		            string pythonPathAtual = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine);
		            if(!pythonPathAtual.Contains(valor)){
						criarVarEnv("PYTHONPATH", valor+";"+pythonPathAtual);
		            }
	            }else{
	            	criarVarEnv("PYTHONPATH", valor);
	            }
	            string pathPython2 = python+@";"+
                python+@"\Scripts;";/*Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Machine);
		    	int idx = pathPython2.IndexOf(@";");
		    	pathPython2 = pathPython2.Remove(idx, pathPython2.Length - idx);*/
		    	//pathPython2 = pathPython2 + "\\python.exe";
	            //CONFIGURAÇÃO DA VARIÁVEL "Path"
	        	if(Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine) != null){
	        		string varPath = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
	        		if(!varPath.Contains(pathPython2)){
                        if(varPath.EndsWith(";")){
                            criarVarEnv("Path", varPath+pathPython2);
                        }else{
    	        			criarVarEnv("Path", varPath+";"+pathPython2);
                        }
	        		}
	        	}else{
	        		criarVarEnv("Path", pathPython2);
	        	}
        	}
    		/*RegistryKey pythonShell = cr64.OpenSubKey(@"Python.File\Shell\open\command");
            if(pythonShell == null){
                pythonShell = cr32.OpenSubKey(@"Python.File\Shell\open\command");
            }
        	if(pythonShell != null){
	            //path do python na máquina
            	string python = pythonShell.GetValue(null).ToString();
            	//Setando Path
            	python = python.Remove(python.Length - 9).Replace("\"", "");*/

            	/*string python = @"C:\Python27";
	        	string valorDePATH = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
				if(!valorDePATH.Contains(python)){
					Environment.SetEnvironmentVariable("Path", valorDePATH+@";"+python, EnvironmentVariableTarget.Machine);
				}*/

            	//python = python.Replace(@"\python.exe","");


        	//}
            Console.WriteLine("Variáveis configuradas.");
        }
    }
}
