---
title: "yzl - YAML Zero-Language - F# DSL for YAML"
---

Generating API docs:

```
dotnet run -p ~/github/FSharp.Formatting/src/FSharp.Formatting.CommandTool/ -- build --nodefaultcontent --projects ./src/Yzl.fsproj  --output docs/yzl --input docs/yzl --clean
```

Work with docs:
```
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs \
  squidfunk/mkdocs-material serve -a 0.0.0.0:8089
```