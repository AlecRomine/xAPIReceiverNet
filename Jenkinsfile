
@Library('ELRRLib')_


pipeline {
  agent any
  stages {
    stage('Clean'){
      steps{
        script{
          dotnetClean(".\\xAPIReciever\\xAPIReceiver","xAPIReceiver.csproj")
        }
      }
    }

    stage('Build'){
      steps{
        script{
          dotnetBuild(".\\xAPIReciever\\xAPIReceiver","xAPIReceiver.csproj","Release")
        }
      }
    }

    stage('Publish'){
      steps{
        script{
            dotnetPublish(".\\xAPIReciever\\xAPIReceiver","xAPIReceiver.csproj","Release", "netcoreapp3.1",".\\publish" )
            powershell 'get-childitem \'.\\publish\' -recurse'
        }
      }
    }
    stage('deploy'){
        steps{
            script{
                powershell label: 'Deployment', script: '''$Source = ".\\\\publish\\\\*.*"
                $Destination = "\\\\172.31.0.157\\xAPIReceiver" 
                New-Item -ItemType directory -Path $Destination -Force
                Copy-Item -Recurse -Path $Source\\*.* -Destination $Destination -Force '''
            }
        }
    }
  }
  environment  {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}