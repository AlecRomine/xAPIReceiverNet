pipeline {
  agent any
  stages {
    stage('Clean') {
      steps {
        echo 'Clean build folders'
        bat 'dotnet clean xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj'
      }
    }

    stage('Build') {
      steps {
        echo 'building'
        bat 'dotnet build xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj  --configuration Release'
      }
    }

    stage('Publish') {
      steps {
        echo 'perfoming publish to deploy folders'
        bat 'dotnet publish xAPIReciever\\xAPIReceiver\\xAPIReceiver.csproj  --configuration Release --framework netcoreapp3.1 --output .\\publish'
        powershell 'get-childitem -Directory \'.\\publish\' -recurse'
      }
    }

  }
  environment {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}