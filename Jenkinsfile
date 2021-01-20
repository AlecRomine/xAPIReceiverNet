pipeline {
  agent any
  stages {
    stage('Clean') {
      steps {
        echo 'Clean Bat'
        bat 'dotnet clean xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj'
      }
    }

    stage('Build') {
      steps {
        echo 'build Bat'
        echo 'building'
        bat 'dotnet build xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj  --configuration Release'
      }
    }

  }
  environment {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}