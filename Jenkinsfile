pipeline {  
 agent any  
 environment {  
  dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'  
 }  
 stages {  
  stage('Checkout') {  
   steps {  
    git credentialsId: 'c4d77f79-b332-4211-b8be-f32c145f02cf', url: 'https://github.com/philipeano/Konveyor', branch: ''  
   }  
  }  
 stage('Build') {  
   steps {  
    bat 'dotnet msbuild Konveyor.sln  -r:True -t:Rebuild -p:Configuration=Release'  
   }  
  }  
  stage('Test') {  
   steps {  
    bat 'dotnet test Konveyor.Common.Tests\\Konveyor.Common.Tests.csproj --logger:trx'  
   }  
  }  
 }  
} 
