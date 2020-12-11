dotnet build src
dotnet run --no-build -p ~/github/FSharp.Formatting/src/FSharp.Formatting.CommandTool/ -- \
  build --nodefaultcontent --projects ./src/Yzl.fsproj --output ./docs --input ./fsdocs \
   --parameters root "https://queil.github.io/yzl/"
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs squidfunk/mkdocs-material gh-deploy
