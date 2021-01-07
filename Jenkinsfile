pipeline {  
 agent any  
 environment {  
  dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
 }  
 stages {  
  stage('Checkout') {  
   steps {  
    git credentialsId: 'e7b2b49d-db4a-4e87-b403-f9e6d2f16fe0', url: 'https://github.com/philipeano/Konveyor', branch: 'jenkins-integration'  
   }  
  }  
 stage('Build') {  
   steps {  
    bat 'dotnet build Konveyor\\Konveyor.sln --configuration Release'  
   }  
  }  
  stage('Test') {  
   steps {  
    bat 'dotnet test Konveyor\\Konveyor.Common.Tests\\Konveyor.Common.Tests.csproj --logger:trx'  
   }  
  }  
 }  
} 