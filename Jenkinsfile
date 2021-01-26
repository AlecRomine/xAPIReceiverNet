
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
                ftpPublisher alwaysPublishFromMaster: true, continueOnError: false, failOnError: false, masterNodeName: '', paramPublish: null, publishers: [[configName: 'ELRR-Test', transfers: [[asciiMode: false, cleanRemote: true, excludes: '', flatten: false, makeEmptyDirs: false, noDefaultExcludes: false, patternSeparator: '[, ]+', remoteDirectory: '', remoteDirectorySDF: false, removePrefix: 'publish', sourceFiles: 'publish/*,publish/**/*']], usePromotionTimestamp: false, useWorkspaceInPromotion: false, verbose: false]]
            }
        }
    }
  }
  environment  {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}