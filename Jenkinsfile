
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
                sshPublisher(publishers: [sshPublisherDesc(configName: 'IISTest', transfers: [sshTransfer(cleanRemote: false, excludes: '', execCommand: '', execTimeout: 120000, flatten: false, makeEmptyDirs: false, noDefaultExcludes: false, patternSeparator: '[, ]+', remoteDirectory: '', remoteDirectorySDF: false, removePrefix: '', sourceFiles: './publish/**')], usePromotionTimestamp: false, useWorkspaceInPromotion: false, verbose: false)])
            }
        }
    }
  }
  environment  {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}