#!/bin/bash

dotnet build src

dotnet run --no-build -p ~/github/FSharp.Formatting/src/FSharp.Formatting.CommandTool/ -- build \
  --nodefaultcontent \
  --projects ./src/Yzl.fsproj \
  --output ./docs \
  --input ./fsdocs \
  --parameters root $1
