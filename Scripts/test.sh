set -euo pipefail

echo "Running Unit Tests..."
dotnet test Tests/UT/UT.csproj \
  -c Release \
  --logger "trx;LogFileName=UnitTests.trx" \
  --collect:"XPlat Code Coverage"

echo "Running Integration Tests..."
dotnet test Tests/IT/IT.csproj \
  -c Release \
  --logger "trx;LogFileName=IntegrationTests.trx"

echo "Running End-to-End Tests..."
dotnet test Tests/E2E/E2E.csproj \
  -c Release \
  --logger "trx;LogFileName=E2ETests.trx"

echo "Copying coverage reports..."
mkdir -p TestResults/Coverage
find . -type f -name 'coverage.xml' -exec cp {} Tests/TestResults/Coverage/coverage.xml \;

echo "Tests and coverage completed."
