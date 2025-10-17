set -e

echo "🔍 Starting SonarCloud analysis..."

dotnet tool install --global dotnet-sonarscanner || true
export PATH="$PATH:/root/.dotnet/tools"

if [ -z "$SONAR_TOKEN" ]; then
  echo "❌ SONAR_TOKEN is not set. Exiting..."
  exit 1
fi

SONAR_PROJECT_KEY=PDF-Cloner_api
SONAR_ORG=pdf-cloner

if [ -z "$SONAR_PROJECT_KEY" ]; then
  echo "❌ sonar.projectKey not found in sonar-project.properties"
  exit 1
fi

if [ -z "$SONAR_ORG" ]; then
  echo "❌ sonar.organization not found in sonar-project.properties"
  exit 1
fi

echo "📄 Loaded Sonar project configuration:"
echo "   Project Key: $SONAR_PROJECT_KEY"
echo "   Organization: $SONAR_ORG"

dotnet sonarscanner begin \
  /k:"$SONAR_PROJECT_KEY" \
  /o:"$SONAR_ORG" \
  /d:sonar.token="$SONAR_TOKEN" \
  /d:sonar.cs.vstest.reportsPaths="Tests/TestResults/*/*.xml" \
  /d:sonar.cs.opencover.reportsPaths="Tests/TestResults/Coverage/coverage.xml" \
  /d:sonar.verbose=true

echo "🏗 Running build & test pipeline..."
bash ./Scripts/test.sh

dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"

echo "✅ SonarCloud analysis completed successfully."
