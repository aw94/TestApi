pipeline {
    agent { dockerfile {
            filename 'Dockerfile'
            args '--entrypoint=/bin/bash'
    }
    stages {
        stage('Test') {
            steps {
                sh 'ls -al'
            }
        }
    }
}