pipeline {
    agent { dockerfile {
            filename 'Dockerfile'
            args '--entrypoint='
    }
    }
    stages {
        stage('Test') {
            steps {
                sh 'cd ./ControllerTests' 
                sh 'ls -al'
            }
        }
    }
}