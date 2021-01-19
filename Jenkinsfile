pipeline{
    agent any

   
    environment {
        dotnet ='C:\\Program Files (x86)\\dotnet\\'
        }
        
  stages{
     stage('Clean'){
 	   steps{
            echo "Clean Bat"
        	bat "dotnet clean xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj"
     	        }
   	}
	stage('Build'){
	    steps{
            echo "build Bat"
      		bat "dotnet build xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj --configuration Release"
	        }
 	    }
    }
}